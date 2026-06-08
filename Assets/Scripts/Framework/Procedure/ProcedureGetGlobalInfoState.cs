using System;
using Cysharp.Threading.Tasks;
using GameFrameX.Fsm.Runtime;
using GameFrameX.GlobalConfig.Runtime;
using GameFrameX.Procedure.Runtime;
using GameFrameX.Runtime;
using GameFrameX.Web.Runtime;
using UnityEngine;
using YooAsset;

namespace Unity.Startup.Procedure
{
    /// <summary>
    /// 获取全局信息
    /// </summary>
    public sealed class ProcedureGetGlobalInfoState : ProcedureBase
    {
        private const int MaxRetryCount = 3;
        private const int RetryDelayMilliseconds = 3000;

        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            // 编辑器下的模拟模式
            if (GameApp.Asset.GamePlayMode == EPlayMode.EditorSimulateMode)
            {
                Debug.Log("当前为编辑器模式，直接启动 FsmGetGlobalInfoState");
                ChangeState<ProcedureGetAppVersionInfoState>(procedureOwner);
                return;
            }

            if (GameApp.Asset.GamePlayMode == EPlayMode.OfflinePlayMode)
            {
                Debug.Log("当前为离线模式，直接启动 ProcedurePatchInit");
                ChangeState<ProcedurePatchInit>(procedureOwner);
                return;
            }
            GetGlobalInfo(procedureOwner);
        }

        private async void GetGlobalInfo(IFsm<IProcedureManager> procedureOwner)
        {
            string rootUrl = "http://127.0.0.1:20808/api/GameGlobalInfo/GetInfo";
            var jsonParams = HttpHelper.GetBaseParams();
            for (int retryIndex = 1; retryIndex <= MaxRetryCount; retryIndex++)
            {
                try
                {
                    var json = await GameApp.Web.PostToString(rootUrl, jsonParams);
                    Debug.Log(json);

                    HttpJsonResult httpJsonResult = Utility.Json.ToObject<HttpJsonResult>(json.Result);
                    if (httpJsonResult.Code > 0)
                    {
                        Log.Error($"获取全局信息返回异常=> Retry:{retryIndex}/{MaxRetryCount} Req:{jsonParams} Resp:{json}");
                        if (!await WaitForRetry(retryIndex, "Server error, retrying..."))
                        {
                            return;
                        }

                        continue;
                    }

                    ResponseGlobalInfo responseGlobalInfo = Utility.Json.ToObject<ResponseGlobalInfo>(httpJsonResult.Data);
                    GlobalConfigComponent globalConfigComponent = GameApp.GlobalConfig;
                    globalConfigComponent.CheckAppVersionUrl = responseGlobalInfo.CheckAppVersionUrl;
                    globalConfigComponent.CheckResourceVersionUrl = responseGlobalInfo.CheckResourceVersionUrl;
                    globalConfigComponent.Content = responseGlobalInfo.Content;
                    // TODO  这里要自己从Content中解析。因为可能有多个
                    // globalConfigComponent.HostServerUrl = responseGlobalInfo.CheckResourceVersionUrl;
                    LauncherUIHandler.SetTipText("Loading...");
                    ChangeState<ProcedureGetAppVersionInfoState>(procedureOwner);
                    return;
                }
                catch (Exception e)
                {
                    Log.Error(e);
                    Log.Error($"获取全局信息异常=> Retry:{retryIndex}/{MaxRetryCount} Error:{e.Message} Req:{jsonParams}");
                    if (!await WaitForRetry(retryIndex, "Network error, retrying..."))
                    {
                        return;
                    }
                }
            }
        }

        private static async UniTask<bool> WaitForRetry(int retryIndex, string retryTipText)
        {
            if (retryIndex >= MaxRetryCount)
            {
                LauncherUIHandler.SetTipText("网络请求失败，请检查本地服务配置后重新启动。");
                return false;
            }

            LauncherUIHandler.SetTipText($"{retryTipText} ({retryIndex}/{MaxRetryCount})");
            await UniTask.Delay(RetryDelayMilliseconds);
            return true;
        }
    }
}

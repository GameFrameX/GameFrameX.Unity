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
            try
            {
                var json = await GameApp.Web.PostToString(rootUrl, jsonParams);
                Debug.Log(json);
                
                HttpJsonResult httpJsonResult = Utility.Json.ToObject<HttpJsonResult>(json.Result);
                if (httpJsonResult.Code > 0)
                {
                    // GameApp.EventSystem.Run(EventIdType.UILoadingMainSetText, "Server error, retrying...");
                    LauncherUIHandler.SetTipText("Server error, retrying...");
                    Log.Error($"获取全局信息返回异常=> Req:{jsonParams} Resp:{json}");

                    await UniTask.Delay(3000);
                    // GAHelper.DesignEvent("GetGlobalInfoServerError");
                    GetGlobalInfo(procedureOwner);
                }
                else
                {
                    ResponseGlobalInfo responseGlobalInfo = Utility.Json.ToObject<ResponseGlobalInfo>(httpJsonResult.Data);
                    GlobalConfigComponent globalConfigComponent = GameApp.GlobalConfig;
                    globalConfigComponent.CheckAppVersionUrl = responseGlobalInfo.CheckAppVersionUrl;
                    globalConfigComponent.CheckResourceVersionUrl = responseGlobalInfo.CheckResourceVersionUrl;
                    globalConfigComponent.Content = responseGlobalInfo.Content;
                    // TODO  这里要自己从Content中解析。因为可能有多个
                    // globalConfigComponent.HostServerUrl = responseGlobalInfo.CheckResourceVersionUrl;
                    // Game.EventSystem.Run(EventIdType.UILoadingMainSetText, "Loading...");
                    LauncherUIHandler.SetTipText("Loading...");
                    ChangeState<ProcedureGetAppVersionInfoState>(procedureOwner);
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
                //     // GameApp.EventSystem.Run(EventIdType.UILoadingMainSetText, "Network error, retrying...");
                LauncherUIHandler.SetTipText("Network error, retrying...");
                Log.Error($"获取全局信息异常=>Error:{e.Message}   Req:{jsonParams}");
                await UniTask.Delay(3000);
                GetGlobalInfo(procedureOwner);
            }
        }
    }
}
using System;
using Cysharp.Threading.Tasks;
using GameFrameX.Fsm;
using GameFrameX.Fsm.Runtime;
using GameFrameX.GlobalConfig.Runtime;
using GameFrameX.Procedure.Runtime;
using GameFrameX.Runtime;
using UnityEngine;
using YooAsset;

namespace GameFrameX.Procedure
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
            if (GameEntry.GetComponent<AssetComponent>().GamePlayMode == EPlayMode.EditorSimulateMode)
            {
                Debug.Log("当前为编辑器模式，直接启动 FsmGetGlobalInfoState");
                ChangeState<ProcedureGetAppVersionInfoState>(procedureOwner);
                return;
            }

            GetGlobalInfo(procedureOwner);
        }

        private async void GetGlobalInfo(IFsm<IProcedureManager> procedureOwner)
        {
            string rootUrl = "http://172.18.0.31:20808/api/GameGlobalInfo/GetInfo";
            var jsonParams = HttpHelper.GetBaseParams();
            try
            {
                var json = await GameApp.Web.GetToString(rootUrl, jsonParams);
                Debug.Log(json);
                HttpJsonResult httpJsonResult = Utility.Json.ToObject<HttpJsonResult>(json);
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
                    GlobalConfigComponent globalConfigComponent = GameEntry.GetComponent<GlobalConfigComponent>();
                    globalConfigComponent.CheckAppVersionUrl = responseGlobalInfo.CheckAppVersionUrl;
                    globalConfigComponent.CheckResourceVersionUrl = responseGlobalInfo.CheckResourceVersionUrl;
                    globalConfigComponent.Content = responseGlobalInfo.Content;

                    globalConfigComponent.HostServerUrl = responseGlobalInfo.CheckResourceVersionUrl;
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
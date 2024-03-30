using System;
using Cysharp.Threading.Tasks;
using Game.Model;
using GameFrameX.Fsm;
using GameFrameX.Localization;
using GameFrameX.Runtime;
using UnityEngine;
using YooAsset;

namespace GameFrameX.Procedure
{
    /// <summary>
    /// 获取版本信息
    /// </summary>
    public sealed class ProcedureGetAppVersionInfoState : ProcedureBase
    {
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            // 编辑器下的模拟模式
            if (GameEntry.GetComponent<AssetComponent>().GamePlayMode == EPlayMode.EditorSimulateMode)
            {
                Debug.Log("当前为编辑器模式，直接启动 FsmGetAppVersionInfoState");
                ChangeState<ProcedurePatchInit>(procedureOwner);
                return;
            }

            GetAppVersionInfo(procedureOwner);
        }

        private async void GetAppVersionInfo(IFsm<IProcedureManager> procedureOwner)
        {
            var jsonParams = HttpHelper.GetBaseParams();
            try
            {
                var json = await GameApp.Web.PostToString(GameApp.GlobalConfig.CheckAppVersionUrl, jsonParams);
                Debug.Log(json);

                HttpJsonResult httpJsonResult = Utility.Json.ToObject<HttpJsonResult>(json);
                if (httpJsonResult.Code > 0)
                {
                    LauncherUIHandler.SetTipText("Server error, retrying...");
                    Debug.LogError($"获取全局信息返回异常=> Req:{jsonParams} Resp:{json}");
                    await UniTask.Delay(3000);
                    GetAppVersionInfo(procedureOwner);
                }
                else
                {
                    ResponseGameAppVersion responseGameAppVersion = Utility.Json.ToObject<ResponseGameAppVersion>(httpJsonResult.Data);

                    if (responseGameAppVersion.IsUpgrade)
                    {
                        var uiLoadingMainScene = GameApp.UI.Get<UILauncher>(UILauncher.UIResName);
                        uiLoadingMainScene.m_IsUpgrade.SetSelectedIndex(1);

                        bool isChinese = GameApp.Localization.SystemLanguage == Language.ChineseSimplified ||
                                         GameApp.Localization.SystemLanguage == Language.ChineseTraditional;

                        uiLoadingMainScene.m_upgrade.m_EnterButton.title = isChinese ? "确认" : "Enter";
                        uiLoadingMainScene.m_upgrade.m_TextContent.title = responseGameAppVersion.UpdateAnnouncement;
                        uiLoadingMainScene.m_upgrade.m_TextContent.onClickLink.Set((context => { Application.OpenURL(context.data.ToString()); }));
                        uiLoadingMainScene.m_upgrade.m_EnterButton.onClick.Set(() =>
                        {
                            if (responseGameAppVersion.IsForce)
                            {
                                Application.OpenURL(responseGameAppVersion.AppDownloadUrl);
                            }
                            else
                            {
                                uiLoadingMainScene.m_IsUpgrade.SetSelectedIndex(0);
                                ChangeState<ProcedurePatchInit>(procedureOwner);
                            }
                        });
                    }
                    else
                    {
                        ChangeState<ProcedurePatchInit>(procedureOwner);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
                LauncherUIHandler.SetTipText("Network error, retrying...");
                Debug.LogError($"获取版本信息异常=>Error:{e.Message}   Req:{jsonParams}");
                await UniTask.Delay(3000);
                GetAppVersionInfo(procedureOwner);
            }
        }
    }
}
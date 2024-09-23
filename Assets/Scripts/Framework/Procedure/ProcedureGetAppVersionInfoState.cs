using System;
using Cysharp.Threading.Tasks;
using GameFrameX.Fsm.Runtime;
using GameFrameX.GlobalConfig.Runtime;
using GameFrameX.Procedure.Runtime;
using GameFrameX.Runtime;
using UnityEngine;
using YooAsset;

namespace Unity.Startup.Procedure
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
            if (GameApp.Asset.GamePlayMode == EPlayMode.EditorSimulateMode)
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

                HttpJsonResult httpJsonResult = Utility.Json.ToObject<HttpJsonResult>(json.Result);
                if (httpJsonResult.Code > 0)
                {
                    LauncherUIHandler.SetTipText("Server error, retrying...");
                    Debug.LogError($"获取全局信息返回异常=> Req:{jsonParams} Resp:{json}");
                    await UniTask.Delay(3000);
                    GetAppVersionInfo(procedureOwner);
                }
                else
                {
                    var gameAppVersion = Utility.Json.ToObject<ResponseGameAppVersion>(httpJsonResult.Data);

                    /*if (gameAppVersion.IsUpgrade)
                    {
                        var uiLoadingMainScene = GameApp.FUI.Get<UILauncher>(UILauncher.UIResName);
                        uiLoadingMainScene.m_IsUpgrade.SetSelectedIndex(1);

                        bool isChinese = GameApp.Localization.SystemLanguage == Language.ChineseSimplified ||
                                         GameApp.Localization.SystemLanguage == Language.ChineseTraditional;

                        uiLoadingMainScene.m_upgrade.m_EnterButton.title = isChinese ? "确认" : "Enter";
                        uiLoadingMainScene.m_upgrade.m_TextContent.title = gameAppVersion.UpdateAnnouncement;
                        uiLoadingMainScene.m_upgrade.m_TextContent.onClickLink.Set((context => { Application.OpenURL(context.data.ToString()); }));
                        uiLoadingMainScene.m_upgrade.m_EnterButton.onClick.Set(() =>
                        {
                            if (gameAppVersion.IsForce)
                            {
                                Application.OpenURL(gameAppVersion.AppDownloadUrl);
                            }
                            else
                            {
                                uiLoadingMainScene.m_IsUpgrade.SetSelectedIndex(0);
                                ChangeState<ProcedureGetGameAssetPackageVersionInfoByDefaultPackageState>(procedureOwner);
                            }
                        });
                    }
                    else*/
                    {
                        ChangeState<ProcedureGetGameAssetPackageVersionInfoByDefaultPackageState>(procedureOwner);
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
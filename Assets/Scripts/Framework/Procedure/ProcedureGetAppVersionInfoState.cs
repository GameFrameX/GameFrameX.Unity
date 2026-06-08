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
    /// 获取版本信息
    /// </summary>
    public sealed class ProcedureGetAppVersionInfoState : ProcedureBase
    {
        private const int MaxRetryCount = 3;
        private const int RetryDelayMilliseconds = 3000;

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
            for (int retryIndex = 1; retryIndex <= MaxRetryCount; retryIndex++)
            {
                try
                {
                    var json = await GameApp.Web.PostToString(GameApp.GlobalConfig.CheckAppVersionUrl, jsonParams);
                    Debug.Log(json);

                    HttpJsonResult httpJsonResult = Utility.Json.ToObject<HttpJsonResult>(json.Result);
                    if (httpJsonResult.Code > 0)
                    {
                        LauncherUIHandler.SetTipText($"Server error, retrying... ({retryIndex}/{MaxRetryCount})");
                        Debug.LogError($"获取版本信息返回异常=> Retry:{retryIndex}/{MaxRetryCount} Req:{jsonParams} Resp:{json}");
                        if (!await WaitForRetry(retryIndex, "服务器版本信息异常，请检查服务配置后重新启动。"))
                        {
                            return;
                        }

                        continue;
                    }

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
                        return;
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e);
                    LauncherUIHandler.SetTipText($"Network error, retrying... ({retryIndex}/{MaxRetryCount})");
                    Debug.LogError($"获取版本信息异常=> Retry:{retryIndex}/{MaxRetryCount} Error:{e.Message} Req:{jsonParams}");
                    if (!await WaitForRetry(retryIndex, "网络请求失败，请检查版本服务配置后重新启动。"))
                    {
                        return;
                    }
                }
            }
        }

        private static async UniTask<bool> WaitForRetry(int retryIndex, string failedTipText)
        {
            if (retryIndex >= MaxRetryCount)
            {
                LauncherUIHandler.SetTipText(failedTipText);
                return false;
            }

            await UniTask.Delay(RetryDelayMilliseconds);
            return true;
        }
    }
}

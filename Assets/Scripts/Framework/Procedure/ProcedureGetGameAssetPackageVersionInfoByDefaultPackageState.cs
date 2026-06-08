using System;
using System.IO;
using Cysharp.Threading.Tasks;
using GameFrameX.Asset.Runtime;
using GameFrameX.Fsm.Runtime;
using GameFrameX.GlobalConfig.Runtime;
using GameFrameX.Procedure.Runtime;
using GameFrameX.Runtime;
using GameFrameX.Web.Runtime;
using UnityEngine;

namespace Unity.Startup.Procedure
{
    /// <summary>
    /// 获取默认资源版本信息
    /// </summary>
    public sealed class ProcedureGetGameAssetPackageVersionInfoByDefaultPackageState : ProcedureBase
    {
        private const int MaxRetryCount = 3;
        private const int RetryDelayMilliseconds = 3000;

        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            GetGameAssetPackageVersionInfo(procedureOwner);
        }

        private async void GetGameAssetPackageVersionInfo(IFsm<IProcedureManager> procedureOwner)
        {
            var jsonParams = HttpHelper.GetBaseParams();
            for (int retryIndex = 1; retryIndex <= MaxRetryCount; retryIndex++)
            {
                try
                {
                    jsonParams["AssetPackageName"] = AssetComponent.BuildInPackageName;
                    var json = await GameApp.Web.PostToString(GameApp.GlobalConfig.CheckResourceVersionUrl, jsonParams);
                    Debug.Log(json);

                    HttpJsonResult httpJsonResult = Utility.Json.ToObject<HttpJsonResult>(json.Result);
                    if (httpJsonResult.Code > 0)
                    {
                        LauncherUIHandler.SetTipText($"获取资源版本信息异常, 正在重试... ({retryIndex}/{MaxRetryCount})");
                        Debug.LogError($"获取资源版本信息异常=> Retry:{retryIndex}/{MaxRetryCount} Req:{Utility.Json.ToJson(jsonParams)} Resp:{json}");
                        if (!await WaitForRetry(retryIndex, "获取资源版本信息失败，请检查资源服务配置后重新启动。"))
                        {
                            return;
                        }

                        continue;
                    }

                    var gameAssetPackageVersion = Utility.Json.ToObject<ResponseGameAssetPackageVersion>(httpJsonResult.Data);

                    var gameAppVersionPath = Path.Combine(gameAssetPackageVersion.RootPath, gameAssetPackageVersion.PackageName, gameAssetPackageVersion.Platform, gameAssetPackageVersion.AppVersion, gameAssetPackageVersion.Channel, gameAssetPackageVersion.AssetPackageName, gameAssetPackageVersion.Version) + Path.DirectorySeparatorChar;
                    var varStringDownloadURL = ReferencePool.Acquire<VarString>();
                    varStringDownloadURL.SetValue(gameAppVersionPath);
                    procedureOwner.SetData(AssetComponent.BuildInPackageName, varStringDownloadURL);

                    var varStringVersion = ReferencePool.Acquire<VarString>();
                    varStringVersion.SetValue(gameAssetPackageVersion.Version);
                    procedureOwner.SetData(AssetComponent.BuildInPackageName + "Version", varStringVersion);
                    ChangeState<ProcedurePatchInit>(procedureOwner);
                    return;
                }
                catch (Exception e)
                {
                    Log.Error(e);
                    LauncherUIHandler.SetTipText($"获取资源版本信息异常, 正在重试... ({retryIndex}/{MaxRetryCount})");
                    Debug.LogError($"获取资源版本信息异常=> Retry:{retryIndex}/{MaxRetryCount} Error:{e.Message} Req:{Utility.Json.ToJson(jsonParams)}");
                    if (!await WaitForRetry(retryIndex, "获取资源版本信息失败，请检查资源服务配置后重新启动。"))
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

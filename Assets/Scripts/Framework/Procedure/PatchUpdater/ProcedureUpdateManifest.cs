using System.Collections;
using Cysharp.Threading.Tasks;
using GameFrameX.Asset.Runtime;
using GameFrameX.Fsm.Runtime;
using GameFrameX.Procedure.Runtime;
using GameFrameX.Runtime;
using UnityEngine;
using YooAsset;

namespace Unity.Startup.Procedure
{
    internal sealed class ProcedureUpdateManifest : ProcedureBase
    {
        protected override async void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            if (GameApp.Asset.GamePlayMode == EPlayMode.OfflinePlayMode)
            {
                var varStringVersion = procedureOwner.GetData<VarString>(AssetComponent.BuildInPackageName + "Version");
                var buildInResourcePackage = GameApp.Asset.GetAssetsPackage(AssetComponent.BuildInPackageName);
                var buildInOperation = buildInResourcePackage.UpdatePackageManifestAsync(varStringVersion.Value);
                await buildInOperation.ToUniTask();
                ChangeState<ProcedurePatchDone>(procedureOwner);
                return;
            }

            GameApp.Event.Fire(this, AssetPatchStatesChangeEventArgs.Create(AssetComponent.BuildInPackageName, EPatchStates.UpdateManifest));
            await UpdateManifest(procedureOwner).ToUniTask();
        }


        private IEnumerator UpdateManifest(IFsm<IProcedureManager> procedureOwner)
        {
            yield return new WaitForSecondsRealtime(0.1f);

            var buildInResourcePackage = YooAssets.GetPackage(AssetComponent.BuildInPackageName);
            UpdatePackageManifestOperation buildInOperation;
            if (GameApp.Asset.GamePlayMode == EPlayMode.EditorSimulateMode)
            {
                buildInOperation = buildInResourcePackage.UpdatePackageManifestAsync("Simulate");
            }
            else
            {
                var varStringVersion = procedureOwner.GetData<VarString>(AssetComponent.BuildInPackageName + "Version");

                buildInOperation = buildInResourcePackage.UpdatePackageManifestAsync(varStringVersion.Value);
            }

            yield return buildInOperation;


            if (buildInOperation.Status == EOperationStatus.Succeed)
            {
                //更新成功
                ChangeState<ProcedureCreateDownloader>(procedureOwner);
                procedureOwner.RemoveData(AssetComponent.BuildInPackageName + "Version");
            }
            else
            {
                //更新失败
                Debug.LogError(buildInOperation.Error);
                GameApp.Event.Fire(this, AssetPatchManifestUpdateFailedEventArgs.Create(AssetComponent.BuildInPackageName, buildInOperation.Error));
                ChangeState<ProcedureUpdateManifest>(procedureOwner);
            }
        }
    }
}
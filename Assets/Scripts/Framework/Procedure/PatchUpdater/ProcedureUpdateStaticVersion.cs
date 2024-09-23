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
    internal sealed class ProcedureUpdateStaticVersion : ProcedureBase
    {
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            GameApp.Event.Fire(this, AssetPatchStatesChangeEventArgs.Create(AssetComponent.BuildInPackageName, EPatchStates.UpdateStaticVersion));
            GetStaticVersion(procedureOwner).ToUniTask();
        }

        private IEnumerator GetStaticVersion(IFsm<IProcedureManager> procedureOwner)
        {
            var buildInResourcePackage = YooAssets.GetPackage(AssetComponent.BuildInPackageName);
            var buildInOperation = buildInResourcePackage.RequestPackageVersionAsync();
            yield return buildInOperation;

            if (buildInOperation.Status == EOperationStatus.Succeed)
            {
                //更新成功
                string packageVersion = buildInOperation.PackageVersion;
                if (GameApp.Asset.GamePlayMode == EPlayMode.OfflinePlayMode)
                {
                    var varStringVersion = ReferencePool.Acquire<VarString>();
                    varStringVersion.SetValue(packageVersion);
                    procedureOwner.SetData(AssetComponent.BuildInPackageName + "Version", varStringVersion);
                }

                Debug.Log($"Updated package Version : {packageVersion}");
                ChangeState<ProcedureUpdateManifest>(procedureOwner);
            }
            else
            {
                //更新失败
                Debug.LogError(buildInOperation.Error);
                GameApp.Event.Fire(this, AssetStaticVersionUpdateFailedEventArgs.Create(AssetComponent.BuildInPackageName, buildInOperation.Error));
                ChangeState<ProcedureUpdateStaticVersion>(procedureOwner);
            }
        }
    }
}
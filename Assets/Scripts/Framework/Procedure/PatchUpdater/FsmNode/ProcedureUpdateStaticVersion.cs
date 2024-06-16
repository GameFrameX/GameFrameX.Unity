using System.Collections;
using Cysharp.Threading.Tasks;
using GameFrameX.Asset.Runtime;
using GameFrameX.Fsm.Runtime;
using GameFrameX.Procedure.Runtime;
using UnityEngine;
using YooAsset;

namespace GameFrameX.Procedure
{
    internal sealed class ProcedureUpdateStaticVersion : ProcedureBase
    {
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            PatchEventDispatcher.SendPatchStepsChangeMsg(EPatchStates.UpdateStaticVersion);
            GetStaticVersion(procedureOwner).ToUniTask();
        }

        private IEnumerator GetStaticVersion(IFsm<IProcedureManager> procedureOwner)
        {
            yield return new WaitForSecondsRealtime(0.5f);

            var buildInResourcePackage = YooAssets.GetPackage(AssetComponent.BuildInPackageName);
            var buildInOperation = buildInResourcePackage.UpdatePackageVersionAsync();
            yield return buildInOperation;

            if (buildInOperation.Status == EOperationStatus.Succeed)
            {
                //更新成功
                string packageVersion = buildInOperation.PackageVersion;
                Debug.Log($"Updated package Version : {packageVersion}");
                ChangeState<ProcedureUpdateManifest>(procedureOwner);
            }
            else
            {
                //更新失败
                Debug.LogError(buildInOperation.Error);
                PatchEventDispatcher.SendStaticVersionUpdateFailedMsg();
                ChangeState<ProcedureUpdateStaticVersion>(procedureOwner);
            }
        }
    }
}
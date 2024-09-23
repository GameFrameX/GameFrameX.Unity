using GameFrameX.Asset.Runtime;
using GameFrameX.Fsm.Runtime;
using GameFrameX.Procedure.Runtime;
using UnityEngine;

namespace Unity.Startup.Procedure
{
    internal sealed class ProcedurePatchDone : ProcedureBase
    {
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            GameApp.Event.Fire(this, AssetPatchStatesChangeEventArgs.Create(AssetComponent.BuildInPackageName, EPatchStates.PatchDone));
            LauncherUIHandler.SetProgressUpdateFinish();
            LauncherUIHandler.SetTipText(string.Empty);
            Debug.Log("补丁流程更新完毕！");
            ChangeState<ProcedureGameLauncherState>(procedureOwner);
        }
    }
}
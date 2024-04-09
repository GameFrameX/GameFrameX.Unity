using GameFrameX.Fsm;
using GameFrameX.Fsm.Runtime;
using GameFrameX.Procedure;
using GameFrameX.Procedure.Runtime;
using UnityEngine;

namespace GameFrameX.Procedure
{
    internal sealed class ProcedurePatchDone : ProcedureBase
    {
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            PatchEventDispatcher.SendPatchStepsChangeMsg(EPatchStates.PatchDone);
            LauncherUIHandler.SetProgressUpdateFinish();
            LauncherUIHandler.SetTipText(string.Empty);
            Debug.Log("补丁流程更新完毕！");
            ChangeState<ProcedureGameLauncherState>(procedureOwner);
        }
    }
}
using Cysharp.Threading.Tasks;
using GameFrameX.Fsm.Runtime;
using GameFrameX.Procedure.Runtime;

namespace Unity.Startup.Procedure
{
    /// <summary>
    /// 启动热更新游戏
    /// </summary>
    public sealed class ProcedureGameLauncherState : ProcedureBase
    {
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            Start();
        }

        private async void Start()
        {
            await UniTask.DelayFrame();
            HotfixHelper.StartHotfix();
            LauncherUIHandler.Dispose();
        }
    }
}
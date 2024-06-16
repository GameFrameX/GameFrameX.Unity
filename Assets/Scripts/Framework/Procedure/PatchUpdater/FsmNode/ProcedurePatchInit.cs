using System;
using Cysharp.Threading.Tasks;
using GameFrameX.Asset.Runtime;
using GameFrameX.Fsm.Runtime;
using GameFrameX.Procedure.Runtime;
using GameFrameX.Runtime;
using YooAsset;

namespace GameFrameX.Procedure
{
    internal sealed class ProcedurePatchInit : ProcedureBase
    {
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            if (GameApp.Asset.GamePlayMode == EPlayMode.EditorSimulateMode)
            {
                GameApp.Asset.InitPackage(AssetComponent.BuildInPackageName, string.Empty, string.Empty, true);
                ChangeState<ProcedureUpdateStaticVersion>(procedureOwner);
                return;
            }

            // Game.EventSystem.Run(EventIdType.UILoadingMainSetText, "Loading...");
            // 加载更新面板
            Start(procedureOwner);
        }

        async void Start(IFsm<IProcedureManager> procedureOwner)
        {
            var buildInPackageNameURL = procedureOwner.GetData<VarString>(AssetComponent.BuildInPackageName);
            Log.Debug("下载资源的路径：" + buildInPackageNameURL);
            GameApp.Asset.InitPackage(AssetComponent.BuildInPackageName, buildInPackageNameURL.Value, buildInPackageNameURL.Value, true);
            procedureOwner.RemoveData(AssetComponent.BuildInPackageName);
            // 运行补丁流程
            PatchUpdater.Run();
            await UniTask.DelayFrame();

            ChangeState<ProcedureUpdateStaticVersion>(procedureOwner);
        }
    }
}
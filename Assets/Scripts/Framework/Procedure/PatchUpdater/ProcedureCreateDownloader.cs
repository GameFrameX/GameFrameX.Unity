using GameFrameX.Asset.Runtime;
using GameFrameX.Fsm.Runtime;
using GameFrameX.Procedure.Runtime;
using GameFrameX.Runtime;
using UnityEngine;
using YooAsset;

namespace Unity.Startup.Procedure
{
    internal sealed class ProcedureCreateDownloader : ProcedureBase
    {
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            GameApp.Event.Fire(this, AssetPatchStatesChangeEventArgs.Create(AssetComponent.BuildInPackageName, EPatchStates.CreateDownloader));
            CreateDownloader(procedureOwner);
        }


        void CreateDownloader(IFsm<IProcedureManager> procedureOwner)
        {
            // Debug.Log("创建补丁下载器.");
            int downloadingMaxNum = 10;
            int failedTryAgain = 3;
            ResourceDownloaderOperation downloader = YooAssets.CreateResourceDownloader(downloadingMaxNum, failedTryAgain);
            var downloaderVarObject = new VarObject();
            downloaderVarObject.SetValue(downloader);
            procedureOwner.SetData<VarObject>("Downloader", downloaderVarObject);
            if (downloader.TotalDownloadCount == 0)
            {
                Debug.Log("没有发现需要下载的资源");
                ChangeState<ProcedurePatchDone>(procedureOwner);
            }
            else
            {
                Debug.Log($"一共发现了{downloader.TotalDownloadCount}个资源需要更新下载。");

                // 发现新更新文件后，挂起流程系统
                int totalDownloadCount = downloader.TotalDownloadCount;
                long totalDownloadBytes = downloader.TotalDownloadBytes;
                GameApp.Event.Fire(this, AssetFoundUpdateFilesEventArgs.Create(downloader.GetPackageName(), totalDownloadCount, totalDownloadBytes));
                ChangeState<ProcedureDownloadWebFiles>(procedureOwner);
            }
        }
    }
}
using System.Collections;
using Cysharp.Threading.Tasks;
using GameFrameX.Asset.Runtime;
using GameFrameX.Fsm.Runtime;
using GameFrameX.Procedure.Runtime;
using GameFrameX.Runtime;
using YooAsset;

namespace Unity.Startup.Procedure
{
    internal sealed class ProcedureDownloadWebFiles : ProcedureBase
    {
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            GameApp.Event.Fire(this, AssetPatchStatesChangeEventArgs.Create(AssetComponent.BuildInPackageName, EPatchStates.DownloadWebFiles));
            BeginDownload(procedureOwner).ToUniTask();
        }


        private IEnumerator BeginDownload(IFsm<IProcedureManager> procedureOwner)
        {
            var downloader = (ResourceDownloaderOperation)procedureOwner.GetData<VarObject>("Downloader").GetValue();


            // 注册下载回调
            void DownloaderOnDownloadErrorCallback(DownloadErrorData data)
            {
                GameApp.Event.Fire(this, AssetWebFileDownloadFailedEventArgs.Create(data.PackageName, data.FileName, data.ErrorInfo));
                ChangeState<ProcedureCreateDownloader>(procedureOwner);
            }

            downloader.DownloadErrorCallback = DownloaderOnDownloadErrorCallback;
            downloader.DownloadUpdateCallback = OnDownloadProgressCallback;
            downloader.BeginDownload();
            yield return downloader;

            // 检测下载结果
            if (downloader.Status != EOperationStatus.Succeed)
            {
                yield break;
            }

            ChangeState<ProcedurePatchDone>(procedureOwner);
        }

        private void OnDownloadProgressCallback(DownloadUpdateData data)
        {
            GameApp.Event.Fire(this, AssetDownloadProgressUpdateEventArgs.Create(data.PackageName, data.TotalDownloadCount, data.CurrentDownloadCount, data.TotalDownloadBytes, data.CurrentDownloadBytes));
        }
    }
}
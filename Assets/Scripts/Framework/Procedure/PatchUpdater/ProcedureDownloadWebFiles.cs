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
            void DownloaderOnDownloadErrorCallback(string packageName, string name, string error)
            {
                GameApp.Event.Fire(this, AssetWebFileDownloadFailedEventArgs.Create(packageName, name, error));
                ChangeState<ProcedureCreateDownloader>(procedureOwner);
            }

            downloader.OnDownloadErrorCallback    = DownloaderOnDownloadErrorCallback;
            downloader.OnDownloadProgressCallback = OnDownloadProgressCallback;
            downloader.BeginDownload();
            yield return downloader;

            // 检测下载结果
            if (downloader.Status != EOperationStatus.Succeed)
            {
                yield break;
            }

            ChangeState<ProcedurePatchDone>(procedureOwner);
        }

        private void OnDownloadProgressCallback(string packageName, int totalDownloadCount, int currentDownloadCount, long totalDownloadBytes, long currentDownloadBytes)
        {
            GameApp.Event.Fire(this, AssetDownloadProgressUpdateEventArgs.Create(packageName, totalDownloadCount, currentDownloadCount, totalDownloadBytes, currentDownloadBytes));
        }
    }
}
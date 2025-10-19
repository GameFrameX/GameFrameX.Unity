using GameFrameX.Asset.Runtime;
using GameFrameX.Event.Runtime;
using GameFrameX.Runtime;
using GameFrameX.UI.Runtime;

namespace Unity.Startup.Procedure
{
    public static class LauncherUIHandler
    {
        private static UILauncher _ui;

        public static async void Start()
        {
            _ui = await GameApp.UI.OpenFullScreenAsync<UILauncher>("UI/UILauncher", UIGroupConstants.Loading);

            GameApp.Event.CheckSubscribe(AssetDownloadProgressUpdateEventArgs.EventId, SetProgressUpdate);
        }

        public static void Dispose()
        {
            GameApp.UI.CloseUIForm<UILauncher>();
            _ui = null;
        }

        public static void SetTipText(string text)
        {
            _ui.m_TipText.text = text;
        }

        public static void SetProgressUpdateFinish()
        {
#if ENABLE_UI_FAIRYGUI
            _ui.m_IsDownload.SetSelectedIndex(0);
#endif
        }

        public static void SetProgressUpdate(object sender, GameEventArgs gameEventArgs)
        {
#if ENABLE_UI_FAIRYGUI
            _ui.m_IsDownload.SetSelectedIndex(1);
#endif
            var message = (AssetDownloadProgressUpdateEventArgs)gameEventArgs;
            float progress = message.CurrentDownloadSizeBytes / (message.TotalDownloadSizeBytes * 1f);
            string currentSizeMb = Utility.File.GetBytesSize(message.CurrentDownloadSizeBytes);
            string totalSizeMb = Utility.File.GetBytesSize(message.TotalDownloadSizeBytes);
            _ui.m_ProgressBar.value = progress * 100;
            _ui.m_TipText.text = $"Downloading {currentSizeMb}/{totalSizeMb}";
        }
    }
}
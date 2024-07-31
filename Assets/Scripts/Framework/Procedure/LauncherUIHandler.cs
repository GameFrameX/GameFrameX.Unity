using Game.Model;
using GameFrameX.Asset.Runtime;
using GameFrameX.Event.Runtime;
using GameFrameX.FairyGUI.Runtime;

namespace GameFrameX.Procedure
{
    public static class LauncherUIHandler
    {
        private static UILauncher _ui;

        public static void Start()
        {
            _ui = GameApp.FUI.AddToFullScreen(UILauncher.CreateInstance, "UI/UILauncher/UILauncher", UILayer.Loading);
            GameApp.Event.Subscribe(AssetDownloadProgressUpdateEventArgs.EventId, SetProgressUpdate);
        }

        public static void Dispose()
        {
            GameApp.FUI.Remove(UILauncher.UIResName, UILayer.Loading);
            if (_ui != null)
            {
                _ui.Dispose();
            }

            _ui = null;
        }

        public static void SetTipText(string text)
        {
            _ui.m_TipText.text = text;
        }

        public static void SetProgressUpdateFinish()
        {
            _ui.m_IsDownload.SetSelectedIndex(0);
        }

        public static void SetProgressUpdate(object sender, GameEventArgs gameEventArgs)
        {
            _ui.m_IsDownload.SetSelectedIndex(1);
            var    message       = (AssetDownloadProgressUpdateEventArgs)gameEventArgs;
            float  progress      = message.CurrentDownloadSizeBytes / (message.TotalDownloadSizeBytes * 1f);
            string currentSizeMb = Utility.File.GetBytesSize(message.CurrentDownloadSizeBytes);
            string totalSizeMb   = Utility.File.GetBytesSize(message.TotalDownloadSizeBytes);
            _ui.m_ProgressBar.value = progress * 100;
            _ui.m_TipText.text      = $"Downloading {currentSizeMb}/{totalSizeMb}";
        }
    }
}
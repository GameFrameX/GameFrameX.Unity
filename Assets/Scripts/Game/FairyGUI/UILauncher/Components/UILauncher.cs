/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using Cysharp.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;
using GameFrameX.Entity.Runtime;
using GameFrameX.UI.FairyGUI.Runtime;
using GameFrameX.Runtime;
using GameFrameX.UI.Runtime;
using GameFrameX.UI.UGUI.Runtime;
using UnityEngine;

namespace Unity.Startup
{
    public sealed partial class UILauncher : FUI
    {
        public const string UIPackageName = "UILauncher";
        public const string UIResName = "UILauncher";
        public const string URL = "ui://u7deosq0mw8e0";
        public Controller m_IsUpgrade { get; private set; }
        public Controller m_IsDownload { get; private set; }
        public GLoader m_bg { get; private set; }
        public GTextField m_TipText { get; private set; }
        public GProgressBar m_ProgressBar { get; private set; }
        public UILauncherUpgrade m_upgrade { get; private set; }

        protected override void InitView()
        {
            if (gameObject == null)
            {
                return;
            }

            var com = GObject.asCom;
            if (gameObject != null)
            {
                m_IsUpgrade = com.GetController("IsUpgrade");
                m_IsDownload = com.GetController("IsDownload");
                m_bg = (GLoader)com.GetChild("bg");
                m_TipText = (GTextField)com.GetChild("TipText");
                m_ProgressBar = (GProgressBar)com.GetChild("ProgressBar");
                m_upgrade = UILauncherUpgrade.Create(com.GetChild("upgrade"), this);
            }
        }

        public override void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            base.Dispose();
            m_IsUpgrade = null;
            m_IsDownload = null;
            m_bg = null;
            m_TipText = null;
            m_ProgressBar = null;
            m_upgrade = null;
        }

        public UILauncher(GObject gObject, object userData = null, bool isRoot = false) : base(gObject, userData, isRoot)
        {
        }
    }
}
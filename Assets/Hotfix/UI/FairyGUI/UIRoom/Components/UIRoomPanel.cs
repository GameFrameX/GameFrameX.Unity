/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

#if ENABLE_UI_FAIRYGUI
using FairyGUI;
using FairyGUI.Utils;
using GameFrameX.UI.Runtime;
using GameFrameX.UI.FairyGUI.Runtime;
using GameFrameX.Runtime;
using UnityEngine;

namespace Hotfix.UI
{
    [OptionUIConfig(UIPackageName)]
    public sealed partial class UIRoomPanel : FUI
    {
        public const string UIPackageName = "UIRoom";
        public const string UIResName = "UIRoomPanel";
        public const string URL = "ui://rpsroom1panel1";

        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self { get; private set; }



        private static GObject CreateGObject()
        {
            return UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static UIRoomPanel CreateInstance()
        {
            return Create(CreateGObject());
        }

        public static UIRoomPanel Create(GObject go)
        {
            var fui = go.displayObject.gameObject.GetOrAddComponent<UIRoomPanel>();
            fui?.SetGObject(go);
            fui?.InitView();
            return fui;
        }

        /// <summary>
        /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
        /// </summary>
        public static UIRoomPanel GetFormPool(GObject go)
        {
            var fui = go.Get<UIRoomPanel>();
            if (fui == null)
            {
                fui = Create(go);
            }

            fui.IsFromPool = true;
            return fui;
        }

        protected override void InitView()
        {
            if(GObject == null)
            {
                return;
            }

            self = (GComponent)GObject;
            self.Add(this);
            
            var com = GObject.asCom;
            if (com != null)
            {

            }
        }

        public override void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            base.Dispose();
            self.Remove();

            self = null;
        }

        private UIRoomPanel(GObject gObject) : base(gObject)
        {
        }
    }
}
#endif
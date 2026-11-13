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
    public sealed partial class UILogin : FUI
    {
        public const string UIPackageName = "UILogin";
        public const string UIResName = "UILogin";
        public const string URL = "ui://f011l0h9nmd0c";

        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self { get; private set; }



        private static GObject CreateGObject()
        {
            return UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static UILogin CreateInstance()
        {
            return Create(CreateGObject());
        }

        public static UILogin Create(GObject go)
        {
            var fui = go.displayObject.gameObject.GetOrAddComponent<UILogin>();
            fui?.SetGObject(go);
            fui?.InitView();
            return fui;
        }

        /// <summary>
        /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
        /// </summary>
        public static UILogin GetFormPool(GObject go)
        {
            var fui = go.Get<UILogin>();
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

        private UILogin(GObject gObject) : base(gObject)
        {
        }
    }
}
#endif
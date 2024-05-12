/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using Cysharp.Threading.Tasks;
using FairyGUI.Utils;
using GameFrameX.Entity.Runtime;
using GameFrameX.FairyGUI.Runtime;
using GameFrameX.Runtime;

namespace Hotfix.UI
{
    public sealed partial class UIMain : FUI
    {
        public const string UIPackageName = "UIMain";
        public const string UIResName = "UIMain";
        public const string URL = "ui://q9u97yzfxws70";

        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self { get; private set; }

		public GButton m_bag_button { get; private set; }

        private static GObject CreateGObject()
        {
            return UIPackage.CreateObject(UIPackageName, UIResName);
        }

        private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
        {
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
        }

        public static UIMain CreateInstance(object userData = null)
        {
            return new UIMain(CreateGObject(), userData);
        }

        public static UniTask<UIMain> CreateInstanceAsync(Entity domain, object userData = null)
        {
            UniTaskCompletionSource<UIMain> tcs = new UniTaskCompletionSource<UIMain>();
            CreateGObjectAsync((go) =>
            {
                tcs.TrySetResult(new UIMain(go, userData));
            });
            return tcs.Task;
        }

        public static UIMain Create(GObject go, object userData = null)
        {
            return new UIMain(go, userData);
        }

        /// <summary>
        /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
        /// </summary>
        public static UIMain GetFormPool(GObject go)
        {
            var fui =  go.Get<UIMain>();
            if(fui == null)
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
            if(com != null)
            {
				m_bag_button = (GButton)com.GetChild("bag_button");
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
			m_bag_button = null;
            self = null;            
        }

        private UIMain(GObject gObject, object userData) : base(gObject, userData)
        {
            // Awake(gObject);
        }
    }
}
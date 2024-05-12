/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using Cysharp.Threading.Tasks;
using FairyGUI.Utils;
using GameFrameX.Entity.Runtime;
using GameFrameX.FairyGUI.Runtime;
using GameFrameX.Runtime;

namespace Hotfix.UI
{
    public sealed partial class UIPlayerList : FUI
    {
        public const string UIPackageName = "UILogin";
        public const string UIResName = "UIPlayerList";
        public const string URL = "ui://f011l0h9i3dbs9m";

        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self { get; private set; }

		public GList m_player_list { get; private set; }

        private static GObject CreateGObject()
        {
            return UIPackage.CreateObject(UIPackageName, UIResName);
        }

        private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
        {
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
        }

        public static UIPlayerList CreateInstance(object userData = null)
        {
            return new UIPlayerList(CreateGObject(), userData);
        }

        public static UniTask<UIPlayerList> CreateInstanceAsync(Entity domain, object userData = null)
        {
            UniTaskCompletionSource<UIPlayerList> tcs = new UniTaskCompletionSource<UIPlayerList>();
            CreateGObjectAsync((go) =>
            {
                tcs.TrySetResult(new UIPlayerList(go, userData));
            });
            return tcs.Task;
        }

        public static UIPlayerList Create(GObject go, object userData = null)
        {
            return new UIPlayerList(go, userData);
        }

        /// <summary>
        /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
        /// </summary>
        public static UIPlayerList GetFormPool(GObject go)
        {
            var fui =  go.Get<UIPlayerList>();
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
				m_player_list = (GList)com.GetChild("player_list");
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
			m_player_list = null;
            self = null;            
        }

        private UIPlayerList(GObject gObject, object userData) : base(gObject, userData)
        {
            // Awake(gObject);
        }
    }
}
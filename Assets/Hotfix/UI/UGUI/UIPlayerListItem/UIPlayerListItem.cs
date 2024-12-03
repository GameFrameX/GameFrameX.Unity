/** This is an automatically generated class by UGUI. Please do not modify it. **/

#if ENABLE_UI_UGUI
using Cysharp.Threading.Tasks;
using GameFrameX.Entity.Runtime;
using GameFrameX.UI.Runtime;
using GameFrameX.Runtime;
using GameFrameX.UI.UGUI.Runtime;
using UnityEngine;

namespace Hotfix.UI
{
	/// <summary>
	/// 代码生成的UI代码UIPlayerListItem
	/// </summary>
	[DisallowMultipleComponent]
	public sealed partial class UIPlayerListItem : UGUI
	{
		public GameObject self { get; private set; }

		#region Properties
		[SerializeField] [UGUIElementProperty("click_Button")] private UnityEngine.UI.Button mclick_Button;
		public UnityEngine.UI.Button m_click_Button { get { return mclick_Button;} }

		[SerializeField] [UGUIElementProperty("icon")] private GameFrameX.UI.UGUI.Runtime.UIImage micon;
		public GameFrameX.UI.UGUI.Runtime.UIImage m_icon { get { return micon;} }

		[SerializeField] [UGUIElementProperty("level_text")] private UnityEngine.UI.Text mlevel_text;
		public UnityEngine.UI.Text m_level_text { get { return mlevel_text;} }

		[SerializeField] [UGUIElementProperty("name_text")] private UnityEngine.UI.Text mname_text;
		public UnityEngine.UI.Text m_name_text { get { return mname_text;} }

		#endregion

		protected override void InitView()
		{
			this.self = this.gameObject;
		}

	}
}
#endif

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
	/// 代码生成的UI代码UIPlayerList
	/// </summary>
	[DisallowMultipleComponent]
	public sealed partial class UIPlayerList : UGUI
	{
		public GameObject self { get; private set; }

		#region Properties
		[SerializeField] [UGUIElementProperty("left_Panel/Scroll View/Viewport/Content")] private UnityEngine.Transform mleft_Panel__ScrollView__Viewport__Content;
		public UnityEngine.Transform m_left_Panel__ScrollView__Viewport__Content { get { return mleft_Panel__ScrollView__Viewport__Content;} }

		[SerializeField] [UGUIElementProperty("left_Panel/Scroll View/Viewport")] private UnityEngine.UI.Image mleft_Panel__ScrollView__Viewport;
		public UnityEngine.UI.Image m_left_Panel__ScrollView__Viewport { get { return mleft_Panel__ScrollView__Viewport;} }

		[SerializeField] [UGUIElementProperty("left_Panel/Scroll View/Scrollbar Vertical/Sliding Area/Handle")] private UnityEngine.UI.Image mleft_Panel__ScrollView__ScrollbarVertical__SlidingArea__Handle;
		public UnityEngine.UI.Image m_left_Panel__ScrollView__ScrollbarVertical__SlidingArea__Handle { get { return mleft_Panel__ScrollView__ScrollbarVertical__SlidingArea__Handle;} }

		[SerializeField] [UGUIElementProperty("left_Panel/Scroll View/Scrollbar Vertical/Sliding Area")] private UnityEngine.Transform mleft_Panel__ScrollView__ScrollbarVertical__SlidingArea;
		public UnityEngine.Transform m_left_Panel__ScrollView__ScrollbarVertical__SlidingArea { get { return mleft_Panel__ScrollView__ScrollbarVertical__SlidingArea;} }

		[SerializeField] [UGUIElementProperty("left_Panel/Scroll View/Scrollbar Vertical")] private UnityEngine.UI.Scrollbar mleft_Panel__ScrollView__ScrollbarVertical;
		public UnityEngine.UI.Scrollbar m_left_Panel__ScrollView__ScrollbarVertical { get { return mleft_Panel__ScrollView__ScrollbarVertical;} }

		[SerializeField] [UGUIElementProperty("left_Panel/Scroll View")] private UnityEngine.UI.ScrollRect mleft_Panel__ScrollView;
		public UnityEngine.UI.ScrollRect m_left_Panel__ScrollView { get { return mleft_Panel__ScrollView;} }

		[SerializeField] [UGUIElementProperty("left_Panel")] private UnityEngine.UI.Image mleft_Panel;
		public UnityEngine.UI.Image m_left_Panel { get { return mleft_Panel;} }

		[SerializeField] [UGUIElementProperty("right_Panel/selected_name")] private UnityEngine.UI.Text mright_Panel__selected_name;
		public UnityEngine.UI.Text m_right_Panel__selected_name { get { return mright_Panel__selected_name;} }

		[SerializeField] [UGUIElementProperty("right_Panel/selected_level")] private UnityEngine.UI.Text mright_Panel__selected_level;
		public UnityEngine.UI.Text m_right_Panel__selected_level { get { return mright_Panel__selected_level;} }

		[SerializeField] [UGUIElementProperty("right_Panel/selected_icon")] private UnityEngine.UI.Image mright_Panel__selected_icon;
		public UnityEngine.UI.Image m_right_Panel__selected_icon { get { return mright_Panel__selected_icon;} }

		[SerializeField] [UGUIElementProperty("right_Panel/login_button/Text")] private UnityEngine.UI.Text mright_Panel__login_button__Text;
		public UnityEngine.UI.Text m_right_Panel__login_button__Text { get { return mright_Panel__login_button__Text;} }

		[SerializeField] [UGUIElementProperty("right_Panel/login_button")] private UnityEngine.UI.Button mright_Panel__login_button;
		public UnityEngine.UI.Button m_right_Panel__login_button { get { return mright_Panel__login_button;} }

		[SerializeField] [UGUIElementProperty("right_Panel")] private UnityEngine.UI.Image mright_Panel;
		public UnityEngine.UI.Image m_right_Panel { get { return mright_Panel;} }

		#endregion

		protected override void InitView()
		{
			this.self = this.gameObject;
		}

	}
}
#endif

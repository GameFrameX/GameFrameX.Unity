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
	/// 代码生成的UI代码UILogin
	/// </summary>
	[DisallowMultipleComponent]
	public sealed partial class UILogin : UGUI
	{
		public GameObject self { get; private set; }

		#region Properties
		[SerializeField] [UGUIElementProperty("UserName/Placeholder")] private UnityEngine.UI.Text mUserName__Placeholder;
		public UnityEngine.UI.Text m_UserName__Placeholder { get { return mUserName__Placeholder;} }

		[SerializeField] [UGUIElementProperty("UserName/Text")] private UnityEngine.UI.Text mUserName__Text;
		public UnityEngine.UI.Text m_UserName__Text { get { return mUserName__Text;} }

		[SerializeField] [UGUIElementProperty("UserName")] private UnityEngine.UI.InputField mUserName;
		public UnityEngine.UI.InputField m_UserName { get { return mUserName;} }

		[SerializeField] [UGUIElementProperty("Password/Placeholder")] private UnityEngine.UI.Text mPassword__Placeholder;
		public UnityEngine.UI.Text m_Password__Placeholder { get { return mPassword__Placeholder;} }

		[SerializeField] [UGUIElementProperty("Password/Text")] private UnityEngine.UI.Text mPassword__Text;
		public UnityEngine.UI.Text m_Password__Text { get { return mPassword__Text;} }

		[SerializeField] [UGUIElementProperty("Password")] private UnityEngine.UI.InputField mPassword;
		public UnityEngine.UI.InputField m_Password { get { return mPassword;} }

		[SerializeField] [UGUIElementProperty("enter/Text")] private UnityEngine.UI.Text menter__Text;
		public UnityEngine.UI.Text m_enter__Text { get { return menter__Text;} }

		[SerializeField] [UGUIElementProperty("enter")] private UnityEngine.UI.Button menter;
		public UnityEngine.UI.Button m_enter { get { return menter;} }

		[SerializeField] [UGUIElementProperty("ErrorText")] private UnityEngine.UI.Text mErrorText;
		public UnityEngine.UI.Text m_ErrorText { get { return mErrorText;} }

		[SerializeField] [UGUIElementProperty("TitleText")] private UnityEngine.UI.Text mTitleText;
		public UnityEngine.UI.Text m_TitleText { get { return mTitleText;} }

		#endregion

		protected override void InitView()
		{
			this.self = this.gameObject;
		}

	}
}
#endif

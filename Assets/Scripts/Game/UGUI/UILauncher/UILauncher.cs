/** This is an automatically generated class by UGUI. Please do not modify it. **/
#if ENABLE_UI_UGUI
using Cysharp.Threading.Tasks;
using GameFrameX.Entity.Runtime;
using GameFrameX.UI.Runtime;
using GameFrameX.Runtime;
using GameFrameX.UI.UGUI.Runtime;
using UnityEngine;

namespace Unity.Startup
{
	/// <summary>
	/// 代码生成的UI代码UILauncher
	/// </summary>
	public sealed partial class UILauncher : UGUI
	{
		public GameObject self { get; private set; }

		#region Properties
		[SerializeField] [UGUIElementProperty("BgImage")] private UnityEngine.UI.Image mBgImage;
		public UnityEngine.UI.Image m_BgImage { get { return mBgImage;} }

		[SerializeField] [UGUIElementProperty("TipText")] private UnityEngine.UI.Text mTipText;
		public UnityEngine.UI.Text m_TipText { get { return mTipText;} }

		[SerializeField] [UGUIElementProperty("ProgressBar")] private UnityEngine.UI.Slider mProgressBar;
		public UnityEngine.UI.Slider m_ProgressBar { get { return mProgressBar;} }

		[SerializeField] [UGUIElementProperty("ProgressBar/Background")] private UnityEngine.UI.Image mProgressBar__Background;
		public UnityEngine.UI.Image m_ProgressBar__Background { get { return mProgressBar__Background;} }

		[SerializeField] [UGUIElementProperty("ProgressBar/Fill Area")] private UnityEngine.Transform mProgressBar__FillArea;
		public UnityEngine.Transform m_ProgressBar__FillArea { get { return mProgressBar__FillArea;} }

		[SerializeField] [UGUIElementProperty("ProgressBar/Fill Area/Fill")] private UnityEngine.UI.Image mProgressBar__FillArea__Fill;
		public UnityEngine.UI.Image m_ProgressBar__FillArea__Fill { get { return mProgressBar__FillArea__Fill;} }

		[SerializeField] [UGUIElementProperty("upgrade")] private UnityEngine.UI.Image mupgrade;
		public UnityEngine.UI.Image m_upgrade { get { return mupgrade;} }

		[SerializeField] [UGUIElementProperty("upgrade/Image")] private UnityEngine.UI.Image mupgrade__Image;
		public UnityEngine.UI.Image m_upgrade__Image { get { return mupgrade__Image;} }

		[SerializeField] [UGUIElementProperty("upgrade/TextContent")] private UnityEngine.UI.Text mupgrade__TextContent;
		public UnityEngine.UI.Text m_upgrade__TextContent { get { return mupgrade__TextContent;} }

		[SerializeField] [UGUIElementProperty("upgrade/EnterButton")] private UnityEngine.UI.Button mupgrade__EnterButton;
		public UnityEngine.UI.Button m_upgrade__EnterButton { get { return mupgrade__EnterButton;} }

		[SerializeField] [UGUIElementProperty("upgrade/EnterButton/Text")] private UnityEngine.UI.Text mupgrade__EnterButton__Text;
		public UnityEngine.UI.Text m_upgrade__EnterButton__Text { get { return mupgrade__EnterButton__Text;} }

		#endregion

		protected override void InitView()
		{
			this.self = this.gameObject;
		}

	}
}
#endif

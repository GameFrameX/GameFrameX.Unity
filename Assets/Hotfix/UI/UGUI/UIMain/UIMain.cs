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
	/// 代码生成的UI代码UIMain
	/// </summary>
	[DisallowMultipleComponent]
	public sealed partial class UIMain : UGUI
	{
		public GameObject self { get; private set; }

		#region Properties
		[SerializeField] [UGUIElementProperty("BgImage")] private UnityEngine.UI.Image mBgImage;
		public UnityEngine.UI.Image m_BgImage { get { return mBgImage;} }

		[SerializeField] [UGUIElementProperty("bag_button/Text")] private UnityEngine.UI.Text mbag_button__Text;
		public UnityEngine.UI.Text m_bag_button__Text { get { return mbag_button__Text;} }

		[SerializeField] [UGUIElementProperty("bag_button")] private UnityEngine.UI.Button mbag_button;
		public UnityEngine.UI.Button m_bag_button { get { return mbag_button;} }

		[SerializeField] [UGUIElementProperty("player_icon")] private GameFrameX.UI.UGUI.Runtime.UIImage mplayer_icon;
		public GameFrameX.UI.UGUI.Runtime.UIImage m_player_icon { get { return mplayer_icon;} }

		[SerializeField] [UGUIElementProperty("player_name")] private UnityEngine.UI.Text mplayer_name;
		public UnityEngine.UI.Text m_player_name { get { return mplayer_name;} }

		[SerializeField] [UGUIElementProperty("player_level")] private UnityEngine.UI.Text mplayer_level;
		public UnityEngine.UI.Text m_player_level { get { return mplayer_level;} }

		#endregion

		protected override void InitView()
		{
			this.self = this.gameObject;
		}

	}
}
#endif

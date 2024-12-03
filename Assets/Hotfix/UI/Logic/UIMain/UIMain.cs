using FairyGUI;
using GameFrameX.Runtime;
#if ENABLE_UI_UGUI
using GameFrameX.UI.UGUI.Runtime;
using UnityEngine;
#endif
using Hotfix.Manager;
using Hotfix.Proto;

namespace Hotfix.UI
{
    public partial class UIMain
    {
        public override async void OnOpen(object userData)
        {
            base.OnOpen(userData);
#if ENABLE_UI_UGUI
            var assetHandle = await GameApp.Asset.LoadAssetAsync<Sprite>(Utility.Asset.Path.GetCategoryFilePath("Sprites", $"avatar/{PlayerManager.Instance.PlayerInfo.Avatar}"));
            m_player_icon.sprite = assetHandle.GetAssetObject<Sprite>();
#elif ENABLE_UI_FAIRYGUI
            m_player_icon.icon = UIPackage.GetItemURL(FUIPackage.UICommonAvatar, PlayerManager.Instance.PlayerInfo.Avatar.ToString());
#endif
            m_player_name.text = PlayerManager.Instance.PlayerInfo.Name;
            m_player_level.text = "当前等级:" + PlayerManager.Instance.PlayerInfo.Level;
            m_bag_button.onClick.Set(OnBagBtnClick);
        }

        private async void OnBagBtnClick()
        {
            ReqBagInfo reqBagInfo = new ReqBagInfo();
            var respBagInfo = await GameApp.Network.GetNetworkChannel("network").Call<RespBagInfo>(reqBagInfo);
            Log.Debug(respBagInfo);
        }
    }
}
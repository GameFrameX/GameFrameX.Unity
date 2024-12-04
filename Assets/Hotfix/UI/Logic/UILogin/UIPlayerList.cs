using System.Collections.Generic;
using FairyGUI;
using GameFrameX.Runtime;
using GameFrameX.UI.Runtime;
#if ENABLE_UI_FAIRYGUI
using GameFrameX.UI.FairyGUI.Runtime;
#endif
#if ENABLE_UI_UGUI
using GameFrameX.UI.UGUI.Runtime;
#endif
using Hotfix.Manager;
using Hotfix.Proto;
using UnityEngine;

namespace Hotfix.UI
{
    public partial class UIPlayerList
    {
        List<PlayerInfo> playerList = new List<PlayerInfo>();

        public override async void OnOpen(object userData)
        {
            base.OnOpen(userData);
            
            RespLogin respLogin = userData as RespLogin;
            ReqPlayerList req = new ReqPlayerList();
            if (respLogin != null)
            {
                req.Id = respLogin.Id;
            }

#if ENABLE_UI_FAIRYGUI
            this.m_login_button.onClick.Set(OnLoginButtonClick);
            this.m_player_list.itemRenderer = ItemRenderer;
            this.m_player_list.onClickItem.Set(OnPlayerListItemClick);
#elif ENABLE_UI_UGUI
            m_right_Panel.gameObject.SetActive(false);
            m_right_Panel__login_button.onClick.Set(OnLoginButtonClick);
#endif
            var resp = await GameApp.Network.GetNetworkChannel("network").Call<RespPlayerList>(req);
            playerList = resp.PlayerList;
#if ENABLE_UI_UGUI
            var uiPlayerListItemAssetHandle = await GameApp.Asset.LoadAssetAsync<GameObject>(Utility.Asset.Path.GetUIPath($"{nameof(UILogin)}/{nameof(UIPlayerListItem)}"));
            foreach (var playerInfo in playerList)
            {
                var item = uiPlayerListItemAssetHandle.InstantiateSync(m_left_Panel__ScrollView__Viewport__Content);
                var uiPlayerListItem = item.GetComponent<UIPlayerListItem>();
                uiPlayerListItem.m_level_text.text = "当前等级:" + playerInfo.Level.ToString();
                uiPlayerListItem.m_name_text.text = playerInfo.Name;
                uiPlayerListItem.m_click_Button.onClick.Set(OnPlayerListItemClick, playerInfo);
                var assetHandle = await GameApp.Asset.LoadAssetAsync<Sprite>(Utility.Asset.Path.GetCategoryFilePath("Sprites", $"avatar/{PlayerManager.Instance.PlayerInfo.Avatar}"));
                uiPlayerListItem.m_icon.sprite = assetHandle.GetAssetObject<Sprite>();
            }
#endif
#if ENABLE_UI_FAIRYGUI
            this.m_player_list.numItems = playerList.Count;
#endif
        }

        private async void OnLoginButtonClick()
        {
            ReqPlayerLogin reqPlayerLogin = new ReqPlayerLogin();
            reqPlayerLogin.Id = m_SelectedPlayerInfo.Id;
            var respPlayerLogin = await GameApp.Network.GetNetworkChannel("network").Call<RespPlayerLogin>(reqPlayerLogin);
            PlayerManager.Instance.PlayerInfo = respPlayerLogin.PlayerInfo;
            await GameApp.UI.OpenFullScreenUIFormAsync<UIMain>(Utility.Asset.Path.GetUIPath(nameof(UIMain)), UIGroupConstants.Floor.Name);
            GameApp.UI.CloseUIForm(this);
        }

        PlayerInfo m_SelectedPlayerInfo;
#if ENABLE_UI_FAIRYGUI
        private void OnPlayerListItemClick(EventContext context)
        {
            if ((context.data as GComponent).dataSource is PlayerInfo playerInfo)
            {
                m_SelectedPlayerInfo = playerInfo;

                this.m_selected_icon.icon = UIPackage.GetItemURL(FUIPackage.UICommonAvatar, playerInfo.Avatar.ToString());
                this.m_selected_name.text = playerInfo.Name;
                this.m_selected_level.text = "当前等级:" + playerInfo.Level;
                this.m_IsSelected.SetSelectedIndex(1);

            }
        }
#elif ENABLE_UI_UGUI
        private async void OnPlayerListItemClick(object userData)
        {
            m_SelectedPlayerInfo = (PlayerInfo)userData;
            var assetHandle = await GameApp.Asset.LoadAssetAsync<Sprite>(Utility.Asset.Path.GetCategoryFilePath("Sprites", $"avatar/{PlayerManager.Instance.PlayerInfo.Avatar}"));

            m_right_Panel__selected_icon.sprite = assetHandle.GetAssetObject<Sprite>();
            m_right_Panel__selected_name.text = m_SelectedPlayerInfo.Name;
            m_right_Panel__selected_level.text = "当前等级:" + m_SelectedPlayerInfo.Level;
            m_right_Panel.gameObject.SetActive(true);
        }
#endif
        private void ItemRenderer(int index, GObject item)
        {
            var playerInfo = playerList[index];
#if ENABLE_UI_FAIRYGUI
            var uiPlayerListItem = UIPlayerListItem.GetFormPool(item);
            uiPlayerListItem.m_level_text.text = "当前等级:" + playerInfo.Level.ToString();
            uiPlayerListItem.m_name_text.text = playerInfo.Name;
            uiPlayerListItem.m_icon.icon = UIPackage.GetItemURL(FUIPackage.UICommonAvatar, playerInfo.Avatar.ToString());
#endif

            item.dataSource = playerInfo;
        }
    }
}
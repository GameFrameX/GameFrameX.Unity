using System.Collections.Generic;
using FairyGUI;
using GameFrameX.Runtime;
using GameFrameX.UI.Runtime;
using Hotfix.Manager;
using Hotfix.Proto;

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

            this.m_login_button.onClick.Set(OnLoginButtonClick);
            this.m_player_list.itemRenderer = ItemRenderer;
            this.m_player_list.onClickItem.Set(OnPlayerListItemClick);
            var resp = await GameApp.Network.GetNetworkChannel("network").Call<RespPlayerList>(req);
            playerList = resp.PlayerList;
            this.m_player_list.numItems = playerList.Count;
        }

        private async void OnLoginButtonClick()
        {
            ReqPlayerLogin reqPlayerLogin = new ReqPlayerLogin();
            reqPlayerLogin.Id = m_SelectedPlayerInfo.Id;
            var respPlayerLogin = await GameApp.Network.GetNetworkChannel("network").Call<RespPlayerLogin>(reqPlayerLogin);
            PlayerManager.Instance.PlayerInfo = respPlayerLogin.PlayerInfo;
            await GameApp.UI.OpenUIFormAsync<UIMain>(Utility.Asset.Path.GetUIPackagePath(FUIPackage.UIMain), UIGroupConstants.Floor.Name);
            GameApp.UI.CloseUIForm(this);
        }

        PlayerInfo m_SelectedPlayerInfo;

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

        private void ItemRenderer(int index, GObject item)
        {
            var playerInfo = playerList[index];
            var uiPlayerListItem = UIPlayerListItem.GetFormPool(item);
            uiPlayerListItem.m_level_text.text = "当前等级:" + playerInfo.Level.ToString();
            uiPlayerListItem.m_name_text.text = playerInfo.Name;
            uiPlayerListItem.m_icon.icon = UIPackage.GetItemURL(FUIPackage.UICommonAvatar, playerInfo.Avatar.ToString());
            item.dataSource = playerInfo;
        }
    }
}
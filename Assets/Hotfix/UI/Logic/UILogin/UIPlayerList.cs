using System.Collections.Generic;
using FairyGUI;
using GameFrameX.Runtime;
using Hotfix.Proto;

namespace Hotfix.UI
{
    public partial class UIPlayerList
    {
        List<PlayerInfo> playerList = new List<PlayerInfo>();

        protected override async void OnShow()
        {
            base.OnShow();
            RespLogin respLogin = UserData as RespLogin;
            ReqPlayerList req = new ReqPlayerList();
            if (respLogin != null)
            {
                req.Id = respLogin.Id;
            }

            this.m_player_list.itemRenderer = ItemRenderer;
            this.m_player_list.onClickItem.Set(OnPlayerListItemClick);
            var resp = await GameApp.Network.GetNetworkChannel("network").Call<RespPlayerList>(req);
            playerList = resp.PlayerList;
            this.m_player_list.numItems = playerList.Count;
        }

        private async void OnPlayerListItemClick(EventContext context)
        {
            if ((context.data as GComponent).dataSource is PlayerInfo playerInfo)
            {
                Log.Info(playerInfo);
                ReqPlayerLogin reqPlayerLogin = new ReqPlayerLogin();
                reqPlayerLogin.Id = playerInfo.Id;
                var respPlayerLogin = await GameApp.Network.GetNetworkChannel("network").Call<RespPlayerLogin>(reqPlayerLogin);
                // GameApp.FUI.AddAsync(UIPlayerCreate.CreateInstance, Utility.Asset.Path.GetUIPackagePath(FUIPackage.UILogin), UILayer.Floor, false, playerInfo);
            }
        }

        private void ItemRenderer(int index, GObject item)
        {
            var playerInfo = playerList[index];
            var uiPlayerListItem = UIPlayerListItem.GetFormPool(item);
            uiPlayerListItem.m_level_text.text = playerInfo.Level.ToString();
            uiPlayerListItem.m_name_text.text = playerInfo.Name;
            item.dataSource = playerInfo;
        }
    }
}
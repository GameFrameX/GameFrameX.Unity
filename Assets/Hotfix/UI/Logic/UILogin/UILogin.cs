using System.Net;
using GameFrameX;
using GameFrameX.Event.Runtime;
using GameFrameX.UI.FairyGUI.Runtime;
using GameFrameX.Network.Runtime;
using GameFrameX.Runtime;
using GameFrameX.UI.Runtime;
using Hotfix.Config.item;
using Hotfix.Network;
using Hotfix.Proto;
using UnityEngine;

namespace Hotfix.UI
{
    public partial class UILogin
    {
        private static INetworkChannel networkChannel;
        public static string serverIp = "127.0.0.1";
        public static int serverPort = 29100;

        public override void OnOpen(object userData)
        {
            m_enter.onClick.Add(OnLoginClick);
            base.OnOpen(userData);
        }

        private void OnLoginClick()
        {
            if (networkChannel != null && networkChannel.Connected)
            {
                Login();
                return;
            }

            if (networkChannel != null && GameApp.Network.HasNetworkChannel("network") && !networkChannel.Connected)
            {
                GameApp.Network.DestroyNetworkChannel("network");
            }

            networkChannel = GameApp.Network.CreateNetworkChannel("network", new DefaultNetworkChannelHelper());
            // 注册心跳消息
            DefaultPacketHeartBeatHandler packetSendHeaderHandler = new DefaultPacketHeartBeatHandler();
            networkChannel.RegisterHeartBeatHandler(packetSendHeaderHandler);
            networkChannel.Connect(IPAddress.Parse(serverIp), serverPort);
            GameApp.Event.CheckSubscribe(NetworkConnectedEventArgs.EventId, OnNetworkConnected);
            GameApp.Event.CheckSubscribe(NetworkClosedEventArgs.EventId, OnNetworkClosed);
        }

        private async void Login()
        {
            var item = GameApp.Config.GetConfig<TbItem>().Get(1);
            Log.Info(item);
            if (m_UserName.text.IsNullOrWhiteSpace() || m_Password.text.IsNullOrWhiteSpace())
            {
                m_ErrorText.text = "用户名或密码不能为空";
                return;
            }


            var req = new ReqLogin
            {
                SdkType = 0,
                SdkToken = "",
                UserName = m_UserName.text,
                Password = m_Password.text,
                Device = SystemInfo.deviceUniqueIdentifier
            };
            req.Platform = PathHelper.GetPlatformName;

            RespLogin respLogin = await networkChannel.Call<RespLogin>(req);
            Log.Info(respLogin);
            ReqPlayerList reqPlayerList = new ReqPlayerList();

            reqPlayerList.Id = respLogin.Id;

            var respPlayerList = await networkChannel.Call<RespPlayerList>(reqPlayerList);
            if (respPlayerList.PlayerList.Count > 0)
            {
                await GameApp.UI.OpenUIFormAsync<UIPlayerList>(Utility.Asset.Path.GetUIPath(FUIPackage.UILogin), UIGroupConstants.Floor.Name, respLogin);
            }
            else
            {
                await GameApp.UI.OpenUIFormAsync<UIPlayerCreate>(Utility.Asset.Path.GetUIPath(FUIPackage.UILogin), UIGroupConstants.Floor.Name, respLogin);
            }

            await GameApp.UI.OpenUIFormAsync<UIMain>(Utility.Asset.Path.GetUIPath(FUIPackage.UIMain), UIGroupConstants.Floor.Name);
            GameApp.UI.CloseUIForm(this);
        }

        private static void OnNetworkClosed(object sender, GameEventArgs e)
        {
            Log.Info(nameof(OnNetworkClosed));
        }

        private void OnNetworkConnected(object sender, GameEventArgs e)
        {
            Login();
            Log.Info(nameof(OnNetworkConnected));
        }
    }
}
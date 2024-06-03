using System.Net;
using GameFrameX;
using GameFrameX.Event.Runtime;
using GameFrameX.FairyGUI.Runtime;
using GameFrameX.Network.Runtime;
using GameFrameX.Runtime;
using Hotfix.Config.item;
using Hotfix.Network;
using Hotfix.Proto;
using UnityEngine;

namespace Hotfix.UI
{
    public partial class UILogin
    {
        private static INetworkChannel networkChannel;

        // public static string serverIp = "gameframex.alianblank.com";
        public static string serverIp = "47.106.128.180";

        // public static string serverIp = "127.0.0.1";
        public static int serverPort = 21100;

        protected override void OnShow()
        {
            base.OnShow();
            m_enter.onClick.Add(OnLoginClick);
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
            networkChannel.RegisterHandler(packetSendHeaderHandler);
            networkChannel.Connect(IPAddress.Parse(serverIp), serverPort);
            GameApp.Event.Subscribe(NetworkConnectedEventArgs.EventId, OnNetworkConnected);
            GameApp.Event.Subscribe(NetworkClosedEventArgs.EventId, OnNetworkClosed);
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
                await GameApp.FUI.AddAsync(UIPlayerList.CreateInstance, Utility.Asset.Path.GetUIPackagePath(FUIPackage.UILogin), UILayer.Floor, true, respLogin);
            }
            else
            {
                await GameApp.FUI.AddAsync(UIPlayerCreate.CreateInstance, Utility.Asset.Path.GetUIPackagePath(FUIPackage.UILogin), UILayer.Floor, true, respLogin);
            }

            // await GameApp.FUI.AddAsync(UIMain.CreateInstance, Utility.Asset.Path.GetUIPackagePath(FUIPackage.UIMain), UILayer.Floor);
            GameApp.FUI.Remove(Name, UILayer.Floor);
        }

        private static void OnNetworkClosed(object sender, GameEventArgs e)
        {
            Log.Info(nameof(OnNetworkClosed));
        }

        private static void OnNetworkConnected(object sender, GameEventArgs e)
        {
            Log.Info(nameof(OnNetworkConnected));
        }
    }
}
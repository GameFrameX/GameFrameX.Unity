using System.Net;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GameFrameX;
using GameFrameX.Event.Runtime;
using GameFrameX.FairyGUI.Runtime;
using GameFrameX.Network.Runtime;
using Hotfix.Proto;
using SimpleJSON;
using UnityEngine;
using GameFrameX.Runtime;
using Hotfix.Config;
using Hotfix.Config.item;
using Hotfix.Config.test;
using Hotfix.Network;
using Hotfix.UI;

namespace Hotfix
{
    public static class HotfixLauncher
    {
        public static string serverIp = "127.0.0.1";
        public static int serverPort = 21100;
        private static INetworkChannel networkChannel;

        public static void Main()
        {
            Log.Info("Hello World HybridCLR");
            ProtoMessageIdHandler.Init(HotfixProtoHandler.CurrentAssembly);
            LoadConfig();
            LoadUI();
        }

        private static async void LoadUI()
        {
            var uiLogin = await GameApp.FUI.AddAsync<UILogin>(UILogin.CreateInstance, Utility.Asset.Path.GetUIPackagePath(FUIPackage.UILogin), UILayer.Floor);
            uiLogin.m_enter.onClick.Add(() =>
            {
                if (networkChannel != null && networkChannel.Connected)
                {
                    NetTest();
                    return;
                }

                if (networkChannel != null && GameApp.Network.HasNetworkChannel("network") && !networkChannel.Connected)
                {
                    GameApp.Network.DestroyNetworkChannel("network");
                }

                networkChannel = GameApp.Network.CreateNetworkChannel("network", new DefaultNetworkChannelHelper());
                // NetManager.Singleton.Init();
                // 注册心跳消息
                DefaultPacketHeartBeatHandler packetSendHeaderHandler = new DefaultPacketHeartBeatHandler();
                networkChannel.RegisterHandler(packetSendHeaderHandler);
                networkChannel.Connect(IPAddress.Parse(serverIp), serverPort);
                networkChannel.SetDefaultHandler((sender, e) => { Log.Info("Receive: " + e); });
                GameApp.Event.Subscribe(NetworkConnectedEventArgs.EventId, OnNetworkConnected);
                GameApp.Event.Subscribe(NetworkClosedEventArgs.EventId, OnNetworkClosed);
            });
        }

        private static void OnNetworkClosed(object sender, GameEventArgs e)
        {
            Log.Info(nameof(OnNetworkClosed));
        }

        private static void OnNetworkConnected(object sender, GameEventArgs e)
        {
            Log.Info(nameof(OnNetworkConnected));
            NetTest();
        }


        private static async void NetTest()
        {
            await UniTask.Delay(1);


            var req = new ReqLogin
            {
                SdkType = 0,
                SdkToken = "",
                UserName = "Blank",
                Password = "123456",
                Device = SystemInfo.deviceUniqueIdentifier
            };
            req.Platform = PathHelper.GetPlatformName;

            RespLogin respLogin = await networkChannel.Call<RespLogin>(req);
            Log.Info(respLogin);
        }

        static async void LoadConfig()
        {
            var tablesComponent = new TablesComponent();
            tablesComponent.Init(GameApp.Config);
            await tablesComponent.LoadAsync(ConfigLoader);
            var item = GameApp.Config.GetConfig<TbItem>().Get(1);
            Log.Info(item);
        }

        private static async Task<JSONNode> ConfigLoader(string file)
        {
            var assetHandle = await GameApp.Asset.LoadAssetAsync<TextAsset>(Utility.Asset.Path.GetConfigPath(file, Utility.Const.FileNameSuffix.Json));

            return JSON.Parse(assetHandle.GetAssetObject<TextAsset>().text);
        }
    }
}
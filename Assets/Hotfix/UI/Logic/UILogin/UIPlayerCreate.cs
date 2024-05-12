using GameFrameX;
using GameFrameX.FairyGUI.Runtime;
using GameFrameX.Runtime;
using Hotfix.Proto;

namespace Hotfix.UI
{
    public partial class UIPlayerCreate
    {
        ReqPlayerCreate req = new ReqPlayerCreate();

        protected override async void OnShow()
        {
            base.OnShow();
            RespLogin respLogin = UserData as RespLogin;
            this.m_enter.onClick.Set(OnCreateButtonClick);
            req.Id = respLogin.Id;
        }

        private async void OnCreateButtonClick()
        {
            if (m_UserName.text.IsNullOrWhiteSpace())
            {
                m_ErrorText.text = "角色名不能为空";
                return;
            }

            req.Name = m_UserName.text;
            var resp = await GameApp.Network.GetNetworkChannel("network").Call<RespPlayerCreate>(req);
            if (resp.PlayerInfo != null)
            {
                Log.Info("创建角色成功");
            }

            await GameApp.FUI.AddAsync(UIPlayerList.CreateInstance, Utility.Asset.Path.GetUIPackagePath(FUIPackage.UILogin), UILayer.Floor, false, UserData);
            GameApp.FUI.Remove(UIResName, UILayer.Floor);
        }
    }
}
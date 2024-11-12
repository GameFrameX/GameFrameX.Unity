using GameFrameX;
using GameFrameX.UI.FairyGUI.Runtime;
using GameFrameX.Runtime;
using GameFrameX.UI.Runtime;
using Hotfix.Proto;

namespace Hotfix.UI
{
    public partial class UIPlayerCreate
    {
        ReqPlayerCreate req;

        public override void OnOpen(object userData)
        {
            req = new ReqPlayerCreate();
            base.OnOpen(userData);

            RespLogin respLogin = userData as RespLogin;
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

            await GameApp.UI.OpenUIFormAsync<UIPlayerList>( Utility.Asset.Path.GetUIPackagePath(FUIPackage.UILogin),UIGroupConstants.Floor.Name, UserData);
            GameApp.UI.CloseUIForm(this);
        }
    }
}
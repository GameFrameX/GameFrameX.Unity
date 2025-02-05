using System.Collections.Generic;
using GameFrameX;
using GameFrameX.GlobalConfig.Runtime;
#if ENABLE_UI_FAIRYGUI
using GameFrameX.UI.FairyGUI.Runtime;
#endif
using GameFrameX.Runtime;
using GameFrameX.UI.Runtime;
using Hotfix.Manager;
#if ENABLE_UI_UGUI
using GameFrameX.UI.UGUI.Runtime;
#endif
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

            #region 创建角色

            var respPlayerCreate = await GameApp.Web.Post<RespPlayerCreate>($"http://127.0.0.1:28080/game/api/{nameof(ReqPlayerCreate).ConvertToSnakeCase()}", req);

            if (respPlayerCreate.ErrorCode > 0)
            {
                Log.Error("登录失败，错误信息:" + respPlayerCreate.ErrorCode);
                return;
            }

            if (respPlayerCreate.PlayerInfo != null)
            {
                Log.Info("创建角色成功");
            }

            #endregion

            #region 获取角色列表

            ReqPlayerList reqPlayerList = new ReqPlayerList();

            reqPlayerList.Id = req.Id;
            var respPlayerList = await GameApp.Web.Post<RespPlayerList>($"http://127.0.0.1:28080/game/api/{nameof(ReqPlayerList).ConvertToSnakeCase()}", reqPlayerList);

            if (respPlayerList.ErrorCode > 0)
            {
                Log.Error("登录失败，错误信息:" + respPlayerList.ErrorCode);
                return;
            }

            AccountManager.Instance.PlayerList = respPlayerList.PlayerList;

            #endregion

            await GameApp.UI.OpenUIFormAsync<UIPlayerList>(Utility.Asset.Path.GetUIPath(nameof(UILogin)), UIGroupConstants.Floor.Name, UserData, true);
            GameApp.UI.CloseUIForm(this);
        }
    }
}
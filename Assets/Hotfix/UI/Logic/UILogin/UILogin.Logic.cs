using System.Collections.Generic;
using System.Net;
using GameFrameX;
using GameFrameX.Event.Runtime;
using GameFrameX.GlobalConfig.Runtime;
#if ENABLE_UI_FAIRYGUI
using GameFrameX.UI.FairyGUI.Runtime;
#endif
using GameFrameX.Network.Runtime;
using GameFrameX.Runtime;
using GameFrameX.UI.Runtime;
#if ENABLE_UI_UGUI
using GameFrameX.UI.UGUI.Runtime;
#endif
using Hotfix.Config.item;
using Hotfix.Manager;
using Hotfix.Network;
using Hotfix.Proto;
using UnityEngine;

namespace Hotfix.UI
{
    public partial class UILogin
    {


        public override void OnOpen(object userData)
        {
            m_enter.onClick.Set(OnLoginClick);
            base.OnOpen(userData);
        }

        private void OnLoginClick()
        {
            Login();
            return;

        }

        private async void Login()
        {
            if (m_UserName.text.IsNullOrWhiteSpace() || m_Password.text.IsNullOrWhiteSpace())
            {
                m_ErrorText.text = "用户名或密码不能为空";
                return;
            }


            #region 账号登录

            var req = new ReqLogin
            {
                SdkType = 0,
                SdkToken = "",
                UserName = m_UserName.text,
                Password = m_Password.text,
                Device = SystemInfo.deviceUniqueIdentifier
            };
            req.Platform = PathHelper.GetPlatformName;

            var respLoginWebResult = await GameApp.Web.PostToString($"http://127.0.0.1:28080/game/api/{nameof(ReqLogin)}", Utility.Json.ToObject<Dictionary<string, object>>(Utility.Json.ToJson(req)));
            HttpJsonResult respLoginHttpJsonResult = Utility.Json.ToObject<HttpJsonResult>(respLoginWebResult.Result);
            if (respLoginHttpJsonResult.Code > 0)
            {
                Log.Error("登录失败，错误信息:" + respLoginHttpJsonResult.Message);
                return;
            }

            Log.Info(respLoginWebResult.Result);
            Log.Info(respLoginHttpJsonResult.Data);
            var respLogin = Utility.Json.ToObject<RespLogin>(respLoginHttpJsonResult.Data);

            #endregion

            #region 获取角色列表

            ReqPlayerList reqPlayerList = new ReqPlayerList();

            reqPlayerList.Id = respLogin.Id;
            var respPlayerListWebResult = await GameApp.Web.PostToString($"http://127.0.0.1:28080/game/api/{nameof(ReqPlayerList)}", Utility.Json.ToObject<Dictionary<string, object>>(Utility.Json.ToJson(reqPlayerList)));
            HttpJsonResult respPlayerListHttpJsonResult = Utility.Json.ToObject<HttpJsonResult>(respPlayerListWebResult.Result);
            if (respPlayerListHttpJsonResult.Code > 0)
            {
                Log.Error("登录失败，错误信息:" + respPlayerListHttpJsonResult.Message);
                return;
            }

            Log.Info(respPlayerListWebResult.Result);
            Log.Info(respPlayerListHttpJsonResult.Data);

            var respPlayerList = Utility.Json.ToObject<RespPlayerList>(respPlayerListHttpJsonResult.Data);
            AccountManager.Instance.PlayerList = respPlayerList.PlayerList;

            #endregion

            if (respPlayerList.PlayerList.Count > 0)
            {
                await GameApp.UI.OpenUIFormAsync<UIPlayerList>(Utility.Asset.Path.GetUIPath(nameof(UILogin)), UIGroupConstants.Floor.Name, respLogin, true);
            }
            else
            {
                await GameApp.UI.OpenUIFormAsync<UIPlayerCreate>(Utility.Asset.Path.GetUIPath(nameof(UILogin)), UIGroupConstants.Floor.Name, respLogin, true);
            }

            GameApp.UI.CloseUIForm(this);
        }
    }
}
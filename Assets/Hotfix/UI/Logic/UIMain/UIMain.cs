using GameFrameX.Runtime;
using Hotfix.Proto;

namespace Hotfix.UI
{
    public partial class UIMain
    {
        protected override void OnShow()
        {
            base.OnShow();
            this.m_bag_button.onClick.Set(OnBagBtnClick);
        }

        private async void OnBagBtnClick()
        {
            ReqBagInfo reqBagInfo = new ReqBagInfo();
            var respBagInfo = await GameApp.Network.GetNetworkChannel("network").Call<RespBagInfo>(reqBagInfo);
            Log.Debug(respBagInfo);
        }
    }
}
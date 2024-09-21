using GameFrameX.Network.Runtime;
using GameFrameX.Runtime;
using Hotfix.Proto;

namespace Hotfix.Manager
{
    /// <summary>
    /// 背包 管理器
    /// </summary>
    public sealed class BagManager : GameFrameworkSingleton<BagManager>, IMessageHandler
    {
        /// <summary>
        /// 监听道具变化通知
        /// </summary>
        /// <param name="msg"></param>
        [MessageHandler(typeof(NotifyBagInfoChanged), nameof(NotifyBagInfoChanged))]
        private void NotifyBagInfoChanged(NotifyBagInfoChanged msg)
        {
            Log.Debug(msg);
        }

        /// <summary>
        /// 由于是单例对象。所以在初始化的时候自动调用一次注册消息
        /// </summary>
        public BagManager()
        {
            Register();
        }
        
        /// <summary>
        /// 注册消息。请勿多次调用
        /// </summary>
        public void Register()
        {
            ProtoMessageHandler.Add(this);
        }
    }
}
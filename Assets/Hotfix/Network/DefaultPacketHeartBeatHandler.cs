using GameFrameX.Network.Runtime;
using GameFrameX.Runtime;
using Hotfix.Proto;

namespace Hotfix.Network
{
    public sealed class DefaultPacketHeartBeatHandler : BasePacketHeartBeatHandler
    {
        private readonly ReqHeartBeat _reqHeartBeat;

        public DefaultPacketHeartBeatHandler()
        {
            _reqHeartBeat = new ReqHeartBeat();
        }

        public override MessageObject Handler()
        {
            _reqHeartBeat.Timestamp = GameTimeHelper.UnixTimeMilliseconds();
            _reqHeartBeat.UpdateUniqueId();
            return _reqHeartBeat;
        }
    }
}
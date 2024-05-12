using System;
using ProtoBuf;
using System.Collections.Generic;
using GameFrameX.Network.Runtime;

namespace Hotfix.Proto
{
	/// <summary>
	/// 请求心跳
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(1)]
	public partial class ReqHeartBeat : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 时间戳
		/// </summary>
		[ProtoMember(1)]
		public long Timestamp { get; set; }

	}

	/// <summary>
	/// 返回心跳
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(1)]
	public partial class RespHeartBeat : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 时间戳
		/// </summary>
		[ProtoMember(1)]
		public long Timestamp { get; set; }

	}

}

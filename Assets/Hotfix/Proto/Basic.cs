using System;
using ProtoBuf;
using System.Collections.Generic;
using GameFrameX.Network;

namespace Hotfix.Proto
{
	/// <summary>
	/// 请求心跳
	/// </summary>
	[MessageTypeHandler(1)]
	[ProtoContract]
	public partial class ReqHeartBeat : MessageObject, IRequestMessage
	{
		/// <summary>
		///  时间戳
		/// </summary>
		[ProtoMember(1)]
		public long Timestamp { get; set; }

	}

	/// <summary>
	/// 返回心跳
	/// </summary>
	[MessageTypeHandler(1)]
	[ProtoContract]
	public partial class RespHeartBeat : MessageObject, IResponseMessage
	{
		/// <summary>
		///  时间戳
		/// </summary>
		[ProtoMember(1)]
		public long Timestamp { get; set; }

	}

	/// <summary>
	/// 请求心跳
	/// </summary>
	
	[ProtoContract]
	public partial class ReqActorHeartBeat : MessageObject
	{
		/// <summary>
		///  时间戳
		/// </summary>
		[ProtoMember(1)]
		public long Timestamp { get; set; }

	}

	/// <summary>
	/// 返回心跳
	/// </summary>
	
	[ProtoContract]
	public partial class RespActorHeartBeat : MessageObject
	{
		/// <summary>
		///  时间戳
		/// </summary>
		[ProtoMember(1)]
		public long Timestamp { get; set; }

	}

}

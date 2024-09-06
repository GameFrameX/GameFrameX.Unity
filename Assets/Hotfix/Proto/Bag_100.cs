using System;
using ProtoBuf;
using System.Collections.Generic;
using GameFrameX.Network.Runtime;

namespace Hotfix.Proto
{
	/// <summary>
	/// 
	/// </summary>
	public enum BagType
	{
		/// <summary>
		/// 默认
		/// </summary>
		Default = 0, 
		/// <summary>
		/// 宠物
		/// </summary>
		Pet = 1, 
	}

	/// <summary>
	/// 请求背包数据
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(6553610)]
	public sealed class ReqBagInfo : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 背包类型
		/// </summary>
		[ProtoMember(1)]
		public BagType BagType { get; set; }

	}

	/// <summary>
	/// 返回背包数据
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(6553611)]
	public sealed class RespBagInfo : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 
		/// </summary>
		[ProtoMember(1)]
		[ProtoMap(DisableMap = true)]
		public Dictionary<int, long> ItemDic { get; set; } = new Dictionary<int, long>();

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(888)]
		public int ErrorCode { get; set; }

	}

	/// <summary>
	/// 通知背包数据变化
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(6553612)]
	public sealed class NotifyBagInfoChanged : MessageObject, INotifyMessage
	{
		/// <summary>
		/// 变化的道具，key:道具id，value:数量
		/// </summary>
		[ProtoMember(1)]
		[ProtoMap(DisableMap = true)]
		public Dictionary<int, long> ItemDic { get; set; } = new Dictionary<int, long>();

	}

	/// <summary>
	/// 请求合成宠物
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(6553613)]
	public sealed class ReqComposePet : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 碎片id
		/// </summary>
		[ProtoMember(1)]
		public int FragmentId { get; set; }

	}

	/// <summary>
	/// 返回合成宠物
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(6553614)]
	public sealed class RespComposePet : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 合成宠物的Id
		/// </summary>
		[ProtoMember(1)]
		public int PetId { get; set; }

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(888)]
		public int ErrorCode { get; set; }

	}

	/// <summary>
	/// 请求使用道具
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(6553615)]
	public sealed class ReqUseItem : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 道具id
		/// </summary>
		[ProtoMember(1)]
		public int ItemId { get; set; }

	}

	/// <summary>
	/// 出售道具
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(6553616)]
	public sealed class ReqSellItem : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 道具id
		/// </summary>
		[ProtoMember(1)]
		public int ItemId { get; set; }

	}

	/// <summary>
	/// 出售道具
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(6553617)]
	public sealed class RespItemChange : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 变化的道具
		/// </summary>
		[ProtoMember(1)]
		[ProtoMap(DisableMap = true)]
		public Dictionary<int, long> ItemDic { get; set; } = new Dictionary<int, long>();

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(888)]
		public int ErrorCode { get; set; }

	}

}

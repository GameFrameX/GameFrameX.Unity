using System;
using ProtoBuf;
using System.Collections.Generic;
using GameFrameX.Network.Runtime;

namespace Hotfix.Proto
{
	/// <summary>
	/// 请求背包数据
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(100)]
	public partial class ReqBagInfo : MessageObject, IRequestMessage
	{
	}

	/// <summary>
	/// 返回背包数据
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(100)]
	public partial class RespBagInfo : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 
		/// </summary>
		[ProtoMember(1)]
		public Dictionary<int, long> ItemDic { get; set; }

	}

	/// <summary>
	/// 请求合成宠物
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(101)]
	public partial class ReqComposePet : MessageObject, IRequestMessage
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
	[MessageTypeHandler(101)]
	public partial class RespComposePet : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 合成宠物的Id
		/// </summary>
		[ProtoMember(1)]
		public int PetId { get; set; }

	}

	/// <summary>
	/// 请求使用道具
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(102)]
	public partial class ReqUseItem : MessageObject, IRequestMessage
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
	[MessageTypeHandler(103)]
	public partial class ReqSellItem : MessageObject, IRequestMessage
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
	[MessageTypeHandler(104)]
	public partial class RespItemChange : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 变化的道具
		/// </summary>
		[ProtoMember(1)]
		public Dictionary<int, long> ItemDic { get; set; }

	}

}

using System;
using ProtoBuf;
using System.Collections.Generic;
using GameFrameX.Network.Runtime;

namespace Hotfix.Proto
{
	/// <summary>
	/// 获取商品列表
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(13107201)]
	public sealed class C2S_GetShopItemList : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 
		/// </summary>
		[ProtoMember(90)]
		public int RpcId { get; set; }

		/// <summary>
		/// 商店类型
		/// </summary>
		[ProtoMember(1)]
		public int shopType { get; set; }

	}

	/// <summary>
	/// 
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(13107202)]
	public sealed class S2C_GetShopItemList : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 
		/// </summary>
		[ProtoMember(90)]
		public int RpcId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[ProtoMember(91)]
		public int Error { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[ProtoMember(92)]
		public string Message { get; set; }

		/// <summary>
		/// 商店类型
		/// </summary>
		[ProtoMember(1)]
		public int shopType { get; set; }

		/// <summary>
		/// 分页号（分页号如果为-1，则将该商店中的所有物品都推送给前端）
		/// </summary>
		[ProtoMember(2)]
		public int pageId { get; set; }

		/// <summary>
		/// 免费总刷新次数
		/// </summary>
		[ProtoMember(4)]
		public int maxFreeRfsTimes { get; set; }

		/// <summary>
		/// 货币总刷新次数
		/// </summary>
		[ProtoMember(5)]
		public int maxCurrencyRfsTimes { get; set; }

		/// <summary>
		/// 广告总刷新次数
		/// </summary>
		[ProtoMember(6)]
		public int maxAdsRfsTimes { get; set; }

		/// <summary>
		/// 下次刷新时间
		/// </summary>
		[ProtoMember(8)]
		public long nextRefreshTime { get; set; }

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(888)]
		public int ErrorCode { get; set; }

	}

	/// <summary>
	/// 获取限购列表
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(13107203)]
	public sealed class C2S_GetLimitList : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 
		/// </summary>
		[ProtoMember(90)]
		public int RpcId { get; set; }

		/// <summary>
		/// 商店类型
		/// </summary>
		[ProtoMember(1)]
		public int shopType { get; set; }

		/// <summary>
		/// 玩家id
		/// </summary>
		[ProtoMember(2)]
		public long playerId { get; set; }

		/// <summary>
		/// 分页号（分页号如果为-1，则将该商店中的所有限购物品都推送给前端）
		/// </summary>
		[ProtoMember(3)]
		public int pageId { get; set; }

	}

	/// <summary>
	/// 存储数据
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(13107204)]
	public sealed class S2C_GetLimitList : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 
		/// </summary>
		[ProtoMember(90)]
		public int RpcId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[ProtoMember(91)]
		public int Error { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[ProtoMember(92)]
		public string Message { get; set; }

		/// <summary>
		/// 商店类型
		/// </summary>
		[ProtoMember(1)]
		public int shopType { get; set; }

		/// <summary>
		/// 玩家id
		/// </summary>
		[ProtoMember(2)]
		public long playerId { get; set; }

		/// <summary>
		/// 免费刷新次数
		/// </summary>
		[ProtoMember(4)]
		public int freeRfsTimes { get; set; }

		/// <summary>
		/// 货币刷新次数
		/// </summary>
		[ProtoMember(5)]
		public int currencyRfsTimes { get; set; }

		/// <summary>
		/// 广告刷新次数
		/// </summary>
		[ProtoMember(6)]
		public int adsRfsTimes { get; set; }

		/// <summary>
		/// 刷新时间标识 判断当前周期内是否刷新过 now - rfstime > 24
		/// </summary>
		[ProtoMember(7)]
		public long refreshedTime { get; set; }

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(888)]
		public int ErrorCode { get; set; }

	}

	/// <summary>
	/// 获取商品列表
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(13107205)]
	public sealed class C2S_GetShopPaymentList : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 
		/// </summary>
		[ProtoMember(90)]
		public int RpcId { get; set; }

	}

	/// <summary>
	/// 
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(13107206)]
	public sealed class S2C_GetShopPaymentList : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 
		/// </summary>
		[ProtoMember(90)]
		public int RpcId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[ProtoMember(91)]
		public int Error { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[ProtoMember(92)]
		public string Message { get; set; }

		/// <summary>
		/// 货物类型
		/// </summary>
		[ProtoMember(2)]
		public List<PaymentGood> goods = new List<PaymentGood>();

		/// <summary>
		/// 下次刷新时间
		/// </summary>
		[ProtoMember(14)]
		public long nextRefreshTime { get; set; }

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(888)]
		public int ErrorCode { get; set; }

	}

	/// <summary>
	/// 获取限购列表
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(13107207)]
	public sealed class C2S_GetPaymentList : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 
		/// </summary>
		[ProtoMember(90)]
		public int RpcId { get; set; }

		/// <summary>
		/// 玩家id
		/// </summary>
		[ProtoMember(2)]
		public long playerId { get; set; }

		/// <summary>
		/// 分页号（分页号如果为-1，则将该商店中的所有限购物品都推送给前端）
		/// </summary>
		[ProtoMember(3)]
		public int pageId { get; set; }

	}

	/// <summary>
	/// 存储数据
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(13107208)]
	public sealed class S2C_GetPaymentList : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 
		/// </summary>
		[ProtoMember(90)]
		public int RpcId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[ProtoMember(91)]
		public int Error { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[ProtoMember(92)]
		public string Message { get; set; }

		/// <summary>
		/// 玩家id
		/// </summary>
		[ProtoMember(2)]
		public long playerId { get; set; }

		/// <summary>
		/// 购买存储数据
		/// </summary>
		[ProtoMember(3)]
		public List<PaymentData> dataList = new List<PaymentData>();

		/// <summary>
		/// 刷新时间标识 判断当前周期内是否刷新过 now - rfstime > 24
		/// </summary>
		[ProtoMember(4)]
		public long refreshedTime { get; set; }

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(888)]
		public int ErrorCode { get; set; }

	}

	/// <summary>
	/// ===============================数据结构==============================
	/// </summary>
	[ProtoContract]
	public sealed class PaymentGood
	{
		/// <summary>
		/// 货物id
		/// </summary>
		[ProtoMember(1)]
		public int goodIndex { get; set; }

		/// <summary>
		/// 广告获取最大广告次数
		/// </summary>
		[ProtoMember(4)]
		public int maxAdsCount { get; set; }

		/// <summary>
		/// 广告递增数值参数
		/// </summary>
		[ProtoMember(6)]
		public int adsAddNumber { get; set; }

		/// <summary>
		/// 在线时长获取时间(分钟)
		/// </summary>
		[ProtoMember(7)]
		public int onlineTime { get; set; }

		/// <summary>
		/// 时间段登陆
		/// </summary>
		[ProtoMember(8)]
		public List<string> timelogin = new List<string>();

		/// <summary>
		/// 是否下架
		/// </summary>
		[ProtoMember(10)]
		public bool banned { get; set; }

		/// <summary>
		/// 排序id
		/// </summary>
		[ProtoMember(11)]
		public int sortId { get; set; }

		/// <summary>
		/// 显示品阶
		/// </summary>
		[ProtoMember(13)]
		public int quality { get; set; }

		/// <summary>
		/// 名称
		/// </summary>
		[ProtoMember(14)]
		public string name { get; set; }

	}

	/// <summary>
	/// 
	/// </summary>
	[ProtoContract]
	public sealed class PaymentData
	{
		/// <summary>
		/// 货物id
		/// </summary>
		[ProtoMember(1)]
		public int goodIndex { get; set; }

		/// <summary>
		/// 已经获取的次数
		/// </summary>
		[ProtoMember(3)]
		public int getNum { get; set; }

	}

}

// ==========================================================================================
//  GameFrameX 组织及其衍生项目的版权、商标、专利及其他相关权利
//  GameFrameX organization and its derivative projects' copyrights, trademarks, patents, and related rights
//  均受中华人民共和国及相关国际法律法规保护。
//  are protected by the laws of the People's Republic of China and relevant international regulations.
// 
//  使用本项目须严格遵守相应法律法规及开源许可证之规定。
//  Usage of this project must strictly comply with applicable laws, regulations, and open-source licenses.
// 
//  本项目采用 MIT 许可证与 Apache License 2.0 双许可证分发，
//  This project is dual-licensed under the MIT License and Apache License 2.0,
//  完整许可证文本请参见源代码根目录下的 LICENSE 文件。
//  please refer to the LICENSE file in the root directory of the source code for the full license text.
// 
//  禁止利用本项目实施任何危害国家安全、破坏社会秩序、
//  It is prohibited to use this project to engage in any activities that endanger national security, disrupt social order,
//  侵犯他人合法权益等法律法规所禁止的行为！
//  or infringe upon the legitimate rights and interests of others, as prohibited by laws and regulations!
//  因基于本项目二次开发所产生的一切法律纠纷与责任，
//  Any legal disputes and liabilities arising from secondary development based on this project
//  本项目组织与贡献者概不承担。
//  shall be borne solely by the developer; the project organization and contributors assume no responsibility.
// 
//  GitHub 仓库：https://github.com/GameFrameX
//  GitHub Repository: https://github.com/GameFrameX
//  Gitee  仓库：https://gitee.com/GameFrameX
//  Gitee Repository:  https://gitee.com/GameFrameX
//  官方文档：https://gameframex.doc.alianblank.com/
//  Official Documentation: https://gameframex.doc.alianblank.com/
// ==========================================================================================

using System;
using ProtoBuf;
using System.Collections.Generic;
using GameFrameX.Network.Runtime;

namespace Hotfix.Proto
{
	/// <summary>
	/// 拍卖批次状态
	/// </summary>
	public enum AuctionLotStatus
	{
		/// <summary>
		/// 挂牌中
		/// </summary>
		Listing = 1,

		/// <summary>
		/// 已售出
		/// </summary>
		Sold = 2,

		/// <summary>
		/// 已过期流拍
		/// </summary>
		Expired = 3,
	}

	/// <summary>
	/// 拍卖业务错误码（6 位 = 模块ID 810 + 3 位编号）。客户端协议 ErrorCode 以 int 透传：发生错误时赋具体码，成功不赋值（框架默认 0）。
	/// </summary>
	public enum AuctionErrorCode
	{
		/// <summary>
		/// 会话未绑定玩家（未登录）
		/// </summary>
		NotLoggedIn = 810001,

		/// <summary>
		/// 批次已结束（到期/已售出/已流拍）
		/// </summary>
		LotEnded = 810002,

		/// <summary>
		/// 出价低于当前最低有效出价
		/// </summary>
		BidTooLow = 810003,

		/// <summary>
		/// 不能竞拍自己挂拍的批次
		/// </summary>
		CannotSelfBid = 810004,

		/// <summary>
		/// 价格参数非法
		/// </summary>
		PriceInvalid = 810005,

		/// <summary>
		/// 批次不存在
		/// </summary>
		LotNotFound = 810006,

		/// <summary>
		/// 批次未到期（不可提前结算，区别于已终结 LotEnded）
		/// </summary>
		NotExpired = 810007,
	}

	/// <summary>
	/// 拍卖批次载荷
	/// </summary>
	[ProtoContract]
	public sealed class AuctionLotInfo
	{
		/// <summary>
		/// 批次ID
		/// </summary>
		[ProtoMember(1)]
		public long LotId { get; set; }

		/// <summary>
		/// 物品ID
		/// </summary>
		[ProtoMember(2)]
		public long ItemId { get; set; }

		/// <summary>
		/// 保留价（起拍价）
		/// </summary>
		[ProtoMember(3)]
		public long ReservePrice { get; set; }

		/// <summary>
		/// 一口价
		/// </summary>
		[ProtoMember(4)]
		public long BuyoutPrice { get; set; }

		/// <summary>
		/// 当前最高出价（0 = 暂无出价）
		/// </summary>
		[ProtoMember(5)]
		public long HighestBid { get; set; }

		/// <summary>
		/// 有效出价次数
		/// </summary>
		[ProtoMember(6)]
		public int BidCount { get; set; }

		/// <summary>
		/// 批次状态
		/// </summary>
		[ProtoMember(7)]
		public AuctionLotStatus Status { get; set; }

		/// <summary>
		/// 到期 Unix 秒
		/// </summary>
		[ProtoMember(8)]
		public long ExpireUnixTime { get; set; }

		/// <summary>
		/// 当前最高出价玩家ID（0 = 暂无出价）
		/// </summary>
		[ProtoMember(9)]
		public long HighestBidderPlayerId { get; set; }
	}

	/// <summary>
	/// 请求挂拍（创建拍卖批次）
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((810) << 16) + 10)]
	public sealed class ReqAuctionListLot : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 物品ID
		/// </summary>
		[ProtoMember(1)]
		public long ItemId { get; set; }

		/// <summary>
		/// 保留价（起拍价，须大于 0）
		/// </summary>
		[ProtoMember(2)]
		public long ReservePrice { get; set; }

		/// <summary>
		/// 一口价（须大于保留价）
		/// </summary>
		[ProtoMember(3)]
		public long BuyoutPrice { get; set; }

		/// <summary>
		/// 到期 Unix 秒（须晚于当前时间）
		/// </summary>
		[ProtoMember(4)]
		public long ExpireUnixTime { get; set; }

		public override void Clear()
		{
			ItemId = default;
			ReservePrice = default;
			BuyoutPrice = default;
			ExpireUnixTime = default;
		}
	}

	/// <summary>
	/// 返回挂拍结果
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((810) << 16) + 11)]
	public sealed class RespAuctionListLot : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 新建批次ID
		/// </summary>
		[ProtoMember(1)]
		public long LotId { get; set; }

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			LotId = default;
			ErrorCode = default;
		}
	}

	/// <summary>
	/// 请求出价竞拍
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((810) << 16) + 12)]
	public sealed class ReqAuctionBid : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 批次ID
		/// </summary>
		[ProtoMember(1)]
		public long LotId { get; set; }

		/// <summary>
		/// 出价（首价不低于保留价，其后不低于最高价 ×1.1 向上取整）
		/// </summary>
		[ProtoMember(2)]
		public long Price { get; set; }

		public override void Clear()
		{
			LotId = default;
			Price = default;
		}
	}

	/// <summary>
	/// 返回出价结果
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((810) << 16) + 13)]
	public sealed class RespAuctionBid : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 出价后的当前最高出价
		/// </summary>
		[ProtoMember(1)]
		public long HighestBid { get; set; }

		/// <summary>
		/// 出价后的有效出价次数
		/// </summary>
		[ProtoMember(2)]
		public int BidCount { get; set; }

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			HighestBid = default;
			BidCount = default;
			ErrorCode = default;
		}
	}

	/// <summary>
	/// 请求一口价买断
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((810) << 16) + 14)]
	public sealed class ReqAuctionBuyout : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 批次ID
		/// </summary>
		[ProtoMember(1)]
		public long LotId { get; set; }

		public override void Clear()
		{
			LotId = default;
		}
	}

	/// <summary>
	/// 返回一口价买断结果
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((810) << 16) + 15)]
	public sealed class RespAuctionBuyout : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 成交价（= 一口价）
		/// </summary>
		[ProtoMember(1)]
		public long FinalPrice { get; set; }

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			FinalPrice = default;
			ErrorCode = default;
		}
	}

	/// <summary>
	/// 请求结算批次（仅可结算已到期的挂牌批次）
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((810) << 16) + 16)]
	public sealed class ReqAuctionSettle : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 批次ID
		/// </summary>
		[ProtoMember(1)]
		public long LotId { get; set; }

		public override void Clear()
		{
			LotId = default;
		}
	}

	/// <summary>
	/// 返回结算结果
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((810) << 16) + 17)]
	public sealed class RespAuctionSettle : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 结算后的批次状态（Sold / Expired）
		/// </summary>
		[ProtoMember(1)]
		public AuctionLotStatus Status { get; set; }

		/// <summary>
		/// 成交价（流拍为 0）
		/// </summary>
		[ProtoMember(2)]
		public long FinalPrice { get; set; }

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			Status = default;
			FinalPrice = default;
			ErrorCode = default;
		}
	}

	/// <summary>
	/// 请求查询全部挂牌中的批次
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((810) << 16) + 18)]
	public sealed class ReqAuctionQueryLots : MessageObject, IRequestMessage
	{

		public override void Clear()
		{
		}
	}

	/// <summary>
	/// 返回挂牌中的批次列表（过期批次已惰性结算并被过滤）
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((810) << 16) + 19)]
	public sealed class RespAuctionQueryLots : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 挂牌中的批次列表
		/// </summary>
		[ProtoMember(1)]
		public List<AuctionLotInfo> Lots { get; set; } = new List<AuctionLotInfo>();

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			Lots.Clear();
			ErrorCode = default;
		}
	}

}

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
	/// 挂单方向
	/// </summary>
	public enum TradeSide
	{
		/// <summary>
		/// 买单（买入）
		/// </summary>
		Buy = 1,

		/// <summary>
		/// 卖单（卖出）
		/// </summary>
		Sell = 2,
	}

	/// <summary>
	/// 挂单交易业务错误码（6 位 = 模块ID 800 + 3 位编号）。客户端协议 ErrorCode 以 int 透传：发生错误时赋具体码，成功不赋值（框架默认 0）。
	/// </summary>
	public enum TradeErrorCode
	{
		/// <summary>
		/// 会话未绑定玩家（未登录）
		/// </summary>
		NotLoggedIn = 800001,

		/// <summary>
		/// 挂单方向非法
		/// </summary>
		SideInvalid = 800002,

		/// <summary>
		/// 挂单数量非法（须大于 0）
		/// </summary>
		QuantityInvalid = 800003,

		/// <summary>
		/// 挂单单价非法（须大于 0）
		/// </summary>
		PriceInvalid = 800004,

		/// <summary>
		/// 订单不存在
		/// </summary>
		OrderNotFound = 800005,
	}

	/// <summary>
	/// 撮合成交回执载荷
	/// </summary>
	[ProtoContract]
	public sealed class TradeDealInfo
	{
		/// <summary>
		/// 成交流水ID
		/// </summary>
		[ProtoMember(1)]
		public long TradeId { get; set; }

		/// <summary>
		/// 成交数量
		/// </summary>
		[ProtoMember(2)]
		public int Quantity { get; set; }

		/// <summary>
		/// 成交单价（按挂单方 maker 价格）
		/// </summary>
		[ProtoMember(3)]
		public long UnitPrice { get; set; }

		/// <summary>
		/// 对手方玩家ID
		/// </summary>
		[ProtoMember(4)]
		public long CounterpartyPlayerId { get; set; }
	}

	/// <summary>
	/// 订单簿挂单载荷（盘口查询）
	/// </summary>
	[ProtoContract]
	public sealed class TradeOrderInfo
	{
		/// <summary>
		/// 订单ID
		/// </summary>
		[ProtoMember(1)]
		public long OrderId { get; set; }

		/// <summary>
		/// 挂单玩家ID
		/// </summary>
		[ProtoMember(2)]
		public long PlayerId { get; set; }

		/// <summary>
		/// 剩余数量
		/// </summary>
		[ProtoMember(3)]
		public int Remaining { get; set; }

		/// <summary>
		/// 挂单单价
		/// </summary>
		[ProtoMember(4)]
		public long UnitPrice { get; set; }
	}

	/// <summary>
	/// 请求挂单（新单进入订单簿并即时撮合）
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((800) << 16) + 10)]
	public sealed class ReqTradePlace : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 挂单方向
		/// </summary>
		[ProtoMember(1)]
		public TradeSide Side { get; set; }

		/// <summary>
		/// 物品ID
		/// </summary>
		[ProtoMember(2)]
		public long ItemId { get; set; }

		/// <summary>
		/// 挂单数量（须大于 0）
		/// </summary>
		[ProtoMember(3)]
		public int Quantity { get; set; }

		/// <summary>
		/// 挂单单价（须大于 0）
		/// </summary>
		[ProtoMember(4)]
		public long UnitPrice { get; set; }

		public override void Clear()
		{
			Side = default;
			ItemId = default;
			Quantity = default;
			UnitPrice = default;
		}
	}

	/// <summary>
	/// 返回挂单结果（新单即时撮合回执；OrderId 为 0 表示全部成交，非 0 表示留量挂单号）
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((800) << 16) + 11)]
	public sealed class RespTradePlace : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 挂单号（0 = 新单已全部成交，无留量）
		/// </summary>
		[ProtoMember(1)]
		public long OrderId { get; set; }

		/// <summary>
		/// 新单剩余未成交数量
		/// </summary>
		[ProtoMember(2)]
		public int Remaining { get; set; }

		/// <summary>
		/// 即时撮合成交回执列表（按成交顺序）
		/// </summary>
		[ProtoMember(3)]
		public List<TradeDealInfo> Trades { get; set; } = new List<TradeDealInfo>();

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			OrderId = default;
			Remaining = default;
			Trades.Clear();
			ErrorCode = default;
		}
	}

	/// <summary>
	/// 请求撤销挂单
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((800) << 16) + 12)]
	public sealed class ReqTradeCancel : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 订单ID
		/// </summary>
		[ProtoMember(1)]
		public long OrderId { get; set; }

		public override void Clear()
		{
			OrderId = default;
		}
	}

	/// <summary>
	/// 返回撤销挂单结果（撤销时的剩余数量）
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((800) << 16) + 13)]
	public sealed class RespTradeCancel : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 撤销时的剩余数量
		/// </summary>
		[ProtoMember(1)]
		public int Remaining { get; set; }

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			Remaining = default;
			ErrorCode = default;
		}
	}

	/// <summary>
	/// 请求查询订单簿（按价格优先序返回指定方向的挂单列表）
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((800) << 16) + 14)]
	public sealed class ReqTradeBook : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 物品ID
		/// </summary>
		[ProtoMember(1)]
		public long ItemId { get; set; }

		/// <summary>
		/// 挂单方向（Buy 返回买盘、Sell 返回卖盘）
		/// </summary>
		[ProtoMember(2)]
		public TradeSide Side { get; set; }

		public override void Clear()
		{
			ItemId = default;
			Side = default;
		}
	}

	/// <summary>
	/// 返回订单簿盘口（按价格优先序）
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((800) << 16) + 15)]
	public sealed class RespTradeBook : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 挂单列表（买盘高价优先 / 卖盘低价优先，同价时间优先）
		/// </summary>
		[ProtoMember(1)]
		public List<TradeOrderInfo> Orders { get; set; } = new List<TradeOrderInfo>();

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			Orders.Clear();
			ErrorCode = default;
		}
	}

}

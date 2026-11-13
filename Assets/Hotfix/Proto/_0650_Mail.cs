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
	/// 邮件服务器（Mail Role，跨服投递队列，域号 650）业务错误码（6 位 = 模块ID 650 + 3 位编号）。
	/// </summary>
	public enum MailDeliveryErrorCode
	{
		/// <summary>
		/// 会话未绑定玩家（未登录）
		/// </summary>
		NotLoggedIn = 650001,

		/// <summary>
		/// 邮件已处于终态（已投递/已死亡/已过期），不可再操作
		/// </summary>
		NotDeliverable = 650002,

		/// <summary>
		/// 邮件标题非法（空或超过长度上限）
		/// </summary>
		TitleInvalid = 650003,

		/// <summary>
		/// 邮件不存在
		/// </summary>
		MailNotFound = 650004,
	}

	/// <summary>
	/// 跨服投递状态
	/// </summary>
	public enum MailDeliveryStatus
	{
		/// <summary>
		/// 排队待投递
		/// </summary>
		Queued = 1,

		/// <summary>
		/// 已投递（终态）
		/// </summary>
		Delivered = 2,

		/// <summary>
		/// 重试超限死亡（终态）
		/// </summary>
		Dead = 3,

		/// <summary>
		/// 已过期（终态）
		/// </summary>
		Expired = 4,
	}

	/// <summary>
	/// 跨服投递邮件摘要载荷
	/// </summary>
	[ProtoContract]
	public sealed class MailDeliveryInfo
	{
		/// <summary>
		/// 邮件ID
		/// </summary>
		[ProtoMember(1)]
		public long MailId { get; set; }

		/// <summary>
		/// 发件玩家ID
		/// </summary>
		[ProtoMember(2)]
		public long FromPlayerId { get; set; }

		/// <summary>
		/// 收件玩家ID
		/// </summary>
		[ProtoMember(3)]
		public long ToPlayerId { get; set; }

		/// <summary>
		/// 邮件标题
		/// </summary>
		[ProtoMember(4)]
		public string Title { get; set; }

		/// <summary>
		/// 投递状态
		/// </summary>
		[ProtoMember(5)]
		public MailDeliveryStatus Status { get; set; }
	}

	/// <summary>
	/// 请求将一封邮件加入跨服投递队列
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((650) << 16) + 10)]
	public sealed class ReqMailEnqueue : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 收件玩家ID
		/// </summary>
		[ProtoMember(1)]
		public long ToPlayerId { get; set; }

		/// <summary>
		/// 邮件标题（1-64 字符）
		/// </summary>
		[ProtoMember(2)]
		public string Title { get; set; }

		/// <summary>
		/// 邮件正文
		/// </summary>
		[ProtoMember(3)]
		public string Body { get; set; }

		/// <summary>
		/// 过期 Unix 秒（0 表示永久有效）
		/// </summary>
		[ProtoMember(4)]
		public long ExpireUnixTime { get; set; }

		public override void Clear()
		{
			ToPlayerId = default;
			Title = default;
			Body = default;
			ExpireUnixTime = default;
		}
	}

	/// <summary>
	/// 返回入队结果
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((650) << 16) + 11)]
	public sealed class RespMailEnqueue : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 分配的邮件ID
		/// </summary>
		[ProtoMember(1)]
		public long MailId { get; set; }

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			MailId = default;
			ErrorCode = default;
		}
	}

	/// <summary>
	/// 请求查询指定收件人的待投递邮件（参数键控，无需登录语义）
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((650) << 16) + 12)]
	public sealed class ReqMailQueryPending : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 收件玩家ID
		/// </summary>
		[ProtoMember(1)]
		public long ToPlayerId { get; set; }

		/// <summary>
		/// 查询条数上限
		/// </summary>
		[ProtoMember(2)]
		public int Limit { get; set; }

		public override void Clear()
		{
			ToPlayerId = default;
			Limit = default;
		}
	}

	/// <summary>
	/// 返回待投递邮件摘要列表（过期邮件惰性置 Expired 且不返回）
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((650) << 16) + 13)]
	public sealed class RespMailQueryPending : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 待投递邮件摘要列表
		/// </summary>
		[ProtoMember(1)]
		public List<MailDeliveryInfo> Mails { get; set; } = new List<MailDeliveryInfo>();

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			Mails.Clear();
			ErrorCode = default;
		}
	}

	/// <summary>
	/// 请求将邮件标记为已投递
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((650) << 16) + 14)]
	public sealed class ReqMailMarkDelivered : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 邮件ID
		/// </summary>
		[ProtoMember(1)]
		public long MailId { get; set; }

		public override void Clear()
		{
			MailId = default;
		}
	}

	/// <summary>
	/// 返回标记已投递结果
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((650) << 16) + 15)]
	public sealed class RespMailMarkDelivered : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			ErrorCode = default;
		}
	}

	/// <summary>
	/// 请求登记一次投递失败（重试计数 +1，超过上限进入 Dead 终态）
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((650) << 16) + 16)]
	public sealed class ReqMailMarkFailed : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 邮件ID
		/// </summary>
		[ProtoMember(1)]
		public long MailId { get; set; }

		public override void Clear()
		{
			MailId = default;
		}
	}

	/// <summary>
	/// 返回登记失败结果（含最新重试计数）
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((650) << 16) + 17)]
	public sealed class RespMailMarkFailed : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 登记后的重试计数
		/// </summary>
		[ProtoMember(1)]
		public int RetryCount { get; set; }

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			RetryCount = default;
			ErrorCode = default;
		}
	}

}

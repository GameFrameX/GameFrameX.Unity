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
	/// 违规处罚类型
	/// </summary>
	public enum GmPenaltyType
	{
		/// <summary>
		/// 封禁
		/// </summary>
		Ban = 1,

		/// <summary>
		/// 禁言
		/// </summary>
		Mute = 2,
	}

	/// <summary>
	/// 违规处罚状态
	/// </summary>
	public enum GmPenaltyStatus
	{
		/// <summary>
		/// 生效中
		/// </summary>
		Active = 1,

		/// <summary>
		/// 已撤销
		/// </summary>
		Revoked = 2,

		/// <summary>
		/// 已过期
		/// </summary>
		Expired = 3,
	}

	/// <summary>
	/// 违规处罚业务错误码（6 位 = 模块ID 900 + 3 位编号）。客户端协议 ErrorCode 以 int 透传：发生错误时赋具体码，成功不赋值（框架默认 0）。
	/// </summary>
	public enum GmErrorCode
	{
		/// <summary>
		/// 会话未绑定玩家（未登录）
		/// </summary>
		NotLoggedIn = 900001,

		/// <summary>
		/// 处罚类型非法
		/// </summary>
		PenaltyTypeInvalid = 900002,

		/// <summary>
		/// 处罚时长非法（须在 (0, 365 天] 内）
		/// </summary>
		DurationInvalid = 900003,

		/// <summary>
		/// 处罚记录不存在
		/// </summary>
		PenaltyNotFound = 900004,

		/// <summary>
		/// 处罚不在生效中（已撤销或已过期）
		/// </summary>
		NotActive = 900005,
	}

	/// <summary>
	/// 违规处罚载荷
	/// </summary>
	[ProtoContract]
	public sealed class GmPenaltyInfo
	{
		/// <summary>
		/// 处罚ID
		/// </summary>
		[ProtoMember(1)]
		public long PenaltyId { get; set; }

		/// <summary>
		/// 处罚类型
		/// </summary>
		[ProtoMember(2)]
		public GmPenaltyType Type { get; set; }

		/// <summary>
		/// 处罚原因
		/// </summary>
		[ProtoMember(3)]
		public string Reason { get; set; }

		/// <summary>
		/// 创建 Unix 秒
		/// </summary>
		[ProtoMember(4)]
		public long CreatedUnixTime { get; set; }

		/// <summary>
		/// 到期 Unix 秒
		/// </summary>
		[ProtoMember(5)]
		public long ExpireUnixTime { get; set; }

		/// <summary>
		/// 处罚状态
		/// </summary>
		[ProtoMember(6)]
		public GmPenaltyStatus Status { get; set; }
	}

	/// <summary>
	/// 请求新增违规处罚
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((900) << 16) + 10)]
	public sealed class ReqGmAddPenalty : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 被处罚玩家ID
		/// </summary>
		[ProtoMember(1)]
		public long PlayerId { get; set; }

		/// <summary>
		/// 处罚类型（Ban / Mute）
		/// </summary>
		[ProtoMember(2)]
		public GmPenaltyType Type { get; set; }

		/// <summary>
		/// 处罚原因
		/// </summary>
		[ProtoMember(3)]
		public string Reason { get; set; }

		/// <summary>
		/// 处罚时长（秒，(0, 365 天]）
		/// </summary>
		[ProtoMember(4)]
		public long DurationSeconds { get; set; }

		public override void Clear()
		{
			PlayerId = default;
			Type = default;
			Reason = default;
			DurationSeconds = default;
		}
	}

	/// <summary>
	/// 返回新增违规处罚结果
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((900) << 16) + 11)]
	public sealed class RespGmAddPenalty : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 新建处罚ID
		/// </summary>
		[ProtoMember(1)]
		public long PenaltyId { get; set; }

		/// <summary>
		/// 处罚到期 Unix 秒
		/// </summary>
		[ProtoMember(2)]
		public long ExpireUnixTime { get; set; }

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			PenaltyId = default;
			ExpireUnixTime = default;
			ErrorCode = default;
		}
	}

	/// <summary>
	/// 请求撤销违规处罚（仅可撤销生效中的处罚）
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((900) << 16) + 12)]
	public sealed class ReqGmRevokePenalty : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 处罚ID
		/// </summary>
		[ProtoMember(1)]
		public long PenaltyId { get; set; }

		public override void Clear()
		{
			PenaltyId = default;
		}
	}

	/// <summary>
	/// 返回撤销违规处罚结果
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((900) << 16) + 13)]
	public sealed class RespGmRevokePenalty : MessageObject, IResponseMessage
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
	/// 请求查询玩家生效中的违规处罚（已过期惰性置 Expired 并被过滤）
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((900) << 16) + 14)]
	public sealed class ReqGmQueryPenalties : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 被查询玩家ID
		/// </summary>
		[ProtoMember(1)]
		public long PlayerId { get; set; }

		public override void Clear()
		{
			PlayerId = default;
		}
	}

	/// <summary>
	/// 返回玩家生效中的违规处罚列表
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((900) << 16) + 15)]
	public sealed class RespGmQueryPenalties : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 生效中的处罚列表（未过期且未撤销）
		/// </summary>
		[ProtoMember(1)]
		public List<GmPenaltyInfo> Penalties { get; set; } = new List<GmPenaltyInfo>();

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			Penalties.Clear();
			ErrorCode = default;
		}
	}

}

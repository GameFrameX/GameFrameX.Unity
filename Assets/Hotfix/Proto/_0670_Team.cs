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
	/// 队伍服务器（Team Role，队伍管理，域号 670）业务错误码（6 位 = 模块ID 670 + 3 位编号）。
	/// </summary>
	public enum TeamErrorCode
	{
		/// <summary>
		/// 会话未绑定玩家（未登录）
		/// </summary>
		NotLoggedIn = 670001,

		/// <summary>
		/// 队伍名称非法（空或超过长度上限）
		/// </summary>
		NameInvalid = 670002,

		/// <summary>
		/// 队伍人数上限非法（允许区间 2-10）
		/// </summary>
		CapacityInvalid = 670003,

		/// <summary>
		/// 队伍不存在
		/// </summary>
		TeamNotFound = 670004,

		/// <summary>
		/// 队伍已满员
		/// </summary>
		TeamFull = 670005,

		/// <summary>
		/// 玩家已在其他队伍中（一人一队）
		/// </summary>
		AlreadyInTeam = 670006,

		/// <summary>
		/// 玩家不是该队伍成员
		/// </summary>
		NotMember = 670007,

		/// <summary>
		/// 仅队长可执行该操作
		/// </summary>
		NotLeader = 670008,

		/// <summary>
		/// 不能对自己执行该操作
		/// </summary>
		CannotSelf = 670009,
	}

	/// <summary>
	/// 队伍摘要载荷
	/// </summary>
	[ProtoContract]
	public sealed class TeamInfo
	{
		/// <summary>
		/// 队伍ID
		/// </summary>
		[ProtoMember(1)]
		public long TeamId { get; set; }

		/// <summary>
		/// 队伍名称
		/// </summary>
		[ProtoMember(2)]
		public string Name { get; set; }

		/// <summary>
		/// 队长玩家ID
		/// </summary>
		[ProtoMember(3)]
		public long LeaderId { get; set; }

		/// <summary>
		/// 当前成员数
		/// </summary>
		[ProtoMember(4)]
		public int MemberCount { get; set; }

		/// <summary>
		/// 成员数上限
		/// </summary>
		[ProtoMember(5)]
		public int MaxMemberCount { get; set; }
	}

	/// <summary>
	/// 请求创建队伍（创建者为队长）
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((670) << 16) + 10)]
	public sealed class ReqTeamCreate : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 队伍名称（1-32 字符）
		/// </summary>
		[ProtoMember(1)]
		public string Name { get; set; }

		/// <summary>
		/// 成员数上限（2-10）
		/// </summary>
		[ProtoMember(2)]
		public int MaxMemberCount { get; set; }

		public override void Clear()
		{
			Name = default;
			MaxMemberCount = default;
		}
	}

	/// <summary>
	/// 返回创建队伍结果
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((670) << 16) + 11)]
	public sealed class RespTeamCreate : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 分配的队伍ID
		/// </summary>
		[ProtoMember(1)]
		public long TeamId { get; set; }

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			TeamId = default;
			ErrorCode = default;
		}
	}

	/// <summary>
	/// 请求加入指定队伍
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((670) << 16) + 12)]
	public sealed class ReqTeamJoin : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 队伍ID
		/// </summary>
		[ProtoMember(1)]
		public long TeamId { get; set; }

		public override void Clear()
		{
			TeamId = default;
		}
	}

	/// <summary>
	/// 返回加入队伍结果
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((670) << 16) + 13)]
	public sealed class RespTeamJoin : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 加入后成员数
		/// </summary>
		[ProtoMember(1)]
		public int MemberCount { get; set; }

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			MemberCount = default;
			ErrorCode = default;
		}
	}

	/// <summary>
	/// 请求离开当前队伍（队长离队触发转移或解散）
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((670) << 16) + 14)]
	public sealed class ReqTeamLeave : MessageObject, IRequestMessage
	{

		public override void Clear()
		{
		}
	}

	/// <summary>
	/// 返回离开队伍结果
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((670) << 16) + 15)]
	public sealed class RespTeamLeave : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 离开的队伍ID
		/// </summary>
		[ProtoMember(1)]
		public long TeamId { get; set; }

		/// <summary>
		/// 离开后队伍是否已解散
		/// </summary>
		[ProtoMember(2)]
		public bool Disbanded { get; set; }

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			TeamId = default;
			Disbanded = default;
			ErrorCode = default;
		}
	}

	/// <summary>
	/// 请求队长移除指定成员（不能踢自己）
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((670) << 16) + 16)]
	public sealed class ReqTeamKick : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 要移除的成员玩家ID
		/// </summary>
		[ProtoMember(1)]
		public long MemberPlayerId { get; set; }

		public override void Clear()
		{
			MemberPlayerId = default;
		}
	}

	/// <summary>
	/// 返回移除成员结果
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((670) << 16) + 17)]
	public sealed class RespTeamKick : MessageObject, IResponseMessage
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
	/// 请求队长解散当前队伍
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((670) << 16) + 18)]
	public sealed class ReqTeamDisband : MessageObject, IRequestMessage
	{

		public override void Clear()
		{
		}
	}

	/// <summary>
	/// 返回解散队伍结果
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((670) << 16) + 19)]
	public sealed class RespTeamDisband : MessageObject, IResponseMessage
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
	/// 请求拉取全部队伍列表
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((670) << 16) + 20)]
	public sealed class ReqTeamList : MessageObject, IRequestMessage
	{

		public override void Clear()
		{
		}
	}

	/// <summary>
	/// 返回全部队伍列表
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((670) << 16) + 21)]
	public sealed class RespTeamList : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 队伍摘要列表
		/// </summary>
		[ProtoMember(1)]
		public List<TeamInfo> Teams { get; set; } = new List<TeamInfo>();

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			Teams.Clear();
			ErrorCode = default;
		}
	}

}

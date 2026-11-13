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
	/// 世界公告状态。
	/// </summary>
	public enum WorldAnnouncementStatus
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
	/// 世界公告业务错误码（6 位 = 模块ID 450 + 3 位编号）。客户端协议 ErrorCode 以 int 透传：发生错误时赋具体码，成功不赋值（框架默认 0）。
	/// </summary>
	public enum WorldErrorCode
	{
		/// <summary>
		/// 会话未绑定玩家（未登录，发布者身份获取失败）
		/// </summary>
		NotLoggedIn = 450001,

		/// <summary>
		/// 标题非法（空或超过长度上限）
		/// </summary>
		TitleInvalid = 450002,

		/// <summary>
		/// 过期时间非法（须晚于当前时间）
		/// </summary>
		ExpireInvalid = 450003,

		/// <summary>
		/// 公告不存在
		/// </summary>
		AnnouncementNotFound = 450004,

		/// <summary>
		/// 公告非生效中状态（不可撤销）
		/// </summary>
		NotActive = 450005,
	}

	/// <summary>
	/// 世界公告信息载荷
	/// </summary>
	[ProtoContract]
	public sealed class WorldAnnouncementInfo
	{
		/// <summary>
		/// 公告ID
		/// </summary>
		[ProtoMember(1)]
		public long Id { get; set; }

		/// <summary>
		/// 标题
		/// </summary>
		[ProtoMember(2)]
		public string Title { get; set; }

		/// <summary>
		/// 正文
		/// </summary>
		[ProtoMember(3)]
		public string Content { get; set; }

		/// <summary>
		/// 发布者玩家ID
		/// </summary>
		[ProtoMember(4)]
		public long PublisherPlayerId { get; set; }

		/// <summary>
		/// 发布 Unix 秒
		/// </summary>
		[ProtoMember(5)]
		public long CreatedUnixTime { get; set; }

		/// <summary>
		/// 过期 Unix 秒
		/// </summary>
		[ProtoMember(6)]
		public long ExpireUnixTime { get; set; }
	}

	/// <summary>
	/// 请求发布公告（需登录，发布者为会话玩家）
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((450) << 16) + 10)]
	public sealed class ReqWorldPublish : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 标题（1-64 字符）
		/// </summary>
		[ProtoMember(1)]
		public string Title { get; set; }

		/// <summary>
		/// 正文
		/// </summary>
		[ProtoMember(2)]
		public string Content { get; set; }

		/// <summary>
		/// 过期 Unix 秒（须晚于当前时间）
		/// </summary>
		[ProtoMember(3)]
		public long ExpireUnixTime { get; set; }

		public override void Clear()
		{
			Title = default;
			Content = default;
			ExpireUnixTime = default;
		}
	}

	/// <summary>
	/// 返回发布公告结果
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((450) << 16) + 11)]
	public sealed class RespWorldPublish : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 新公告ID
		/// </summary>
		[ProtoMember(1)]
		public long AnnouncementId { get; set; }

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			AnnouncementId = default;
			ErrorCode = default;
		}
	}

	/// <summary>
	/// 请求撤销公告（无需登录）
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((450) << 16) + 12)]
	public sealed class ReqWorldRevoke : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 公告ID
		/// </summary>
		[ProtoMember(1)]
		public long AnnouncementId { get; set; }

		public override void Clear()
		{
			AnnouncementId = default;
		}
	}

	/// <summary>
	/// 返回撤销公告结果
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((450) << 16) + 13)]
	public sealed class RespWorldRevoke : MessageObject, IResponseMessage
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
	/// 请求查询生效中公告（无需登录）
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((450) << 16) + 14)]
	public sealed class ReqWorldQuery : MessageObject, IRequestMessage
	{

		public override void Clear()
		{
		}
	}

	/// <summary>
	/// 返回生效中公告列表（过期已惰性置 Expired 并过滤）
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((450) << 16) + 15)]
	public sealed class RespWorldQuery : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 生效中公告列表
		/// </summary>
		[ProtoMember(1)]
		public List<WorldAnnouncementInfo> Announcements { get; set; } = new List<WorldAnnouncementInfo>();

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			Announcements.Clear();
			ErrorCode = default;
		}
	}

}

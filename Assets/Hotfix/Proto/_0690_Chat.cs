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
	/// 聊天业务错误码（6 位 = 模块ID 690 + 3 位编号）。客户端协议 ErrorCode 以 int 透传：发生错误时赋具体码，成功不赋值（框架默认 0）。
	/// </summary>
	public enum ChatErrorCode
	{
		/// <summary>
		/// 会话未绑定玩家（未登录）
		/// </summary>
		NotLoggedIn = 690001,

		/// <summary>
		/// 频道不存在
		/// </summary>
		ChannelNotFound = 690002,

		/// <summary>
		/// 未加入该频道
		/// </summary>
		NotJoined = 690003,

		/// <summary>
		/// 频道成员已满
		/// </summary>
		ChannelFull = 690004,

		/// <summary>
		/// 发言内容非法（空或超过长度上限）
		/// </summary>
		TextInvalid = 690005,
	}

	/// <summary>
	/// 聊天消息载荷
	/// </summary>
	[ProtoContract]
	public sealed class ChatMessageInfo
	{
		/// <summary>
		/// 频道内递增序号
		/// </summary>
		[ProtoMember(1)]
		public long Seq { get; set; }

		/// <summary>
		/// 发言玩家ID
		/// </summary>
		[ProtoMember(2)]
		public long PlayerId { get; set; }

		/// <summary>
		/// 消息文本
		/// </summary>
		[ProtoMember(3)]
		public string Text { get; set; }

		/// <summary>
		/// 发言 Unix 秒
		/// </summary>
		[ProtoMember(4)]
		public long UnixTime { get; set; }
	}

	/// <summary>
	/// 请求加入聊天频道
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((690) << 16) + 10)]
	public sealed class ReqChatJoin : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 频道ID
		/// </summary>
		[ProtoMember(1)]
		public long ChannelId { get; set; }

		public override void Clear()
		{
			ChannelId = default;
		}
	}

	/// <summary>
	/// 返回加入聊天频道
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((690) << 16) + 11)]
	public sealed class RespChatJoin : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 加入后频道成员数
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
	/// 请求离开聊天频道
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((690) << 16) + 12)]
	public sealed class ReqChatLeave : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 频道ID
		/// </summary>
		[ProtoMember(1)]
		public long ChannelId { get; set; }

		public override void Clear()
		{
			ChannelId = default;
		}
	}

	/// <summary>
	/// 返回离开聊天频道
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((690) << 16) + 13)]
	public sealed class RespChatLeave : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 离开后频道成员数
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
	/// 请求在频道发言
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((690) << 16) + 14)]
	public sealed class ReqChatSpeak : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 频道ID
		/// </summary>
		[ProtoMember(1)]
		public long ChannelId { get; set; }

		/// <summary>
		/// 消息文本（1-200 字符）
		/// </summary>
		[ProtoMember(2)]
		public string Text { get; set; }

		public override void Clear()
		{
			ChannelId = default;
			Text = default;
		}
	}

	/// <summary>
	/// 返回频道发言结果（含落库后的完整消息）
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((690) << 16) + 15)]
	public sealed class RespChatSpeak : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 落库后的消息载荷
		/// </summary>
		[ProtoMember(1)]
		public ChatMessageInfo Message { get; set; }

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			Message = default;
			ErrorCode = default;
		}
	}

	/// <summary>
	/// 请求拉取频道历史消息（增量：仅返回序号大于 SinceSeq 的消息）
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((690) << 16) + 16)]
	public sealed class ReqChatPullHistory : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 频道ID
		/// </summary>
		[ProtoMember(1)]
		public long ChannelId { get; set; }

		/// <summary>
		/// 已同步到的最大序号（0 表示从头拉取）
		/// </summary>
		[ProtoMember(2)]
		public long SinceSeq { get; set; }

		/// <summary>
		/// 拉取条数上限（&lt;=0 或超上限按上限处理）
		/// </summary>
		[ProtoMember(3)]
		public int Limit { get; set; }

		public override void Clear()
		{
			ChannelId = default;
			SinceSeq = default;
			Limit = default;
		}
	}

	/// <summary>
	/// 返回频道历史消息
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((690) << 16) + 17)]
	public sealed class RespChatPullHistory : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 增量消息列表（按序号升序）
		/// </summary>
		[ProtoMember(1)]
		public List<ChatMessageInfo> Messages { get; set; } = new List<ChatMessageInfo>();

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			Messages.Clear();
			ErrorCode = default;
		}
	}

}

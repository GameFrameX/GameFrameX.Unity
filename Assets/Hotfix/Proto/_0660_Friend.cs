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
	/// 好友服务器（Friend Role，好友关系，域号 660）业务错误码（6 位 = 模块ID 660 + 3 位编号）。
	/// </summary>
	public enum FriendErrorCode
	{
		/// <summary>
		/// 会话未绑定玩家（未登录）
		/// </summary>
		NotLoggedIn = 660001,

		/// <summary>
		/// 不能对自己发起好友操作
		/// </summary>
		CannotSelf = 660002,

		/// <summary>
		/// 双方已经是好友
		/// </summary>
		AlreadyFriends = 660003,

		/// <summary>
		/// 好友申请已在等待中（重复申请）
		/// </summary>
		RequestPending = 660004,

		/// <summary>
		/// 对应好友申请不存在
		/// </summary>
		RequestNotFound = 660005,
	}

	/// <summary>
	/// 请求向目标玩家发送好友申请
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((660) << 16) + 10)]
	public sealed class ReqFriendSendRequest : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 目标玩家ID
		/// </summary>
		[ProtoMember(1)]
		public long ToPlayerId { get; set; }

		public override void Clear()
		{
			ToPlayerId = default;
		}
	}

	/// <summary>
	/// 返回发送好友申请结果
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((660) << 16) + 11)]
	public sealed class RespFriendSendRequest : MessageObject, IResponseMessage
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
	/// 请求接受指定来源玩家的好友申请
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((660) << 16) + 12)]
	public sealed class ReqFriendAccept : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 申请来源玩家ID
		/// </summary>
		[ProtoMember(1)]
		public long FromPlayerId { get; set; }

		public override void Clear()
		{
			FromPlayerId = default;
		}
	}

	/// <summary>
	/// 返回接受好友申请结果
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((660) << 16) + 13)]
	public sealed class RespFriendAccept : MessageObject, IResponseMessage
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
	/// 请求拒绝指定来源玩家的好友申请
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((660) << 16) + 14)]
	public sealed class ReqFriendReject : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 申请来源玩家ID
		/// </summary>
		[ProtoMember(1)]
		public long FromPlayerId { get; set; }

		public override void Clear()
		{
			FromPlayerId = default;
		}
	}

	/// <summary>
	/// 返回拒绝好友申请结果
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((660) << 16) + 15)]
	public sealed class RespFriendReject : MessageObject, IResponseMessage
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
	/// 请求解除与指定好友的好友关系
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((660) << 16) + 16)]
	public sealed class ReqFriendRemove : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 要解除的好友玩家ID
		/// </summary>
		[ProtoMember(1)]
		public long FriendPlayerId { get; set; }

		public override void Clear()
		{
			FriendPlayerId = default;
		}
	}

	/// <summary>
	/// 返回解除好友关系结果
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((660) << 16) + 17)]
	public sealed class RespFriendRemove : MessageObject, IResponseMessage
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
	/// 请求拉取自己的好友关系全量列表
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((660) << 16) + 18)]
	public sealed class ReqFriendRelationList : MessageObject, IRequestMessage
	{

		public override void Clear()
		{
		}
	}

	/// <summary>
	/// 返回好友关系全量列表（好友 / 收到的申请 / 发出的申请）
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((660) << 16) + 19)]
	public sealed class RespFriendRelationList : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 好友列表（玩家ID）
		/// </summary>
		[ProtoMember(1)]
		public List<long> Friends { get; set; } = new List<long>();

		/// <summary>
		/// 收到的好友申请列表（玩家ID）
		/// </summary>
		[ProtoMember(2)]
		public List<long> IncomingRequests { get; set; } = new List<long>();

		/// <summary>
		/// 发出的好友申请列表（玩家ID）
		/// </summary>
		[ProtoMember(3)]
		public List<long> OutgoingRequests { get; set; } = new List<long>();

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			Friends.Clear();
			IncomingRequests.Clear();
			OutgoingRequests.Clear();
			ErrorCode = default;
		}
	}

}

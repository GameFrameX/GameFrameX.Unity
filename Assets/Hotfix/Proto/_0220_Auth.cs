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
	/// 权限码白名单枚举。
	/// </summary>
	public enum AuthPermission
	{
		/// <summary>
		/// 登录权限
		/// </summary>
		Login = 1,

		/// <summary>
		/// 聊天权限
		/// </summary>
		Chat = 2,

		/// <summary>
		/// 交易权限
		/// </summary>
		Trade = 3,

		/// <summary>
		/// GM 指令权限
		/// </summary>
		GmCommand = 4,
	}

	/// <summary>
	/// 权限授予业务错误码（6 位 = 模块ID 220 + 3 位编号）。客户端协议 ErrorCode 以 int 透传：发生错误时赋具体码，成功不赋值（框架默认 0）。
	/// </summary>
	public enum AuthErrorCode
	{
		/// <summary>
		/// 会话未绑定玩家（未登录）
		/// </summary>
		NotLoggedIn = 220001,

		/// <summary>
		/// 权限码未在白名单定义
		/// </summary>
		PermissionUnknown = 220002,

		/// <summary>
		/// 权限已授予（重复授予幂等报错）
		/// </summary>
		AlreadyGranted = 220003,

		/// <summary>
		/// 权限未被授予（撤销不存在）
		/// </summary>
		NotGranted = 220004,
	}

	/// <summary>
	/// 请求授予玩家权限（操作者=请求者）
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((220) << 16) + 10)]
	public sealed class ReqAuthGrant : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 目标玩家ID
		/// </summary>
		[ProtoMember(1)]
		public long TargetPlayerId { get; set; }

		/// <summary>
		/// 权限码（AuthPermission 白名单）
		/// </summary>
		[ProtoMember(2)]
		public AuthPermission Permission { get; set; }

		public override void Clear()
		{
			TargetPlayerId = default;
			Permission = default;
		}
	}

	/// <summary>
	/// 返回授予玩家权限结果
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((220) << 16) + 11)]
	public sealed class RespAuthGrant : MessageObject, IResponseMessage
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
	/// 请求撤销玩家权限（操作者=请求者）
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((220) << 16) + 12)]
	public sealed class ReqAuthRevoke : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 目标玩家ID
		/// </summary>
		[ProtoMember(1)]
		public long TargetPlayerId { get; set; }

		/// <summary>
		/// 权限码（AuthPermission 白名单）
		/// </summary>
		[ProtoMember(2)]
		public AuthPermission Permission { get; set; }

		public override void Clear()
		{
			TargetPlayerId = default;
			Permission = default;
		}
	}

	/// <summary>
	/// 返回撤销玩家权限结果
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((220) << 16) + 13)]
	public sealed class RespAuthRevoke : MessageObject, IResponseMessage
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
	/// 请求查询玩家是否持有权限
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((220) << 16) + 14)]
	public sealed class ReqAuthHas : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 目标玩家ID
		/// </summary>
		[ProtoMember(1)]
		public long TargetPlayerId { get; set; }

		/// <summary>
		/// 权限码（AuthPermission 白名单）
		/// </summary>
		[ProtoMember(2)]
		public AuthPermission Permission { get; set; }

		public override void Clear()
		{
			TargetPlayerId = default;
			Permission = default;
		}
	}

	/// <summary>
	/// 返回玩家是否持有权限
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((220) << 16) + 15)]
	public sealed class RespAuthHas : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 是否已授予
		/// </summary>
		[ProtoMember(1)]
		public bool Granted { get; set; }

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			Granted = default;
			ErrorCode = default;
		}
	}

	/// <summary>
	/// 请求列举玩家持有的全部权限
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((220) << 16) + 16)]
	public sealed class ReqAuthListPermissions : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 目标玩家ID
		/// </summary>
		[ProtoMember(1)]
		public long TargetPlayerId { get; set; }

		public override void Clear()
		{
			TargetPlayerId = default;
		}
	}

	/// <summary>
	/// 返回玩家持有的全部权限列表
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((220) << 16) + 17)]
	public sealed class RespAuthListPermissions : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 权限列表
		/// </summary>
		[ProtoMember(1)]
		public List<AuthPermission> Permissions { get; set; } = new List<AuthPermission>();

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			Permissions.Clear();
			ErrorCode = default;
		}
	}

}

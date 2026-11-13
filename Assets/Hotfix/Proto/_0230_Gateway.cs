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
	/// 后端路由业务错误码（6 位 = 模块ID 230 + 3 位编号）。客户端协议 ErrorCode 以 int 透传：发生错误时赋具体码，成功不赋值（框架默认 0）。
	/// </summary>
	public enum GatewayErrorCode
	{
		/// <summary>
		/// 权重非法（须大于 0）
		/// </summary>
		WeightInvalid = 230002,

		/// <summary>
		/// 路由目标不存在
		/// </summary>
		TargetNotFound = 230003,

		/// <summary>
		/// 无可用路由目标（全部心跳超时或角色类型无注册）
		/// </summary>
		NoAvailableTarget = 230004,
	}

	/// <summary>
	/// 请求注册路由目标
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((230) << 16) + 10)]
	public sealed class ReqGatewayRegisterTarget : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 角色类型（如 Trade/Chat）
		/// </summary>
		[ProtoMember(1)]
		public string RoleType { get; set; }

		/// <summary>
		/// 主机地址
		/// </summary>
		[ProtoMember(2)]
		public string Host { get; set; }

		/// <summary>
		/// 端口
		/// </summary>
		[ProtoMember(3)]
		public int Port { get; set; }

		/// <summary>
		/// 路由权重（大于 0）
		/// </summary>
		[ProtoMember(4)]
		public int Weight { get; set; }

		public override void Clear()
		{
			RoleType = default;
			Host = default;
			Port = default;
			Weight = default;
		}
	}

	/// <summary>
	/// 返回注册路由目标结果
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((230) << 16) + 11)]
	public sealed class RespGatewayRegisterTarget : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 新注册目标的ID
		/// </summary>
		[ProtoMember(1)]
		public long TargetId { get; set; }

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			TargetId = default;
			ErrorCode = default;
		}
	}

	/// <summary>
	/// 请求路由目标心跳
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((230) << 16) + 12)]
	public sealed class ReqGatewayHeartbeat : MessageObject, IRequestMessage, IHeartBeatMessage
	{
		/// <summary>
		/// 路由目标ID
		/// </summary>
		[ProtoMember(1)]
		public long TargetId { get; set; }

		public override void Clear()
		{
			TargetId = default;
		}
	}

	/// <summary>
	/// 返回路由目标心跳结果
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((230) << 16) + 13)]
	public sealed class RespGatewayHeartbeat : MessageObject, IResponseMessage, IHeartBeatMessage
	{
		/// <summary>
		/// 目标是否存活（心跳已刷新）
		/// </summary>
		[ProtoMember(1)]
		public bool Alive { get; set; }

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			Alive = default;
			ErrorCode = default;
		}
	}

	/// <summary>
	/// 请求解析角色类型的路由目标（可用集按权重随机）
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((230) << 16) + 14)]
	public sealed class ReqGatewayResolveTarget : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 角色类型
		/// </summary>
		[ProtoMember(1)]
		public string RoleType { get; set; }

		public override void Clear()
		{
			RoleType = default;
		}
	}

	/// <summary>
	/// 返回解析到的路由目标
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((230) << 16) + 15)]
	public sealed class RespGatewayResolveTarget : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 路由目标ID
		/// </summary>
		[ProtoMember(1)]
		public long TargetId { get; set; }

		/// <summary>
		/// 主机地址
		/// </summary>
		[ProtoMember(2)]
		public string Host { get; set; }

		/// <summary>
		/// 端口
		/// </summary>
		[ProtoMember(3)]
		public int Port { get; set; }

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			TargetId = default;
			Host = default;
			Port = default;
			ErrorCode = default;
		}
	}

	/// <summary>
	/// 请求注销路由目标
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((230) << 16) + 16)]
	public sealed class ReqGatewayUnregisterTarget : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 路由目标ID
		/// </summary>
		[ProtoMember(1)]
		public long TargetId { get; set; }

		public override void Clear()
		{
			TargetId = default;
		}
	}

	/// <summary>
	/// 返回注销路由目标结果
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((230) << 16) + 17)]
	public sealed class RespGatewayUnregisterTarget : MessageObject, IResponseMessage
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

}

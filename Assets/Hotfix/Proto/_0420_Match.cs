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
	/// 匹配模式。
	/// </summary>
	public enum MatchMode
	{
		/// <summary>
		/// 1 对 1
		/// </summary>
		OneVsOne = 1,

		/// <summary>
		/// 2 对 2
		/// </summary>
		TwoVsTwo = 2,
	}

	/// <summary>
	/// 匹配业务错误码（6 位 = 模块ID 420 + 3 位编号）。客户端协议 ErrorCode 以 int 透传：发生错误时赋具体码，成功不赋值（框架默认 0）。
	/// </summary>
	public enum MatchErrorCode
	{
		/// <summary>
		/// 会话未绑定玩家（未登录）
		/// </summary>
		NotLoggedIn = 420001,

		/// <summary>
		/// 玩家已在任一匹配池排队中
		/// </summary>
		AlreadyQueuing = 420002,

		/// <summary>
		/// 玩家当前不在匹配池排队中
		/// </summary>
		NotQueuing = 420003,

		/// <summary>
		/// 匹配模式非法（未定义）
		/// </summary>
		ModeInvalid = 420004,

		/// <summary>
		/// 评分非法（须大于 0）
		/// </summary>
		RatingInvalid = 420005,
	}

	/// <summary>
	/// 请求加入匹配池
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((420) << 16) + 10)]
	public sealed class ReqMatchEnqueue : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 匹配模式
		/// </summary>
		[ProtoMember(1)]
		public MatchMode Mode { get; set; }

		/// <summary>
		/// 玩家评分（大于 0）
		/// </summary>
		[ProtoMember(2)]
		public int Rating { get; set; }

		public override void Clear()
		{
			Mode = default;
			Rating = default;
		}
	}

	/// <summary>
	/// 返回加入匹配池结果（MatchId 非 0 表示立即配对成功）
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((420) << 16) + 11)]
	public sealed class RespMatchEnqueue : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 匹配对局ID（0 = 排队中，尚未配对）
		/// </summary>
		[ProtoMember(1)]
		public long MatchId { get; set; }

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			MatchId = default;
			ErrorCode = default;
		}
	}

	/// <summary>
	/// 请求取消排队
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((420) << 16) + 12)]
	public sealed class ReqMatchCancel : MessageObject, IRequestMessage
	{

		public override void Clear()
		{
		}
	}

	/// <summary>
	/// 返回取消排队结果
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((420) << 16) + 13)]
	public sealed class RespMatchCancel : MessageObject, IResponseMessage
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
	/// 请求查询本人匹配状态
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((420) << 16) + 14)]
	public sealed class ReqMatchStatus : MessageObject, IRequestMessage
	{

		public override void Clear()
		{
		}
	}

	/// <summary>
	/// 返回本人匹配状态（排队中或最近一次配对结果）
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((420) << 16) + 15)]
	public sealed class RespMatchStatus : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 是否排队中
		/// </summary>
		[ProtoMember(1)]
		public bool Queuing { get; set; }

		/// <summary>
		/// 最近配对成功的对局ID（0 = 无）
		/// </summary>
		[ProtoMember(2)]
		public long MatchId { get; set; }

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			Queuing = default;
			MatchId = default;
			ErrorCode = default;
		}
	}

}

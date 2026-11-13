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
	/// 场景在线业务错误码（6 位 = 模块ID 440 + 3 位编号）。客户端协议 ErrorCode 以 int 透传：发生错误时赋具体码，成功不赋值（框架默认 0）。
	/// </summary>
	public enum SceneErrorCode
	{
		/// <summary>
		/// 会话未绑定玩家（未登录）
		/// </summary>
		NotLoggedIn = 440001,

		/// <summary>
		/// 玩家不在该场景中
		/// </summary>
		NotInScene = 440002,

		/// <summary>
		/// 场景已满（容量 100）
		/// </summary>
		SceneFull = 440003,
	}

	/// <summary>
	/// 请求进入场景
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((440) << 16) + 10)]
	public sealed class ReqSceneEnter : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 场景ID
		/// </summary>
		[ProtoMember(1)]
		public long SceneId { get; set; }

		public override void Clear()
		{
			SceneId = default;
		}
	}

	/// <summary>
	/// 返回进入场景结果
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((440) << 16) + 11)]
	public sealed class RespSceneEnter : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 进入后场景在线人数
		/// </summary>
		[ProtoMember(1)]
		public int PlayerCount { get; set; }

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			PlayerCount = default;
			ErrorCode = default;
		}
	}

	/// <summary>
	/// 请求离开场景
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((440) << 16) + 12)]
	public sealed class ReqSceneLeave : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 场景ID
		/// </summary>
		[ProtoMember(1)]
		public long SceneId { get; set; }

		public override void Clear()
		{
			SceneId = default;
		}
	}

	/// <summary>
	/// 返回离开场景结果
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((440) << 16) + 13)]
	public sealed class RespSceneLeave : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 离开后场景在线人数
		/// </summary>
		[ProtoMember(1)]
		public int PlayerCount { get; set; }

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			PlayerCount = default;
			ErrorCode = default;
		}
	}

	/// <summary>
	/// 请求查询场景在线列表
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((440) << 16) + 14)]
	public sealed class ReqSceneQuery : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 场景ID
		/// </summary>
		[ProtoMember(1)]
		public long SceneId { get; set; }

		public override void Clear()
		{
			SceneId = default;
		}
	}

	/// <summary>
	/// 返回场景在线列表
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((440) << 16) + 15)]
	public sealed class RespSceneQuery : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 场景在线人数
		/// </summary>
		[ProtoMember(1)]
		public int PlayerCount { get; set; }

		/// <summary>
		/// 在线玩家ID列表
		/// </summary>
		[ProtoMember(2)]
		public List<long> PlayerIds { get; set; } = new List<long>();

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			PlayerCount = default;
			PlayerIds.Clear();
			ErrorCode = default;
		}
	}

}

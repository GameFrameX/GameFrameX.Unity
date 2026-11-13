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
	/// 房间分配业务错误码（6 位 = 模块ID 430 + 3 位编号）。客户端协议 ErrorCode 以 int 透传：发生错误时赋具体码，成功不赋值（框架默认 0）。
	/// </summary>
	public enum RoomErrorCode
	{
		/// <summary>
		/// 会话未绑定玩家（未登录）
		/// </summary>
		NotLoggedIn = 430001,

		/// <summary>
		/// 玩家当前未被分配房间
		/// </summary>
		NotAllocated = 430002,

		/// <summary>
		/// 房间容量非法（须 1-20，0 表示默认 4）
		/// </summary>
		CapacityInvalid = 430003,
	}

	/// <summary>
	/// 房间槽位信息载荷
	/// </summary>
	[ProtoContract]
	public sealed class RoomSlotInfo
	{
		/// <summary>
		/// 房间ID
		/// </summary>
		[ProtoMember(1)]
		public long RoomId { get; set; }

		/// <summary>
		/// 房间容量
		/// </summary>
		[ProtoMember(2)]
		public int Capacity { get; set; }

		/// <summary>
		/// 当前占用数
		/// </summary>
		[ProtoMember(3)]
		public int PlayerCount { get; set; }
	}

	/// <summary>
	/// 请求分配房间槽位
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((430) << 16) + 10)]
	public sealed class ReqRoomAllocate : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 期望房间容量（1-20；0 = 默认 4）
		/// </summary>
		[ProtoMember(1)]
		public int Capacity { get; set; }

		public override void Clear()
		{
			Capacity = default;
		}
	}

	/// <summary>
	/// 返回分配房间槽位结果
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((430) << 16) + 11)]
	public sealed class RespRoomAllocate : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 分配到的房间ID
		/// </summary>
		[ProtoMember(1)]
		public long RoomId { get; set; }

		/// <summary>
		/// 分配后房间占用数
		/// </summary>
		[ProtoMember(2)]
		public int PlayerCount { get; set; }

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			RoomId = default;
			PlayerCount = default;
			ErrorCode = default;
		}
	}

	/// <summary>
	/// 请求释放房间槽位
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((430) << 16) + 12)]
	public sealed class ReqRoomRelease : MessageObject, IRequestMessage
	{

		public override void Clear()
		{
		}
	}

	/// <summary>
	/// 返回释放房间槽位结果
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((430) << 16) + 13)]
	public sealed class RespRoomRelease : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 被释放的房间ID
		/// </summary>
		[ProtoMember(1)]
		public long RoomId { get; set; }

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			RoomId = default;
			ErrorCode = default;
		}
	}

	/// <summary>
	/// 请求查询全部房间槽位
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((430) << 16) + 14)]
	public sealed class ReqRoomQuery : MessageObject, IRequestMessage
	{

		public override void Clear()
		{
		}
	}

	/// <summary>
	/// 返回全部房间槽位列表
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((430) << 16) + 15)]
	public sealed class RespRoomQuery : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 房间槽位列表
		/// </summary>
		[ProtoMember(1)]
		public List<RoomSlotInfo> Rooms { get; set; } = new List<RoomSlotInfo>();

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			Rooms.Clear();
			ErrorCode = default;
		}
	}

}

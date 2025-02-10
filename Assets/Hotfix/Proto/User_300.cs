using System;
using ProtoBuf;
using System.Collections.Generic;
using GameFrameX.Network.Runtime;

namespace Hotfix.Proto
{
	/// <summary>
	/// 请求账号登录
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(19660810)]
	public sealed class ReqLogin : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 
		/// </summary>
		[ProtoMember(1)]
		public string UserName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[ProtoMember(2)]
		public string Platform { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[ProtoMember(3)]
		public int SdkType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[ProtoMember(4)]
		public string SdkToken { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[ProtoMember(5)]
		public string Device { get; set; }

		/// <summary>
		/// 密码
		/// </summary>
		[ProtoMember(6)]
		public string Password { get; set; }

	}

	/// <summary>
	/// 请求账号登录返回
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(19660811)]
	public sealed class RespLogin : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 
		/// </summary>
		[ProtoMember(1)]
		public int Code { get; set; }

		/// <summary>
		/// 账号名
		/// </summary>
		[ProtoMember(2)]
		public string RoleName { get; set; }

		/// <summary>
		/// 账号ID
		/// </summary>
		[ProtoMember(3)]
		public long Id { get; set; }

		/// <summary>
		/// 账号等级
		/// </summary>
		[ProtoMember(4)]
		public uint Level { get; set; }

		/// <summary>
		/// 创建时间
		/// </summary>
		[ProtoMember(5)]
		public long CreateTime { get; set; }

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(888)]
		public int ErrorCode { get; set; }

	}

	/// <summary>
	/// 请求角色创建
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(19660812)]
	public sealed class ReqPlayerCreate : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 账号ID
		/// </summary>
		[ProtoMember(1)]
		public long Id { get; set; }

		/// <summary>
		/// 角色名
		/// </summary>
		[ProtoMember(2)]
		public string Name { get; set; }

	}

	/// <summary>
	/// 请求角色创建返回
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(19660813)]
	public sealed class RespPlayerCreate : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 角色信息
		/// </summary>
		[ProtoMember(1)]
		public PlayerInfo PlayerInfo { get; set; }

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(888)]
		public int ErrorCode { get; set; }

	}

	/// <summary>
	/// 请求角色列表
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(19660814)]
	public sealed class ReqPlayerList : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 账号ID
		/// </summary>
		[ProtoMember(1)]
		public long Id { get; set; }

	}

	/// <summary>
	/// 请求角色列表返回
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(19660815)]
	public sealed class RespPlayerList : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 角色列表
		/// </summary>
		[ProtoMember(1)]
		public List<PlayerInfo> PlayerList { get; set; } = new List<PlayerInfo>();

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(888)]
		public int ErrorCode { get; set; }

	}

	/// <summary>
	/// 
	/// </summary>
	[ProtoContract]
	public sealed class PlayerInfo
	{
		/// <summary>
		/// 角色ID
		/// </summary>
		[ProtoMember(1)]
		public long Id { get; set; }

		/// <summary>
		/// 角色名
		/// </summary>
		[ProtoMember(2)]
		public string Name { get; set; }

		/// <summary>
		/// 角色等级
		/// </summary>
		[ProtoMember(3)]
		public uint Level { get; set; }

		/// <summary>
		/// 角色状态
		/// </summary>
		[ProtoMember(4)]
		public int State { get; set; }

		/// <summary>
		/// 角色头像
		/// </summary>
		[ProtoMember(5)]
		public uint Avatar { get; set; }

		/// <summary>
		/// 角色当前经验
		/// </summary>
		[ProtoMember(6)]
		public ulong CurrentExp { get; set; }

	}

	/// <summary>
	/// 请求玩家登录
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(19660816)]
	public sealed class ReqPlayerLogin : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 角色ID
		/// </summary>
		[ProtoMember(1)]
		public long Id { get; set; }

	}

	/// <summary>
	/// 请求玩家登录返回
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(19660817)]
	public sealed class RespPlayerLogin : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 
		/// </summary>
		[ProtoMember(1)]
		public int Code { get; set; }

		/// <summary>
		/// 创建时间
		/// </summary>
		[ProtoMember(2)]
		public long CreateTime { get; set; }

		/// <summary>
		/// 角色信息
		/// </summary>
		[ProtoMember(3)]
		public PlayerInfo PlayerInfo { get; set; }

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(888)]
		public int ErrorCode { get; set; }

	}

	/// <summary>
	/// 客户端每次请求都会回复错误码
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(19660818)]
	public sealed class RespErrorCode : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 0:表示无错误
		/// </summary>
		[ProtoMember(1)]
		public long ErrCode { get; set; }

		/// <summary>
		/// 错误描述（不为0时有效）
		/// </summary>
		[ProtoMember(2)]
		public string Desc { get; set; }

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(888)]
		public int ErrorCode { get; set; }

	}

	/// <summary>
	/// 
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(19660819)]
	public sealed class RespPrompt : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 提示信息类型（1Tip提示，2跑马灯，3插队跑马灯，4弹窗，5弹窗回到登陆，6弹窗退出游戏）
		/// </summary>
		[ProtoMember(1)]
		public int Type { get; set; }

		/// <summary>
		/// 提示内容
		/// </summary>
		[ProtoMember(2)]
		public string Content { get; set; }

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(888)]
		public int ErrorCode { get; set; }

	}

}

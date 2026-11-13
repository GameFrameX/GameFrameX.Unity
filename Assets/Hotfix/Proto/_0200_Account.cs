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
	/// 账号业务错误码（6 位 = 模块ID 200 + 3 位编号）。客户端协议 ErrorCode 以 int 透传：发生错误时赋具体码，成功不赋值（框架默认 0）。
	/// </summary>
	public enum AccountErrorCode
	{
		/// <summary>
		/// 账号名已存在
		/// </summary>
		AccountExists = 200002,

		/// <summary>
		/// 账号名非法（长度 4-32 且非纯空白）
		/// </summary>
		NameInvalid = 200003,

		/// <summary>
		/// 密码过短（至少 6 个字符）
		/// </summary>
		PasswordTooShort = 200004,

		/// <summary>
		/// 账号不存在
		/// </summary>
		AccountNotFound = 200005,

		/// <summary>
		/// 旧密码不匹配
		/// </summary>
		OldPasswordMismatch = 200006,
	}

	/// <summary>
	/// 请求注册账号
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((200) << 16) + 10)]
	public sealed class ReqAccountRegister : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 账号名（4-32 字符，非纯空白，全局唯一）
		/// </summary>
		[ProtoMember(1)]
		public string AccountName { get; set; }

		/// <summary>
		/// 密码（至少 6 个字符）
		/// </summary>
		[ProtoMember(2)]
		public string Password { get; set; }

		public override void Clear()
		{
			AccountName = default;
			Password = default;
		}
	}

	/// <summary>
	/// 返回注册账号结果（新建账号档案）
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((200) << 16) + 11)]
	public sealed class RespAccountRegister : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 新建账号ID
		/// </summary>
		[ProtoMember(1)]
		public long AccountId { get; set; }

		/// <summary>
		/// 创建 Unix 秒
		/// </summary>
		[ProtoMember(2)]
		public long CreatedUnixTime { get; set; }

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			AccountId = default;
			CreatedUnixTime = default;
			ErrorCode = default;
		}
	}

	/// <summary>
	/// 请求查询账号档案（不含密码）
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((200) << 16) + 12)]
	public sealed class ReqAccountQuery : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 账号名
		/// </summary>
		[ProtoMember(1)]
		public string AccountName { get; set; }

		public override void Clear()
		{
			AccountName = default;
		}
	}

	/// <summary>
	/// 返回账号档案（不含密码）
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((200) << 16) + 13)]
	public sealed class RespAccountQuery : MessageObject, IResponseMessage
	{
		/// <summary>
		/// 账号ID
		/// </summary>
		[ProtoMember(1)]
		public long AccountId { get; set; }

		/// <summary>
		/// 创建 Unix 秒
		/// </summary>
		[ProtoMember(2)]
		public long CreatedUnixTime { get; set; }

		/// <summary>
		/// 最近一次登录 Unix 秒（从未登录为 0）
		/// </summary>
		[ProtoMember(3)]
		public long LastLoginUnixTime { get; set; }

		/// <summary>
		/// 返回的错误码
		/// </summary>
		[ProtoMember(2047)]
		public int ErrorCode { get; set; }

		public override void Clear()
		{
			AccountId = default;
			CreatedUnixTime = default;
			LastLoginUnixTime = default;
			ErrorCode = default;
		}
	}

	/// <summary>
	/// 请求修改账号密码
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((200) << 16) + 14)]
	public sealed class ReqAccountChangePassword : MessageObject, IRequestMessage
	{
		/// <summary>
		/// 账号名
		/// </summary>
		[ProtoMember(1)]
		public string AccountName { get; set; }

		/// <summary>
		/// 旧密码
		/// </summary>
		[ProtoMember(2)]
		public string OldPassword { get; set; }

		/// <summary>
		/// 新密码（至少 6 个字符）
		/// </summary>
		[ProtoMember(3)]
		public string NewPassword { get; set; }

		public override void Clear()
		{
			AccountName = default;
			OldPassword = default;
			NewPassword = default;
		}
	}

	/// <summary>
	/// 返回修改账号密码结果
	/// </summary>
	[ProtoContract]
	[MessageTypeHandler(((200) << 16) + 15)]
	public sealed class RespAccountChangePassword : MessageObject, IResponseMessage
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

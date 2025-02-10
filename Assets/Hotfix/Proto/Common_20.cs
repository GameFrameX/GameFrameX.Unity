using System;
using ProtoBuf;
using System.Collections.Generic;
using GameFrameX.Network.Runtime;

namespace Hotfix.Proto
{
	/// <summary>
	/// 返回码
	/// </summary>
	public enum ResultCode
	{
		/// <summary>
		/// 成功
		/// </summary>
		Success = 0, 
		/// <summary>
		/// 失败
		/// </summary>
		Failed = 1, 
	}

	/// <summary>
	/// 
	/// </summary>
	public enum PhoneType
	{
		/// <summary>
		/// 手机
		/// </summary>
		Mobile = 0, 
		/// <summary>
		/// 
		/// </summary>
		Home = 1, 
		/// <summary>
		/// 工作号码
		/// </summary>
		Work = 2, 
	}

	/// <summary>
	/// 
	/// </summary>
	[ProtoContract]
	public sealed class PhoneNumber
	{
		/// <summary>
		/// 
		/// </summary>
		[ProtoMember(1)]
		public string Number { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[ProtoMember(2)]
		public PhoneType Type { get; set; }

	}

	/// <summary>
	/// 
	/// </summary>
	[ProtoContract]
	public sealed class Person
	{
		/// <summary>
		/// 
		/// </summary>
		[ProtoMember(1)]
		public string Name { get; set; }

		/// <summary>
		/// Unique ID number for this person.
		/// </summary>
		[ProtoMember(2)]
		public int Id { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[ProtoMember(3)]
		public string Email { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[ProtoMember(4)]
		public List<PhoneNumber> Phones { get; set; } = new List<PhoneNumber>();

	}

	/// <summary>
	/// Our address book file is just one of these.
	/// </summary>
	[ProtoContract]
	public sealed class AddressBook
	{
		/// <summary>
		/// 
		/// </summary>
		[ProtoMember(1)]
		public List<Person> People { get; set; } = new List<Person>();

	}

}

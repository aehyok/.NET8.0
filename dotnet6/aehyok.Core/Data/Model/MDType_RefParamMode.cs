using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Data.Model
{
	public enum MDType_RefParamMode
	{
		/// <summary>
		/// 正常模式
		/// </summary>
		[EnumMember]
		Normal = 1,
		/// <summary>
		/// 使用参数模式
		/// </summary>
		[EnumMember]
		UserParam = 2,
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Data.Model
{
	public enum MDType_DisplayType
	{
		/// <summary>
		/// 表格方式显示
		/// </summary>
		[EnumMember]
		GridType = 0,
		/// <summary>
		/// 表单方式显示
		/// </summary>
		[EnumMember]
		FormType = 1
	}
}

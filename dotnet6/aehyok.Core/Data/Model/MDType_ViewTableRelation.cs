using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Data.Model
{
	public enum MDType_ViewTableRelation
	{
		/// <summary>
		/// 主表的一条记录对应本表一条(或没有)记录
		/// </summary>
		[EnumMember]
		SingleChildRecord = 1,
		/// <summary>
		/// 主表的一条记录对应本表多条(或没有)记录
		/// </summary>
		[EnumMember]
		MultiChildRecord = 0,
	}
}

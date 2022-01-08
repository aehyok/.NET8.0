using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Data.Model
{
	public enum MDType_ViewTable
	{
		[EnumMember]
		MainTable = 0,
		[EnumMember]
		ChildTable = 1,
	}
}

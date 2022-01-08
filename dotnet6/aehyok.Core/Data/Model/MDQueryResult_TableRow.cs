using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Data.Model
{
	public class MDQueryResult_TableRow
	{
		/// <summary>
		/// 表名称
		/// </summary>
		
		private Dictionary<string, object> _data = new Dictionary<string, object>();


		/// <summary>
		/// 记录列值
		/// </summary>
		
		public Dictionary<string, object> Values
		{
			get { return _data; }
			set { _data = value; }
		}

	}
}

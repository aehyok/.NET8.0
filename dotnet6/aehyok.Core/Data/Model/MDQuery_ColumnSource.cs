using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Data.Model
{
	public class MDQuery_ColumnSource
	{
		/// <summary>
		/// 查询模型名称
		/// </summary>
		
		public string QueryModelName { get; set; }
		/// <summary>
		/// 表名称
		/// </summary>
		
		public string TableName { get; set; }
		/// <summary>
		/// 字段名称
		/// </summary>
		
		public string ColumnName { get; set; }

		/// <summary>
		/// 构造方法
		/// </summary>
		/// <param name="_qvName">查询模型名称</param>
		/// <param name="_tname">表名称</param>
		/// <param name="_cname">字段名称</param>
		public MDQuery_ColumnSource(string qvName, string tname, string cname)
		{
			QueryModelName = qvName;
			TableName = tname;
			ColumnName = cname;
		}
	}
}

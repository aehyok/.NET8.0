using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Data.Model
{
	public class MDQuery_ResultTable
	{

		/// <summary>
		/// 表的显示名称
		/// </summary>
		/// 
		
		public string DisplayTitle { get; set; }

		/// <summary>
		/// 结果表名
		/// </summary>
		/// 
		
		public string TableName { get; set; }

		/// <summary>
		/// 字段列表
		/// </summary>
		/// 
		
		public List<MDQuery_TableColumn> Columns { get; set; }


	}
}

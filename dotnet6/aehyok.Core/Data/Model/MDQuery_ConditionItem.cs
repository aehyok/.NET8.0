using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Data.Model
{
	public class MDQuery_ConditionItem
	{
		/// <summary>
		/// 是否区分大小写
		/// </summary>
		
		public bool CaseSensitive { get; set; }
		/// <summary>
		/// 序号
		/// </summary>
		
		public string ColumnIndex { get; set; }
		/// <summary>
		/// 字段定义
		/// </summary>
		
		public MDQuery_TableColumn Column { get; set; }
		/// <summary>
		/// 操作符
		/// </summary>
		
		public string Operator { get; set; }
		/// <summary>
		/// 值
		/// </summary>
		
		public List<string> Values { get; set; }

	}
}

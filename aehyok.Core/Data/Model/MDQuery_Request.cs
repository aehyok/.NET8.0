using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Data.Model
{
	public class MDQuery_Request
	{

		/// <summary>
		/// 查询模型名称
		/// </summary>
		
		public string QueryModelName { get; set; }
		/// <summary>
		/// 查询条件表达式
		/// </summary>
		
		public string ConditionExpressions { get; set; }
		/// <summary>
		/// 查询条件列表
		/// </summary>
		
		public List<MDQuery_ConditionItem> ConditionItems { get; set; }
		/// <summary>
		/// 查询结果的主表
		/// </summary>
		
		public MDQuery_ResultTable MainResultTable { get; set; }
		/// <summary>
		/// 查询结果的子表
		/// </summary>
		
		public List<MDQuery_ResultTable> ChildResultTables { get; set; }
	}
}

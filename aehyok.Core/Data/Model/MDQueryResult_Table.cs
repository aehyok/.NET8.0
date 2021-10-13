using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Data.Model
{
	public class MDQueryResult_Table
	{
		/// <summary>
		/// 表名称
		/// </summary>
		
		public string TableName { get; set; }

		/// <summary>
		/// 字段列表
		/// </summary>
		
		public List<MDQueryResult_TableColumn> Columns { get; set; }

		/// <summary>
		/// 记录行集
		/// </summary>
		
		public List<MDQueryResult_TableRow> Rows { get; set; }


		public MDQueryResult_Table(string _tableName)
		{
			// TODO: Complete member initialization
			this.TableName = _tableName;
		}

	}
	public class MDQueryResult_TableColumn
	{
		
		public string ColumnName { get; set; }
		
		public string ColumnType { get; set; }
		public MDQueryResult_TableColumn() { }
		public MDQueryResult_TableColumn(string name, string type)
		{
			ColumnName = name;
			ColumnType = type;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Data.Model
{
	public class MDQuery_TableColumn
	{
		public MDQuery_TableColumn()
		{
		}

		public MDQuery_TableColumn(MDModel_Table_Column columnDefine)
		{
			this.ColumnAlgorithm = columnDefine.ColumnAlgorithm;
			this.ColumnAlias = columnDefine.ColumnAlias;
			this.ColumnDataType = columnDefine.ColumnDataType;
			if (columnDefine.ColumnDefine == null)
			{
				this.ColumnLength = 0;
				this.RefDMB = "";
				this.DisplayFormat = "";
				this.DisplayLength = 80;
			}
			else
			{
				if (columnDefine.ColumnType == QueryColumnType.CalculationColumn)
				{
					this.ColumnLength = 1;
					this.DisplayFormat = "";
					this.RefDMB = "";
					this.DisplayLength = 80;
				}
				else
				{
					this.ColumnLength = columnDefine.ColumnDefine.TableColumn.Length;
					this.DisplayFormat = columnDefine.ColumnDefine.TableColumn.DisplayFormat;
					this.RefDMB = columnDefine.ColumnDefine.TableColumn.RefDMB;
					this.DisplayLength = (columnDefine.ColumnDefine.TableColumn.ColWidth > 10) ? columnDefine.ColumnDefine.TableColumn.ColWidth : 80;
				}
			}
			this.ColumnName = columnDefine.ColumnName;
			this.ColumnTitle = columnDefine.ColumnTitle;
			this.ColumnType = columnDefine.ColumnType;
			this.DisplayOrder = columnDefine.DisplayOrder;
			this.TableName = columnDefine.TableName;
			this.Source = new MDQuery_ColumnSource(columnDefine.QueryModelName, columnDefine.TableName, columnDefine.ColumnName);
		}


		#region 属性
		/// <summary>
		/// 字段类型
		/// </summary>
		/// 
		
		public QueryColumnType ColumnType { get; set; }

		/// <summary>
		/// 字段算法(指如果是计算字段或统计字段,其算法)
		/// </summary>
		/// 
		
		public string ColumnAlgorithm { get; set; }

		/// <summary>
		/// 字段名称
		/// </summary>
		/// 
		
		public string ColumnName { get; set; }

		/// <summary>
		/// 字段别名
		/// </summary>
		/// 
		
		public string ColumnAlias { get; set; }

		/// <summary>
		/// 字段显示名称
		/// </summary>
		/// 
		
		public string ColumnTitle { get; set; }

		/// <summary>
		/// 所在表的表名
		/// </summary>
		/// 
		
		public string TableName { get; set; }

		/// <summary>
		/// 所在查询模型名称
		/// </summary>
		/// 
		
		public string QueryModelName { get; set; }
		/// <summary>
		/// 显示顺序
		/// </summary>
		/// 
		
		public int DisplayOrder { get; set; }

		/// <summary>
		/// 字段的数据类型
		/// </summary>
		/// 
		
		public string ColumnDataType { get; set; }
		/// <summary>
		/// 字段长度
		/// </summary>
		/// 
		
		public int ColumnLength { get; set; }

		/// <summary>
		/// 显示格式
		/// </summary>
		/// 
		
		public string DisplayFormat { get; set; }

		/// <summary>
		/// 列表显示的长度　(单位:px)
		/// </summary>
		/// 
		
		public int DisplayLength { get; set; }
		/// <summary>
		/// 代码表
		/// </summary>
		/// 
		
		public string RefDMB { get; set; }
		/// <summary>
		/// 引用表
		/// </summary>
		/// 
		
		public string RefWord { get; set; }


		/// <summary>
		/// 对应的查询模型字段来源
		/// </summary>
		
		public MDQuery_ColumnSource Source { get; set; }


		#endregion
	}
}

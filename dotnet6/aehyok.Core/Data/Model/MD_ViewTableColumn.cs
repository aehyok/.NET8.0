using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Data.Model
{
	public class MD_ViewTableColumn
	{
		public MD_ViewTableColumn(string vtcid, string vtid, string tcid, bool cancondi, bool canres, bool defaultshow,
				bool isfixitem, bool canmodify, string dw, int prio, int displayOrder)
		{

			ViewTableColumnID = vtcid;
			ViewTableID = vtid;
			ColumnID = tcid;
			CanShowAsCondition = cancondi;
			CanShowAsResult = canres;
			DefaultResult = defaultshow;
			IsFixQueryItem = isfixitem;
			CanModify = canmodify;
			DWDM = dw;
			Priority = prio;
			DisplayOrder = displayOrder;

		}

		
		public string ColumnID { get; set; }

		/// <summary>
		/// 表的字段定义
		/// </summary>
		
		public MD_TableColumn TableColumn { get; set; }


		/// <summary>
		/// 取对应的表ID
		/// </summary>
		
		public string TID { get; set; }

		
		public string TableName { get; set; }

		/// <summary>
		/// 本字段所在的视图表
		/// </summary>
		
		public string ViewTableID { get; set; }

		/// <summary>
		/// 本字段的ID
		/// </summary>
		
		public string ViewTableColumnID { get; set; }

		/// <summary>
		/// 是否可做为查询条件显示
		/// </summary>
		
		public bool CanShowAsCondition { get; set; }

		/// <summary>
		/// 是否可做为查询结果显示
		/// </summary>
		
		public bool CanShowAsResult { get; set; }

		/// <summary>
		/// 是否为默认结果项
		/// </summary>
		
		public bool DefaultResult { get; set; }

		/// <summary>
		/// 是否为固定查询字段
		/// </summary>
		
		public bool IsFixQueryItem { get; set; }

		/// <summary>
		/// 是否可以修改（只用于审核类型的VIEW）
		/// </summary>
		
		public bool CanModify { get; set; }

		/// <summary>
		/// 节点编号
		/// </summary>
		
		public string DWDM { get; set; }
		/// <summary>
		/// 显示顺序
		/// </summary>
		
		public int DisplayOrder { get; set; }

		/// <summary>
		/// 查询优先权(数值越小则越高)
		/// </summary>
		
		public int Priority { get; set; }

		public string DisplayTitle
		{
			get
			{
				if (TableColumn == null) return "";
				return TableColumn.DisplayTitle;
			}
			set
			{
				TableColumn.DisplayTitle = value;
			}
		}
	}
}

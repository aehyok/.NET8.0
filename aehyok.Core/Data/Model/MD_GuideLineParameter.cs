using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Data.Model
{
	public class MD_GuideLineParameter
	{
		/// <summary>
		/// 全部的代码
		/// </summary>
		/// 
		
		public string SelectAllCode { get; set; }

		/// <summary>
		/// 是否可以选"全部"
		/// </summary>
		public bool CanSelectAll
		{
			get
			{
				return (SelectAllCode != null && SelectAllCode != "");
			}
		}


		/// <summary>
		/// 当为代码表时,查询代码是否要求包含下级
		/// </summary>
		/// 
		
		public bool IncludeChildren { get; set; }


		/// <summary>
		/// 代码表名称
		/// </summary>
		/// 
		
		public string RefTableName { get; set; }
		/// <summary>
		/// 默认参数值
		/// </summary>
		/// 
		
		public string DefaultValue { get; set; }

		/// <summary>
		/// 参数名称
		/// </summary>
		/// 
		
		public string ParameterName { get; set; }

		/// <summary>
		/// 显示名称
		/// </summary>
		/// 
		
		public string DisplayTitle { get; set; }

		/// <summary>
		/// 录入框长度
		/// </summary>
		/// 
		
		public int InputWidth { get; set; }
		/// <summary>
		/// 参数显示顺序
		/// </summary>
		/// 
		
		public int DisplayOrder { get; set; }
		/// <summary>
		/// 参数类型
		/// </summary>
		/// 
		
		public string ParameterType { get; set; }

		public MD_GuideLineParameter()
		{
		}


		public MD_GuideLineParameter(string pname, string title, string ptype, int order, string defaultValue, int inputWidth, string refTable, bool includeChildren, string selectAllCode)
		{
			ParameterName = pname;
			DisplayTitle = title;
			ParameterType = ptype;
			DisplayOrder = order;
			DefaultValue = defaultValue;
			InputWidth = inputWidth;
			RefTableName = refTable;
			IncludeChildren = includeChildren;
			SelectAllCode = selectAllCode;
		}
		public MD_GuideLineParameter(string pname, string title, string ptype, int order, int inputWidth, string refTable, bool includeChildren, string selectAllCode)
		{
			ParameterName = pname;
			DisplayTitle = title;
			ParameterType = ptype;
			DisplayOrder = order;
			InputWidth = inputWidth;
			RefTableName = refTable;
			IncludeChildren = includeChildren;
			SelectAllCode = selectAllCode;
		}
	}
}

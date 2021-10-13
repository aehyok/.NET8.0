using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Data.Model
{
	public class MDQuery_GuideLineParameter
	{
		/// <summary>
		/// 参数名称
		/// </summary>
		
		public string ParameterName { get; set; }
		/// <summary>
		/// 参数显示名称
		/// </summary>
		
		public string DisplayTitle { get; set; }
		/// <summary>
		/// 参数类型
		/// </summary>
		
		public string ParameterType { get; set; }
		/// <summary>
		/// 参数值
		/// </summary>
		
		public object ParameterValue { get; set; }

		
		public string RefTableName { get; set; }

		
		public bool IncludeChildren { get; set; }

		
		public string SelectAllCode { get; set; }
		
		public MD_GuideLineParameter Paramter { get; set; }

		
		public object Data { set; get; }
		public MDQuery_GuideLineParameter() { }

		public MDQuery_GuideLineParameter(MD_GuideLineParameter pDefine, object data)
		{
			Paramter = pDefine;
			Data = data;
			this.ParameterName = pDefine.ParameterName;
			this.DisplayTitle = pDefine.DisplayTitle;
			this.ParameterType = pDefine.ParameterType;
			this.ParameterValue = data;
			this.RefTableName = pDefine.RefTableName;
			this.IncludeChildren = pDefine.IncludeChildren;
			this.SelectAllCode = pDefine.SelectAllCode;
		}
		public MDQuery_GuideLineParameter(string paramName, string paramType, object value)
		{
			ParameterName = paramName;
			ParameterType = paramType;
			ParameterValue = value;
		}
	}
}

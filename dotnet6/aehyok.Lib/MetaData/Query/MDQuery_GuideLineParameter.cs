using aehyok.Lib.MetaData.Define;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Query
{
    /// <summary>
    /// 指标查询参数
    /// 
    /// Create by:Lintx
    /// </summary>
    public class MDQuery_GuideLineParameter
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        [DataMember]
        public string ParameterName { get; set; }
        /// <summary>
        /// 参数显示名称
        /// </summary>
        [DataMember]
        public string DisplayTitle { get; set; }
        /// <summary>
        /// 参数类型
        /// </summary>
        [DataMember]
        public string ParameterType { get; set; }
        /// <summary>
        /// 参数值
        /// </summary>
        [DataMember]
        public object ParameterValue { get; set; }

        [DataMember]
        public string RefTableName { get; set; }

        [DataMember]
        public bool IncludeChildren { get; set; }

        [DataMember]
        public string SelectAllCode { get; set; }
        [DataMember]
        public MD_GuideLineParameter Paramter { get; set; }

        [DataMember]
        public object Data { set; get; }
        public MDQuery_GuideLineParameter() { }

        public MDQuery_GuideLineParameter(MD_GuideLineParameter _pDefine, object data)
        {
            Paramter = _pDefine;
            Data = data;
            this.ParameterName = _pDefine.ParameterName;
            this.DisplayTitle = _pDefine.DisplayTitle;
            this.ParameterType = _pDefine.ParameterType;
            this.ParameterValue = data;
            this.RefTableName = _pDefine.RefTableName;
            this.IncludeChildren = _pDefine.IncludeChildren;
            this.SelectAllCode = _pDefine.SelectAllCode;
        }
        public MDQuery_GuideLineParameter(string paramName, string paramType, object value)
        {
            ParameterName = paramName;
            ParameterType = paramType;
            ParameterValue = value;
        }
    }
}

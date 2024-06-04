using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace sun.NCDP.Dtos.GuideLine
{
    /// <summary>
    /// 指标参数元数据定义
    /// 
    /// </summary>
    public class AutoGuideLineParameterDto
    {
        /// <summary>
        /// 全部的代码
        /// </summary>
        /// 
        [DataMember]
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
        [DataMember]
        public bool IncludeChildren { get; set; }


        /// <summary>
        /// 代码表名称
        /// </summary>
        /// 
        [DataMember]
        public string RefTableName { get; set; }
        /// <summary>
        /// 默认参数值
        /// </summary>
        /// 
        [DataMember]
        public string DefaultValue { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        /// 
        [DataMember]
        public string ParameterName { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        /// 
        [DataMember]
        public string DisplayTitle { get; set; }

        /// <summary>
        /// 录入框长度
        /// </summary>
        /// 
        [DataMember]
        public int InputWidth { get; set; }
        /// <summary>
        /// 参数显示顺序
        /// </summary>
        /// 
        [DataMember]
        public int DisplayOrder { get; set; }
        /// <summary>
        /// 参数类型
        /// </summary>
        /// 
        [DataMember]
        public string ParameterType { get; set; }


        /// <summary>
        /// 表达式
        /// </summary>
        [DataMember]
        public string ParameterExpression { get; set; }
        public AutoGuideLineParameterDto()
        {
        }


        public AutoGuideLineParameterDto(string pname, string title, string ptype, int order, string defaultValue, int inputWidth, string refTable, bool includeChildren, string selectAllCode)
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
        public AutoGuideLineParameterDto(string pname, string title, string ptype, int order, int inputWidth, string refTable, bool includeChildren, string selectAllCode, string expression)
        {
            ParameterName = pname;
            DisplayTitle = title;
            ParameterType = ptype;
            DisplayOrder = order;
            InputWidth = inputWidth;
            RefTableName = refTable;
            IncludeChildren = includeChildren;
            SelectAllCode = selectAllCode;
            ParameterExpression = expression;
        }
    }
}
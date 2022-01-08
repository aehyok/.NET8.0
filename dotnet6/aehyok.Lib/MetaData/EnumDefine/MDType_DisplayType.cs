using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.EnumDefine
{
    /// <summary>
    /// 子表显示方式枚举
    /// 
    /// Create by: Lintx
    /// </summary>
    public enum MDType_DisplayType
    {
        /// <summary>
        /// 表格方式显示
        /// </summary>
        [EnumMember]
        GridType = 0,
        /// <summary>
        /// 表单方式显示
        /// </summary>
        [EnumMember]
        FormType = 1
    }
}

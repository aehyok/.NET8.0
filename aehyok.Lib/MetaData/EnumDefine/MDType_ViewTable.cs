using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.EnumDefine
{
    /// <summary>
    /// 查询模型表类型枚举定义
    /// 
    /// Create by :Lintx
    /// </summary>
    public enum MDType_ViewTable
    {
        [EnumMember]
        MainTable = 0,
        [EnumMember]
        ChildTable = 1
    }
}

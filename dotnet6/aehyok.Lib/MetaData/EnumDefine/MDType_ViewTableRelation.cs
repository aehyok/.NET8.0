using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.EnumDefine
{
    /// <summary>
    /// 查询模型表关系类型枚举定义
    /// 
    /// Create by:Lintx
    /// </summary>
    public enum MDType_ViewTableRelation
    {
        /// <summary>
        /// 主表的一条记录对应本表一条(或没有)记录
        /// </summary>
        [EnumMember]
        SingleChildRecord = 1,
        /// <summary>
        /// 主表的一条记录对应本表多条(或没有)记录
        /// </summary>
        [EnumMember]
        MultiChildRecord = 0
    }
}

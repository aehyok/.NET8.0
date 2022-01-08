using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.EnumDefine
{
    /// <summary>
    /// 引用参数模式枚举定义
    /// 
    /// Create by: Lintx
    /// </summary>
    public enum MDType_RefParamMode
    {
        /// <summary>
        /// 正常模式
        /// </summary>
        [EnumMember]
        Normal = 1,
        /// <summary>
        /// 使用参数模式
        /// </summary>
        [EnumMember]
        UserParam = 2
    }
}

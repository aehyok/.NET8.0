using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.EnumDefine
{
    /// <summary>
    /// 下载方式枚举定义
    /// 
    /// Create by: Lintx
    /// </summary>
    public enum MDType_RefDownloadMode
    {
        /// <summary>
        /// 一次性下载
        /// </summary>
        [EnumMember]
        FullDownload = 1,
        /// <summary>
        /// 分级下载
        /// </summary>
        [EnumMember]
        LevelDownload = 2
    }
}

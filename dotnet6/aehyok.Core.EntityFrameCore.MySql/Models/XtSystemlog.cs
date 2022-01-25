using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFramework.MySql.Models
{
    /// <summary>
    /// 系统日志
    /// </summary>
    public partial class XtSystemlog
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// 日志记录时间
        /// </summary>
        public DateTime? Czsj { get; set; }
        /// <summary>
        /// 类型　　INFO:信息　ERROR:错误
        /// </summary>
        public string? Logtype { get; set; }
        /// <summary>
        /// 日志内容
        /// </summary>
        public string? Logtext { get; set; }
        /// <summary>
        /// 备注其他
        /// </summary>
        public string? Mark { get; set; }
    }
}

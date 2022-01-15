using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFrameCore.MySql.Models
{
    /// <summary>
    /// 用户操作日志
    /// </summary>
    public partial class XtUserlog
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// 操作用户ID
        /// </summary>
        public long? Yhid { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime? Czsj { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
        public string? Czlx { get; set; }
        /// <summary>
        /// 操作详细内容
        /// </summary>
        public string? Czxxnr { get; set; }
        /// <summary>
        /// 操作来自IP地址
        /// </summary>
        public string? Fromip { get; set; }
        /// <summary>
        /// 客户端系统ID
        /// </summary>
        public string? Systemid { get; set; }
        /// <summary>
        /// 操作结果 0.未知 1.成功 2.失败
        /// </summary>
        public int? Resulttype { get; set; }
        /// <summary>
        /// 使用主机名
        /// </summary>
        public string? Fromhost { get; set; }
        /// <summary>
        /// 岗位id
        /// </summary>
        public string? Gwid { get; set; }
    }
}

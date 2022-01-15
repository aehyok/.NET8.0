using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFrameCore.MySql.Models
{
    /// <summary>
    /// 查询日志
    /// </summary>
    public partial class MdQuerylog
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime? Sj { get; set; }
        /// <summary>
        /// 查询用时（毫秒数）
        /// </summary>
        public int? Usetime { get; set; }
        /// <summary>
        /// 查询语句
        /// </summary>
        public string? QueryStr { get; set; }
        /// <summary>
        /// {1-模型 2-指标 }
        /// </summary>
        public string? Lx { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public long? Yhid { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string? Bz { get; set; }
    }
}

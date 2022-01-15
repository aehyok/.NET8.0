using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFrameCore.MySql.Models
{
    /// <summary>
    /// 用户TOKEN验证表
    /// </summary>
    public partial class XtUsertoken
    {
        public string Id { get; set; } = null!;
        /// <summary>
        /// 用户名
        /// </summary>
        public string? Username { get; set; }
        /// <summary>
        /// 单位代码
        /// </summary>
        public string? Dwdm { get; set; }
        /// <summary>
        /// TOKEN
        /// </summary>
        public string? Token { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? Updatetime { get; set; }
    }
}

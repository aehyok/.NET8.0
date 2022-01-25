using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFramework.MySql.Models
{
    /// <summary>
    /// 指标展示项目表
    /// </summary>
    public partial class MdZbdisplayitem
    {
        public long Id { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string? Displayname { get; set; }
        /// <summary>
        /// 类型 0复合型 1分组型
        /// </summary>
        public string? Type { get; set; }
        /// <summary>
        /// 单位代码
        /// </summary>
        public string? Dwdm { get; set; }
    }
}

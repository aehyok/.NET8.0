using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFramework.MySql.Models
{
    /// <summary>
    /// 表命名空间
    /// </summary>
    public partial class MdTbnamespace
    {
        public string Namespace { get; set; } = null!;
        public string Dwdm { get; set; } = null!;
        public string? Description { get; set; }
        /// <summary>
        /// 所在菜单
        /// </summary>
        public string? Menuposition { get; set; }
        public string? Displaytitle { get; set; }
        public string? Owner { get; set; }
        public int? Displayorder { get; set; }
        /// <summary>
        /// 包含的概念组
        /// </summary>
        public string? Concepts { get; set; }
    }
}

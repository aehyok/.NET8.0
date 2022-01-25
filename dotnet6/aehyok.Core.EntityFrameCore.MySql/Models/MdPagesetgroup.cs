using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFramework.MySql.Models
{
    public partial class MdPagesetgroup
    {
        /// <summary>
        /// 分组ID
        /// </summary>
        public long Groupid { get; set; }
        /// <summary>
        /// 分组名称
        /// </summary>
        public string? Displayname { get; set; }
        /// <summary>
        /// 所属命名空间
        /// </summary>
        public string? Namespace { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int? Displayorder { get; set; }
        /// <summary>
        /// 单位代码
        /// </summary>
        public string? Dwdm { get; set; }
    }
}

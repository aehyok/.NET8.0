using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFramework.MySql.Models
{
    public partial class MdViewgroup
    {
        public long Vgid { get; set; }
        public string? Namespace { get; set; }
        public string? Viewgroupname { get; set; }
        public string? Displayname { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int? Displayorder { get; set; }
        /// <summary>
        /// 单位代码
        /// </summary>
        public string? Dwdm { get; set; }
        /// <summary>
        /// 是否固定查询（0-否 1-是）
        /// </summary>
        public int? IsGdcx { get; set; }
        /// <summary>
        /// 是否关联查询（0-否 1-是）
        /// </summary>
        public int? IsGlcx { get; set; }
        /// <summary>
        /// 是否数据审核（0-否 1-是）
        /// </summary>
        public int? IsSjsh { get; set; }
    }
}

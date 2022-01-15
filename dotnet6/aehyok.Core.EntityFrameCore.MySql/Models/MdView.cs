using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFrameCore.MySql.Models
{
    /// <summary>
    /// 视图
    /// </summary>
    public partial class MdView
    {
        public long Viewid { get; set; }
        public string? Namespace { get; set; }
        public string? Viewname { get; set; }
        public string? Description { get; set; }
        public string? Displayname { get; set; }
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
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int? Displayorder { get; set; }
        /// <summary>
        /// 接口类型 : ORA_JSIS、SQL_GDFS 
        /// </summary>
        public string? Icstype { get; set; }
        /// <summary>
        /// 扩展META
        /// </summary>
        public string? Extmeta { get; set; }
    }
}

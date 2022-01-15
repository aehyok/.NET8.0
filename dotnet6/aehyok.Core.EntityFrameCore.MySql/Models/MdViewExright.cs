using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFrameCore.MySql.Models
{
    /// <summary>
    /// 查询模型_扩展权限
    /// </summary>
    public partial class MdViewExright
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// 权限值（做为参数代入用,可以为空，表示此值将不做为参数代入，仅用于显示）
        /// </summary>
        public string? Rvalue { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string? Rtitle { get; set; }
        /// <summary>
        /// 查询模型ID
        /// </summary>
        public long? Viewid { get; set; }
        /// <summary>
        /// 父权限ID
        /// </summary>
        public string? Fid { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int? Displayorder { get; set; }
    }
}

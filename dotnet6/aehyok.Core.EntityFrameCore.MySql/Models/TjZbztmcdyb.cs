using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFrameCore.MySql.Models
{
    /// <summary>
    /// 指标主题名称定义表
    /// </summary>
    public partial class TjZbztmcdyb
    {
        /// <summary>
        /// 指标所属主题名称
        /// </summary>
        public string Zbztmc { get; set; } = null!;
        /// <summary>
        /// 指标主题说明
        /// </summary>
        public string? Zbztsm { get; set; }
        /// <summary>
        /// 类型1-报表指标 3-统计指标
        /// </summary>
        public int? Lx { get; set; }
        /// <summary>
        /// 权限类型1-允许下级使用 2-只有本单位使用
        /// </summary>
        public int? Qxlx { get; set; }
        /// <summary>
        /// 命名空间
        /// </summary>
        public string? Namespace { get; set; }
        /// <summary>
        /// 所属单位
        /// </summary>
        public string? Ssdw { get; set; }
    }
}

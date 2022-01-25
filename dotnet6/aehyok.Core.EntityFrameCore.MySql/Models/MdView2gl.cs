using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFramework.MySql.Models
{
    /// <summary>
    /// 模型关联指标定义
    /// </summary>
    public partial class MdView2gl
    {
        public string Id { get; set; } = null!;
        /// <summary>
        /// 模型ID
        /// </summary>
        public string? Viewid { get; set; }
        /// <summary>
        /// 目标指标
        /// </summary>
        public string? Targetgl { get; set; }
        /// <summary>
        /// 指标对应参数
        /// </summary>
        public string? Targetcs { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int? Displayorder { get; set; }
        /// <summary>
        /// 显示标题
        /// </summary>
        public string? Displaytitle { get; set; }
        /// <summary>
        /// 扩展定义
        /// </summary>
        public string? Extendmeta { get; set; }
    }
}

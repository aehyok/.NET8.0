using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFrameCore.MySql.Models
{
    /// <summary>
    /// 目标分析
    /// </summary>
    public partial class MdTargetanalysis
    {
        public long Id { get; set; }
        /// <summary>
        /// 分析内容名称
        /// </summary>
        public string? Mc { get; set; }
        /// <summary>
        /// 显示标题
        /// </summary>
        public string? Displaytitle { get; set; }
        /// <summary>
        /// 类型 B 基本信息　CSZB 带参数的指标　WCSZB 无参数指标
        /// </summary>
        public string? Lx { get; set; }
        /// <summary>
        /// 算法
        /// </summary>
        public string? Sf { get; set; }
        /// <summary>
        /// 元数据
        /// </summary>
        public string? Fsmeta { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int? Displayorder { get; set; }
    }
}

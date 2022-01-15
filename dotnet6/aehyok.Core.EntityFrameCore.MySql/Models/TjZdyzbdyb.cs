using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFrameCore.MySql.Models
{
    /// <summary>
    /// 自定义指标定义表
    /// </summary>
    public partial class TjZdyzbdyb
    {
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 指标名称
        /// </summary>
        public string? Zbmc { get; set; }
        /// <summary>
        /// 指标主题
        /// </summary>
        public string? Zbzt { get; set; }
        /// <summary>
        /// 指标算法
        /// </summary>
        public string? Zbsf { get; set; }
        /// <summary>
        /// 指标元数据
        /// </summary>
        public string? Zbmeta { get; set; }
        /// <summary>
        /// 父指标ID
        /// </summary>
        public long? Fid { get; set; }
        /// <summary>
        /// 指标查询算法 指标查询的SELECT语句
        /// </summary>
        public string? Zbcxsf { get; set; }
        /// <summary>
        /// 明细_指标元数据
        /// </summary>
        public string? JsmxZbmeta { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int? Xsxh { get; set; }
        /// <summary>
        /// 指标说明
        /// </summary>
        public string? Zbsm { get; set; }
    }
}

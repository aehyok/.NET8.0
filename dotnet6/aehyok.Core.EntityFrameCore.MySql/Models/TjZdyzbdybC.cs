using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFramework.MySql.Models
{
    /// <summary>
    /// 自定义指标定义表_参数值
    /// </summary>
    public partial class TjZdyzbdybC
    {
        /// <summary>
        /// 参数ID
        /// </summary>
        public long Csid { get; set; }
        /// <summary>
        /// 自定义指标ID
        /// </summary>
        public long? Zbid { get; set; }
        /// <summary>
        /// 单位ID
        /// </summary>
        public long? Dwid { get; set; }
        /// <summary>
        /// 参数存放已经保存的条件值
        /// </summary>
        public string? Cs { get; set; }
        /// <summary>
        /// 按照参数值生成的自定义指标SQL
        /// </summary>
        public string? Querysql { get; set; }
    }
}

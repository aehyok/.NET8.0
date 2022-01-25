using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFramework.MySql.Models
{
    /// <summary>
    /// 页面集
    /// </summary>
    public partial class MdPageset
    {
        /// <summary>
        /// GUID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 页面标题
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// 显示模式0:单栏单指标1:双栏单指标2:双栏双指标
        /// </summary>
        public int? Displaymode { get; set; }
        /// <summary>
        /// 默认开始日期
        /// </summary>
        public string? Defaultstartdate { get; set; }
        /// <summary>
        /// 默认结束日期
        /// </summary>
        public string? Defaultenddate { get; set; }
        /// <summary>
        /// 默认单位
        /// </summary>
        public string? Defaultdw { get; set; }
        /// <summary>
        /// 第一个指标的元数据
        /// </summary>
        public string? Zbmeta1 { get; set; }
        /// <summary>
        /// 第二个指示的元数据
        /// </summary>
        public string? Zbmeta2 { get; set; }
        /// <summary>
        /// 第一个栏目的元数据
        /// </summary>
        public string? Lmmeta1 { get; set; }
        /// <summary>
        /// 第二个栏目的元数据
        /// </summary>
        public string? Lmmeta2 { get; set; }
        /// <summary>
        /// 单位代码
        /// </summary>
        public string? Dwdm { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int? Displayorder { get; set; }
        /// <summary>
        /// R:报表指标   Z:自定义指标
        /// </summary>
        public string? Zbtype { get; set; }
        /// <summary>
        /// 自定义指标算法
        /// </summary>
        public string? Zdyzbsf { get; set; }
        /// <summary>
        /// 分组ID
        /// </summary>
        public long? Groupid { get; set; }
    }
}

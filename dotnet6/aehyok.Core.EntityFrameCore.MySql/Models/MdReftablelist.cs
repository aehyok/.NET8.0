using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFramework.MySql.Models
{
    public partial class MdReftablelist
    {
        public long Rtid { get; set; }
        public string? Namespace { get; set; }
        public string? Reftablename { get; set; }
        public string? Reftablelevelformat { get; set; }
        public string? Description { get; set; }
        /// <summary>
        /// 数据下载模式 1：一次性全部下载 2：分级下载
        /// </summary>
        public int? Downloadmode { get; set; }
        /// <summary>
        /// 代码表模式：1：正常模式 2参数比较下载模式
        /// </summary>
        public int? Reftablemode { get; set; }
        /// <summary>
        /// 单位代码
        /// </summary>
        public string? Dwdm { get; set; }
        /// <summary>
        /// 是否在使用代码表时隐藏代码 0:不隐藏  1隐藏
        /// </summary>
        public int? Hidecode { get; set; }
    }
}

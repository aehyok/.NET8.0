using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFramework.MySql.Models
{
    /// <summary>
    /// 表的列定义
    /// </summary>
    public partial class MdTablecolumn
    {
        public long Tcid { get; set; }
        public long? Tid { get; set; }
        /// <summary>
        /// 列名称
        /// </summary>
        public string? Columnname { get; set; }
        /// <summary>
        /// 可否为空
        /// </summary>
        public string? Isnullable { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string? Type { get; set; }
        /// <summary>
        /// 小数位
        /// </summary>
        public int? Precision { get; set; }
        public int? Scale { get; set; }
        /// <summary>
        /// 数据长度
        /// </summary>
        public int? Length { get; set; }
        /// <summary>
        /// 引用代码表
        /// </summary>
        public string? Refdmb { get; set; }
        /// <summary>
        /// 分级代码表格式
        /// </summary>
        public string? Dmblevelformat { get; set; }
        /// <summary>
        /// 安全级别
        /// </summary>
        public int? Secretlevel { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string? Displaytitle { get; set; }
        /// <summary>
        /// 显示格式
        /// </summary>
        public string? Displayformat { get; set; }
        /// <summary>
        /// 显示列宽
        /// </summary>
        public int? Displaylength { get; set; }
        /// <summary>
        /// 显示行数
        /// </summary>
        public int? Displayheight { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int? Displayorder { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public int? Candisplay { get; set; }
        /// <summary>
        /// 显示宽度
        /// </summary>
        public int? Colwidth { get; set; }
        /// <summary>
        /// 单位代码
        /// </summary>
        public string? Dwdm { get; set; }
        /// <summary>
        /// 概念标签
        /// </summary>
        public string? Ctag { get; set; }
        /// <summary>
        /// 参考用词表，用于规范录入
        /// </summary>
        public string? Refwordtb { get; set; }
    }
}

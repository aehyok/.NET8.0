using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFramework.MySql.Models
{
    /// <summary>
    /// 表定义
    /// </summary>
    public partial class MdTable
    {
        public long Tid { get; set; }
        /// <summary>
        /// 命名空间
        /// </summary>
        public string? Namespace { get; set; }
        /// <summary>
        /// 表名称
        /// </summary>
        public string? Tablename { get; set; }
        /// <summary>
        /// 表类型 VIEW TABLE
        /// </summary>
        public string? Tabletype { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string? Displayname { get; set; }
        /// <summary>
        /// 主键字段
        /// </summary>
        public string? Mainkey { get; set; }
        /// <summary>
        /// 单位代码
        /// </summary>
        public string? Dwdm { get; set; }
        /// <summary>
        /// 安全判别函数名
        /// </summary>
        public string? Secretfun { get; set; }
        /// <summary>
        /// EXTSECRET
        /// </summary>
        public string? Extsecret { get; set; }
        /// <summary>
        /// 本表的资源类型
        /// </summary>
        public string? Restype { get; set; }
    }
}

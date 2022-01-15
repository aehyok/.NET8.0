using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFrameCore.MySql.Models
{
    /// <summary>
    /// 计算字段
    /// </summary>
    public partial class MdComputecolumn
    {
        /// <summary>
        /// GUID
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// 字段名称
        /// </summary>
        public string? Columname { get; set; }
        /// <summary>
        /// 字段表达式
        /// </summary>
        public string Columnexp { get; set; } = null!;
        /// <summary>
        /// 所在表名称
        /// </summary>
        public string Tablename { get; set; } = null!;
        /// <summary>
        /// 查询模型全称：命名空间 .查询模型
        /// </summary>
        public string? Viewname { get; set; }
        /// <summary>
        /// 元数据定义
        /// </summary>
        public string Columnmeta { get; set; } = null!;
        /// <summary>
        /// 字段说明
        /// </summary>
        public string Columndes { get; set; } = null!;
        /// <summary>
        /// 是否公用 0:个人使用  1.公用
        /// </summary>
        public int? Ispublic { get; set; }
        /// <summary>
        /// 私人用户ID
        /// </summary>
        public long Userid { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateOnly Createdate { get; set; }
    }
}

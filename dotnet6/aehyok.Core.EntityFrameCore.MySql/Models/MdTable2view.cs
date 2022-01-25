using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFramework.MySql.Models
{
    /// <summary>
    /// 表关联到的查询模型的定义
    /// </summary>
    public partial class MdTable2view
    {
        /// <summary>
        /// GUID
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// 表ID
        /// </summary>
        public long? Tid { get; set; }
        /// <summary>
        /// 关联到的查询模型名称
        /// </summary>
        public string? Viewname { get; set; }
        /// <summary>
        /// 关联条件定义
        /// </summary>
        public string? Conditionstr { get; set; }
        /// <summary>
        /// 限制条件
        /// </summary>
        public string? Confine { get; set; }
    }
}

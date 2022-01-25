using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFramework.MySql.Models
{
    public partial class MdViewtablecolumn
    {
        public long Vtcid { get; set; }
        public long? Vtid { get; set; }
        public long? Tcid { get; set; }
        /// <summary>
        /// 可做条件
        /// </summary>
        public int? Canconditionshow { get; set; }
        /// <summary>
        /// 可做结果
        /// </summary>
        public int? Canresultshow { get; set; }
        /// <summary>
        /// 是否固定查询项
        /// </summary>
        public int? Fixqueryitem { get; set; }
        /// <summary>
        /// 是否可以修改（只用于审核类型的VIEW）
        /// </summary>
        public int? Canmodify { get; set; }
        /// <summary>
        /// 默认显示字段
        /// </summary>
        public int? Defaultshow { get; set; }
        /// <summary>
        /// 单位代码
        /// </summary>
        public string? Dwdm { get; set; }
        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int? Displayorder { get; set; }
    }
}

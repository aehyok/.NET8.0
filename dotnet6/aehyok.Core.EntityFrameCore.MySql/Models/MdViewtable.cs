using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFramework.MySql.Models
{
    /// <summary>
    /// 视图表
    /// </summary>
    public partial class MdViewtable
    {
        public long Vtid { get; set; }
        public long? Fatherid { get; set; }
        public long? Viewid { get; set; }
        public long? Tid { get; set; }
        /// <summary>
        /// M:表示是视图中的主表   F：表示是视图中的附表
        /// </summary>
        public string? Tabletype { get; set; }
        /// <summary>
        /// 副表与主表的连接关系表达式（ 主表时为空串）
        /// </summary>
        public string? Tablerelation { get; set; }
        /// <summary>
        /// 本表是否可以做为查询的条件字段
        /// </summary>
        public string? Cancondition { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string? Displayname { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int? Displayorder { get; set; }
        /// <summary>
        /// 单位代码
        /// </summary>
        public string? Dwdm { get; set; }
        /// <summary>
        /// 优先级(数值越小优先级越高)
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// 显示方式，0: Grid方式，1:Form方式
        /// </summary>
        public int? Displaytype { get; set; }
        /// <summary>
        /// 集成应用，可多项。(为空表示相应资源均会集成）
        /// </summary>
        public string? Integratedapp { get; set; }
    }
}

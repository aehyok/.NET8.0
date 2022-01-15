using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFrameCore.MySql.Models
{
    /// <summary>
    /// 节点定义
    /// </summary>
    public partial class MdNode
    {
        /// <summary>
        /// 节点ID
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// 节点名称
        /// </summary>
        public string? Nodename { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string? Displaytitle { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string? Descript { get; set; }
        /// <summary>
        /// 节点编码
        /// </summary>
        public string? Dwdm { get; set; }
        /// <summary>
        /// 节点类型
        /// </summary>
        public string? Systemtype { get; set; }
        /// <summary>
        /// 顺序
        /// </summary>
        public int? Displayorder { get; set; }
    }
}

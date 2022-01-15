using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFrameCore.MySql.Models
{
    /// <summary>
    /// 模型关联模型的分组定义
    /// </summary>
    public partial class MdView2viewgroup
    {
        /// <summary>
        /// GUID
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// 模型ID
        /// </summary>
        public long? Viewid { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int? Displayorder { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string? Displaytitle { get; set; }
    }
}

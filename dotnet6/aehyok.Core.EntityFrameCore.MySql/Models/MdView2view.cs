using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFrameCore.MySql.Models
{
    /// <summary>
    /// 查询模型到查询模型之间的关联定义
    /// </summary>
    public partial class MdView2view
    {
        /// <summary>
        /// GUID
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// 模型ID
        /// </summary>
        public long Viewid { get; set; }
        /// <summary>
        /// 关联到的模型名称
        /// </summary>
        public string? Targetviewname { get; set; }
        /// <summary>
        /// 关联条件
        /// </summary>
        public string? Relationstr { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int? Displayorder { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string? Displaytitle { get; set; }
        /// <summary>
        /// 分组ID
        /// </summary>
        public string? Groupid { get; set; }
        /// <summary>
        /// 用于保存一个METADATA信息
        /// </summary>
        public string? V2vmeta { get; set; }
    }
}

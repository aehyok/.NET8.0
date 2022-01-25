using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFramework.MySql.Models
{
    /// <summary>
    /// 查询模型关联注册的应用
    /// </summary>
    public partial class MdView2app
    {
        public string Id { get; set; } = null!;
        /// <summary>
        /// 视图ID
        /// </summary>
        public string? Viewid { get; set; }
        /// <summary>
        /// 显示标题
        /// </summary>
        public string Title { get; set; } = null!;
        /// <summary>
        /// 集成应用名称
        /// </summary>
        public string Integratedapp { get; set; } = null!;
        /// <summary>
        /// 显示区高度
        /// </summary>
        public int Displayheight { get; set; }
        /// <summary>
        /// 注册显示区URL
        /// </summary>
        public string? Url { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int? Displayorder { get; set; }
        /// <summary>
        /// 其它元数据定义
        /// </summary>
        public string? Meta { get; set; }
    }
}

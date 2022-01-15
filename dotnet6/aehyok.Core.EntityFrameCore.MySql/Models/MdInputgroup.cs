using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFrameCore.MySql.Models
{
    /// <summary>
    /// 录入数据分组
    /// </summary>
    public partial class MdInputgroup
    {
        /// <summary>
        /// 组ID
        /// </summary>
        public long IvgId { get; set; }
        /// <summary>
        /// 录入模型ID
        /// </summary>
        public long IvId { get; set; }
        /// <summary>
        /// 组显示名称
        /// </summary>
        public string? Displaytitle { get; set; }
        /// <summary>
        /// 组顺序
        /// </summary>
        public int? Displayorder { get; set; }
        /// <summary>
        /// 分组类型（DEFAULT:正常     APPREG:应用注册）
        /// </summary>
        public string? Grouptype { get; set; }
        /// <summary>
        /// 如果是应用注册类型，则此处为URL
        /// </summary>
        public string? Appregurl { get; set; }
        /// <summary>
        /// 分组参数
        /// </summary>
        public string? Groupcs { get; set; }
    }
}

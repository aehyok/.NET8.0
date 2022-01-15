using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFrameCore.MySql.Models
{
    /// <summary>
    /// 录入模型子模型
    /// </summary>
    public partial class MdInputviewchild
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 录入模型ID
        /// </summary>
        public long? IvId { get; set; }
        /// <summary>
        /// 子模型ID
        /// </summary>
        public long? CivId { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public string? Param { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int? Displayorder { get; set; }
        /// <summary>
        /// 展开显示条件
        /// </summary>
        public string? Showcondition { get; set; }
        /// <summary>
        /// 数据选择方式  0：CheckBox  1：RadioButton?
        /// </summary>
        public int? Selectmode { get; set; }
    }
}

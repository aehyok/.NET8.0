using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFrameCore.MySql.Models
{
    /// <summary>
    /// 录入模型对应的数据表结构定义
    /// </summary>
    public partial class MdInputtable
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 表名称
        /// </summary>
        public string? Tablename { get; set; }
        /// <summary>
        /// 录入模型ID
        /// </summary>
        public long? IvId { get; set; }
        /// <summary>
        /// 是否锁定结构。 0：未锁定，可以修改结构  1：锁定，不可修改结构
        /// </summary>
        public int? Islock { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string? Tabletitle { get; set; }
        /// <summary>
        /// 顺序
        /// </summary>
        public int? Displayorder { get; set; }
        /// <summary>
        /// 保存模式  （Normal 正常   OnlyInsert 仅插入 ，默认是Normal模式
        /// </summary>
        public string? Savemode { get; set; }
    }
}

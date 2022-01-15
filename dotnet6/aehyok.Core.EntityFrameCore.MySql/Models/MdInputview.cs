using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFrameCore.MySql.Models
{
    /// <summary>
    /// 录入视图
    /// </summary>
    public partial class MdInputview
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long IvId { get; set; }
        /// <summary>
        /// 命名空间
        /// </summary>
        public string? Namespace { get; set; }
        /// <summary>
        /// 录入视图名称
        /// </summary>
        public string? IvName { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string? Displayname { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int? Displayorder { get; set; }
        /// <summary>
        /// 录入视图参数
        /// </summary>
        public string? IvCs { get; set; }
        /// <summary>
        /// 录入对应的表名称
        /// </summary>
        public long? Tid { get; set; }
        /// <summary>
        /// 删除记录规则
        /// </summary>
        public string? Delrule { get; set; }
        /// <summary>
        /// 单位代码
        /// </summary>
        public string? Dwdm { get; set; }
        /// <summary>
        /// 集成应用
        /// </summary>
        public string? Integratedapp { get; set; }
        /// <summary>
        /// 资源类型
        /// </summary>
        public string? Restype { get; set; }
        /// <summary>
        /// 写入数据表前执行命令
        /// </summary>
        public string? Beforewrite { get; set; }
        /// <summary>
        /// 写入数据表后执行命令
        /// </summary>
        public string? Afterwrite { get; set; }
    }
}

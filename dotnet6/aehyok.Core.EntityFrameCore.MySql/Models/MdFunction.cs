using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFrameCore.MySql.Models
{
    /// <summary>
    /// 函数定义表
    /// </summary>
    public partial class MdFunction
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Funid { get; set; }
        /// <summary>
        /// 命名空间
        /// </summary>
        public string? Namespace { get; set; }
        /// <summary>
        /// 单位代码
        /// </summary>
        public string? Dwdm { get; set; }
        /// <summary>
        /// 1.计算函数  2统计函数
        /// </summary>
        public int? Type { get; set; }
        /// <summary>
        /// 函数名
        /// </summary>
        public string? Functionname { get; set; }
        /// <summary>
        /// 返回类型：VARCHAR,NUMBER,DATETIME
        /// </summary>
        public string? Resulttype { get; set; }
        /// <summary>
        /// 调用参数元数据， 格式&lt;name&gt;xxx&lt;/name&gt;&lt;type&gt;xxx&lt;/type&gt;,&lt;name&gt;xxx&lt;/name&gt;&lt;type&gt;xxx&lt;/type&gt;
        /// </summary>
        public string? Parameta { get; set; }
        /// <summary>
        /// 函数显示名称
        /// </summary>
        public string? Displayname { get; set; }
        /// <summary>
        /// 函数说明描述
        /// </summary>
        public string? Description { get; set; }
    }
}

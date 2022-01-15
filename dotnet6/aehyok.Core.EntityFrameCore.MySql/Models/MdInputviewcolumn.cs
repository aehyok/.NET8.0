using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFrameCore.MySql.Models
{
    /// <summary>
    /// 录入视图列信息
    /// </summary>
    public partial class MdInputviewcolumn
    {
        /// <summary>
        /// 录入视图列ID
        /// </summary>
        public long IvcId { get; set; }
        /// <summary>
        /// 录入视图ID
        /// </summary>
        public long? IvId { get; set; }
        /// <summary>
        /// 对应的表列ID
        /// </summary>
        public long? Tcid { get; set; }
        /// <summary>
        /// 单位代码
        /// </summary>
        public string? Dwdm { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string? Inputdefault { get; set; }
        /// <summary>
        /// 本字段在录入时的检验规则及自动完成规则
        /// </summary>
        public string? Inputrule { get; set; }
        /// <summary>
        /// 本字段是否可修改的规则
        /// </summary>
        public string? Caneditrule { get; set; }
        /// <summary>
        /// 是否可显示 {‘Y&apos;,&apos;N&apos;}
        /// </summary>
        public string? Candisplay { get; set; }
        /// <summary>
        /// 字段名
        /// </summary>
        public string? Columnname { get; set; }
        /// <summary>
        /// 字段顺序
        /// </summary>
        public int? Columnorder { get; set; }
        /// <summary>
        /// 字段类型
        /// </summary>
        public string? Columntype { get; set; }
        /// <summary>
        /// 是否只读   1：只读  0：可修改
        /// </summary>
        public int? Readonly { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string? Displayname { get; set; }
        /// <summary>
        /// 是否计算字段 1.是计算字段 0：不是
        /// </summary>
        public int? Iscompute { get; set; }
        /// <summary>
        /// 列宽
        /// </summary>
        public int? Columnwidth { get; set; }
        /// <summary>
        /// 列高
        /// </summary>
        public int? Columnheight { get; set; }
        /// <summary>
        /// 显示位置: 0:默认 1: LEFT 2:CENTER 3:RIGHT
        /// </summary>
        public int? Textalignment { get; set; }
        /// <summary>
        /// 修改录入格式
        /// </summary>
        public string? Editformat { get; set; }
        /// <summary>
        /// 显示格式
        /// </summary>
        public string? Displayformat { get; set; }
        /// <summary>
        /// 写入的表名,当此字段为空时,默认为MD_INPUTVIEW参数中定义的TABLE.
        /// </summary>
        public string? Tablename { get; set; }
        /// <summary>
        /// 所在的分组ID
        /// </summary>
        public long? IvgId { get; set; }
        /// <summary>
        /// 是否必填   0：非必填   1：必填
        /// </summary>
        public int? Required { get; set; }
        /// <summary>
        /// 注释文本
        /// </summary>
        public string? Tooltip { get; set; }
        /// <summary>
        /// 数据变更后的相应处理
        /// </summary>
        public string? Datachangedevent { get; set; }
        /// <summary>
        /// 录入的最大长度（小于0为不限制长度，大于0为限制长度）
        /// </summary>
        public int? Maxlength { get; set; }
        /// <summary>
        /// 是否默认显示状态（1为显示 2为不显示）
        /// </summary>
        public int? Defaultshow { get; set; }
    }
}

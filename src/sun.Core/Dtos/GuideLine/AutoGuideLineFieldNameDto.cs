using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace sun.Core.Dtos.GuideLine
{
    /// <summary>
    /// 指标字段名称元数据定义
    /// </summary>
    public class AutoGuideLineFieldNameDto
    {

        /// <summary>
        /// 本字段是否可隐藏
        /// </summary>
        [DataMember]
        public bool CanHide { get; set; }


        /// <summary>
        /// 显示格式
        /// </summary>
        [DataMember]
        public string DisplayFormat { get; set; }


        /// <summary>
        /// 字段名称
        /// </summary>
        [DataMember]
        public string FieldName { get; set; }


        /// <summary>
        /// 显示名称
        /// </summary>
        /// 
        [DataMember]
        public string DisplayTitle { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        /// 
        [DataMember]
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 显示宽度
        /// </summary>
        /// 
        [DataMember]
        public int DisplayWidth { get; set; }

        /// <summary>
        /// 是否居中
        /// </summary>
        /// 
        [DataMember]
        public string TextAlign { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        [DataMember]
        public string GroupName { get; set; }

        /// <summary>
        /// 渲染类型
        /// </summary>
        [DataMember]
        public string EditType { get; set; }

        /// <summary>
        /// 渲染模板
        /// </summary>
        [DataMember]
        public string Render { get; set; }

        public AutoGuideLineFieldNameDto()
        {
        }

        public AutoGuideLineFieldNameDto(string _fname, string _title, int _order, int _width, string _align, string _format, bool _hide, string _groupName)
        {
            FieldName = _fname;
            DisplayTitle = _title;
            DisplayOrder = _order;
            DisplayWidth = _width;
            TextAlign = _align;
            DisplayFormat = _format;
            CanHide = _hide;
            GroupName = _groupName;
        }
        public AutoGuideLineFieldNameDto(string _fname, string _title, int _order, int _width, string _align, string _format, bool _hide)
        {
            FieldName = _fname;
            DisplayTitle = _title;
            DisplayOrder = _order;
            DisplayWidth = _width;
            TextAlign = _align;
            DisplayFormat = _format;
            CanHide = _hide;
        }


    }
}

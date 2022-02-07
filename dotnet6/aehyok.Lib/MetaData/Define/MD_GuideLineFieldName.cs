using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Define
{
    /// <summary>
    /// 指标字段名称元数据定义
    /// 
    /// Create by: Lintx
    /// </summary>
    public class MD_GuideLineFieldName
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        [DataMember]
        public string Id { get; set; }

        /// <summary>
        /// 本字段是否可隐藏
        /// </summary>
        [DataMember]
        public string CanHide { get; set; }


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
        public MD_GuideLineFieldName()
        {
        }

        public MD_GuideLineFieldName(string _fname, string _title, int _order, int _width, string _align, string _format, string _hide, string _groupName)
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
        public MD_GuideLineFieldName(string _fname, string _title, int _order, int _width, string _align, string _format, string _hide)
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

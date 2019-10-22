using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Core.Data.Entity.GuideLine
{
    [DataContract]
    public class MD_GuideLineFieldGroup
    {
        /// <summary>
        /// 字段组下包含的显示字段列表
        /// </summary>
        /// 
        [DataMember]
        public List<MD_GuideLineFieldName> Fields { get; set; }
        /// <summary>
        /// 字段组名称
        /// </summary>
        /// 
        [DataMember]
        public string GroupName { get; set; }

        /// <summary>
        /// 显示标题
        /// </summary>
        /// 
        [DataMember]
        public string DisplayTitle { get; set; }

        /// <summary>
        /// 字段对齐
        /// </summary>
        /// 
        [DataMember]
        public string TextAlign { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        /// 
        [DataMember]
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 是否可隐藏
        /// </summary>
        /// 
        [DataMember]
        public bool CanHide { get; set; }

        /// <summary>
        /// 默认状态　　HIDE:隐藏　SHOW:显示
        /// </summary>
        /// 
        [DataMember]
        public string DefaultStatus { get; set; }

        public MD_GuideLineFieldGroup() { }

        public MD_GuideLineFieldGroup(string name, string title, string align, int order, bool hide, string status)
        {
            GroupName = name;
            DisplayTitle = title;
            TextAlign = align;
            DisplayOrder = order;
            CanHide = hide;
            DefaultStatus = status;
        }
    }
}

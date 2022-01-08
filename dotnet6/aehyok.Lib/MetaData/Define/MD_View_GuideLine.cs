using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Define
{
    /// <summary>
    /// 查询模型关联到指标元数据定义
    /// 
    /// Create by:Lintx
    /// </summary>
    public class MD_View_GuideLine
    {
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public int DisplayOrder { get; set; }
        [DataMember]
        public string DisplayTitle { get; set; }
        [DataMember]
        public string ViewID { get; set; }
        [DataMember]
        public string TargetGuideLineID { get; set; }
        [DataMember]
        public string RelationParam { get; set; }

        public override string ToString()
        {
            return DisplayTitle;
        }
    }
}

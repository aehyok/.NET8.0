using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Define
{
    /// <summary>
    /// 查询模型关联到另一个查询模型元数据定义
    /// 
    /// Create by: Lintx
    /// </summary>
    public class MD_View2View
    {
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public string RelationString { get; set; }
        [DataMember]
        public int DisplayOrder { get; set; }
        [DataMember]
        public string DisplayTitle { get; set; }
        [DataMember]
        public string ViewGroupID { get; set; }
        [DataMember]
        public string TargetViewName { get; set; }
        [DataMember]
        public string QueryModelID { get; set; }

        public override string ToString()
        {
            return this.DisplayTitle;
        }
    }
}

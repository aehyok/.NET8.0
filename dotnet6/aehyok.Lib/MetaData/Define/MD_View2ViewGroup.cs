using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Define
{
    /// <summary>
    /// 查询模型关联到查询模型的分组的元数据定义
    /// 
    /// Create by:Lintx
    /// </summary>
    public class MD_View2ViewGroup
    {
        [DataMember]
        public IList<MD_View2View> View2Views { get; set; }
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public int DisplayOrder { get; set; }
        [DataMember]
        public string DisplayTitle { get; set; }
        [DataMember]
        public string QueryModelID { get; set; }

        public override string ToString()
        {
            return this.DisplayTitle;
        }
    }
}

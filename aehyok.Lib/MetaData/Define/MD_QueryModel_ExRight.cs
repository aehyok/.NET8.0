using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Define
{
    /// <summary>
    /// 查询模型扩展授权定义
    /// 
    /// Create by: Lintx
    /// </summary>
    public class MD_QueryModel_ExRight
    {
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public string RightName { get; set; }
        [DataMember]
        public string RightTitle { get; set; }
        [DataMember]
        public string ModelID { get; set; }
        [DataMember]
        public string FatherRightID { get; set; }
        [DataMember]
        public int DisplayOrder { get; set; }

        public override string ToString()
        {
            return RightTitle;
        }
    }
}

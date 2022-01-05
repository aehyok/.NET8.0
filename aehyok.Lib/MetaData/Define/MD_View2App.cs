using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Define
{
    /// <summary>
    /// 查询模型关联到应用元数据定义
    /// 
    /// Create by:Lintx
    /// </summary>
    public class MD_View2App
    {
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public string ViewID { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string RegURL { get; set; }
        [DataMember]
        public string AppName { get; set; }
        [DataMember]
        public string Meta { get; set; }
        [DataMember]
        public int DisplayOrder { get; set; }
        [DataMember]
        public int DisplayHeight { get; set; }

        public override string ToString()
        {
            if (Title != null)
            {
                return Title;
            }
            else
            {
                return ID;
            }
        }
    }
}

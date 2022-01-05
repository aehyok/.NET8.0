using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Define
{
    /// <summary>
    /// 查询模型组中的项 元数据定义
    /// 
    /// Create by:Lintx
    /// </summary>
    public class MD_QueryModelGroupItem
    {
        /// <summary>
        /// 显示顺序
        /// </summary>
        [DataMember]
        public int ItemDisplayOrder { get; set; }
        /// <summary>
        /// 对应的查询主题
        /// </summary>
        [DataMember]
        public string QueryModelGroupID { get; set; }

        /// <summary>
        /// 节点编号
        /// </summary>
        [DataMember]
        public string ItemDWDM { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        [DataMember]
        public string ID { get; set; }

        [DataMember]
        public MD_QueryModel QueryModel { get; set; }
    }
}

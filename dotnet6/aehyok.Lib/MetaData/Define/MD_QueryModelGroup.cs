using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Define
{
    /// <summary>
    /// 查询模型组元数据定义
    /// 
    /// Create by: Lintx
    /// </summary>
    public class MD_QueryModelGroup
    {
        /// <summary>
        /// 所在命名空间
        /// </summary>
        [DataMember]
        public MD_Namespace Namespace { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        [DataMember]
        public string ID { get; set; }

        /// <summary>
        /// 所包含的查询模型
        /// </summary>
        [DataMember]
        public IList<MD_QueryModelGroupItem> QueryModels { get; set; }

        /// <summary>
        /// 查询模型组名称
        /// </summary>
        [DataMember]
        public string GroupName { get; set; }

        /// <summary>
        /// 显示标题
        /// </summary>
        [DataMember]
        public string DisplayTitle { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [DataMember]
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 是否固定查询组(兼容老版本)
        /// </summary>
        [DataMember]
        public bool IsFixQuery { get; set; }
        /// <summary>
        /// 是否关联查询组(兼容老版本)
        /// </summary>
        [DataMember]
        public bool IsRelationQuery { get; set; }

        /// <summary>
        /// 是否审核模型组(兼容老版本)
        /// </summary>
        [DataMember]
        public bool IsDataAuditing { get; set; }

        /// <summary>
        /// 节点编号
        /// </summary>
        [DataMember]
        public string DWDM { get; set; }
    }
}

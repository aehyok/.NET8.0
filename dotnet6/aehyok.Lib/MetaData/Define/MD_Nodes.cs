using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Define
{
    /// <summary>
    /// 命名空间元数据定义
    /// 
    /// Create by: Lintx
    /// </summary>
    public class MD_Nodes
    {
        /// <summary>
        /// 包含的命名空间
        /// </summary>
        [DataMember]
        public List<MD_Namespace> NameSpaces { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        [DataMember]
        public string NodeName { get; set; }

        /// <summary>
        /// 节点编码
        /// </summary>
        [DataMember]
        public string DWDM { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [DataMember]
        public string DisplayTitle { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [DataMember]
        public string Descript { get; set; }

        /// <summary>
        /// 节点ID
        /// </summary>
        [DataMember]
        public string ID { get; set; }

        public MD_Nodes()
        {
        }

        public MD_Nodes(string _nodename, string _displayTitle, string _dwdm)
        {
            this.NodeName = _nodename;
            this.DisplayTitle = _displayTitle;
            this.DWDM = _dwdm;
        }

        public MD_Nodes(string _id, string _nodename, string _displayTitle, string _descript, string _dwdm)
        {
            this.ID = _id;
            this.NodeName = _nodename;
            this.DisplayTitle = _displayTitle;
            this.DWDM = _dwdm;
            this.Descript = _descript;
        }

        public override string ToString()
        {
            return DisplayTitle;
        }

    }
}

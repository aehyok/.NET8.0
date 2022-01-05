using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Define
{
    /// <summary>
    /// 概念项元数据定义
    /// 
    /// Create by:Lintx
    /// </summary>
    public class MD_ConceptItem
    {
        public MD_ConceptItem() { }

        public MD_ConceptItem(string _ctag, string _des, string _groupname, string _crule, string _dwdm, int _order)
        {
            this.CTag = _ctag;
            this.Description = _des;
            this.GroupName = _groupname;
            this.CRule = _crule;
            this.DWDM = _dwdm;
            this.DisplayOrder = _order;
        }
        /// <summary>
        /// 描述
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// 组名称
        /// </summary>
        [DataMember]
        public string GroupName { get; set; }

        /// <summary>
        /// 规则
        /// </summary>
        [DataMember]
        public string CRule { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        [DataMember]
        public int DisplayOrder { get; set; }
        /// <summary>
        /// 概念标签　
        /// </summary>
        [DataMember]
        public string CTag { get; set; }

        /// <summary>
        /// 此概念属于的节点代码
        /// </summary>
        [DataMember]
        public string DWDM { get; set; }

        public override string ToString()
        {
            return string.Format("{0}", CTag, Description);
        }
    }
}

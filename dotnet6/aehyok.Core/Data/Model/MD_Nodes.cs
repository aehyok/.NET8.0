using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Data.Model
{
    public class MD_Nodes
    {
        /// <summary>
        /// 包含的命名空间
        /// </summary>
        
        public List<MD_Namespace> NameSpaces { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        
        public string NodeName { get; set; }

        /// <summary>
        /// 节点编码
        /// </summary>
        
        public string NodeCode { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        
        public string DisplayTitle { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        
        public string Descript { get; set; }

        /// <summary>
        /// 节点ID
        /// </summary>
        
        public string ID { get; set; }

        /// <summary>
        /// 系统类型
        /// </summary>
        
        public string SystemType { get; set; }

        public MD_Nodes()
        {
        }

        public MD_Nodes(string _nodename, string _displayTitle, string nodeCode)
        {
            this.NodeName = _nodename;
            this.DisplayTitle = _displayTitle;
            this.NodeCode = nodeCode;
        }

        public MD_Nodes(string _id, string _nodename, string _displayTitle, string _descript, string nodeCode, string systemType)
        {
            this.ID = _id;
            this.NodeName = _nodename;
            this.DisplayTitle = _displayTitle;
            this.NodeCode = nodeCode;
            this.Descript = _descript;
            this.SystemType = systemType;
        }

        public override string ToString()
        {
            return DisplayTitle;
        }
    }
}

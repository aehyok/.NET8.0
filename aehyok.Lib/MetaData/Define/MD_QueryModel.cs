using System;
using System.Collections.Generic;
using System.Text;

namespace aehyok.Lib.MetaData.Define
{
    /// <summary>
    /// 查询模型元数据定义
    /// 
    /// Create by: Lintx
    /// </summary>
    public class MD_QueryModel
    {
        public MD_QueryModel() { }
        public MD_QueryModel(string _qid, string _ns, string _qName, string _des, string _title, string _dw, bool _isFix, bool _isRelation,
                bool _isda, int _order, string _interface)
        {
            QueryModelID = _qid;
            NamespaceName = _ns;
            QueryModelName = _qName;
            Description = _des;
            DisplayTitle = _title;
            DWDM = _dw;
            IsFixQuery = _isFix;
            IsRelationQuery = _isRelation;
            IsDataAuditing = _isda;
            DisplayOrder = _order;
            QueryInterface = _interface;
            FullName = string.Format("{0}.{1}", NamespaceName, QueryModelName);
        }

        /// <summary>
        /// 本模型使用的查询接口
        /// </summary>        
        public string QueryInterface { get; set; }


        /// <summary>
        /// 命名空间
        /// </summary>        
        public MD_Namespace Namespace { get; set; }

        public string NamespaceName { get; set; }

        
        public string FullName { get; set; }

        /// <summary>
        /// 主表
        /// </summary>
        
        public MD_ViewTable MainTable { get; set; }


        /// <summary>
        /// 子表列表
        /// </summary>
        
        public IList<MD_ViewTable> ChildTables { get; set; }

        
        public IList<MD_View2ViewGroup> View2ViewGroup { get; set; }

        
        public IList<MD_View_GuideLine> View2GuideLines { get; set; }

        
        public IList<MD_View2App> View2Application { get; set; }

        /// <summary>
        /// 查询模型名称
        /// </summary>
        
        public string QueryModelName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        
        public string Description { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        
        public string DisplayTitle { get; set; }

        /// <summary>
        /// 节点编号
        /// </summary>
        
        public String DWDM { get; set; }

        /// <summary>
        /// 是否固定查询
        /// </summary>
        
        public bool IsFixQuery { get; set; }

        /// <summary>
        /// 是否关联查询
        /// </summary>
        
        public bool IsRelationQuery { get; set; }

        /// <summary>
        /// 是否数据审核
        /// </summary>
        
        public bool IsDataAuditing { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 查询模型ID
        /// </summary>
        
        public string QueryModelID { get; set; }

        
        public string EXTMeta { get; set; }

        public override string ToString()
        {
            return DisplayTitle;
        }
    }
}


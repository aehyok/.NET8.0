using aehyok.Lib.MetaData.Define;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Query
{
    public class MDModel_QueryModel
    {
        public MDModel_QueryModel() { }

        public MDModel_QueryModel(string viewid, string ns, string viewname, string descript, string displaytitle, string dwdm, bool is_gdcx, bool is_glcx, bool is_sjsh,
            int displayorder, string icstype)
        {
            // TODO: Complete member initialization
            this.ViewID = viewid;
            this.NameSpaceName = ns;
            this.QueryModelName = viewname;
            this.Description = descript;
            this.DisplayName = displaytitle;
            this.DisplayOrder = displayorder;
            this.DWDM = dwdm;
            this.QueryInterface = icstype;
            this.FullQueryModelName = this.NameSpaceName + "." + this.QueryModelName;
        }

        /// <summary>
        /// 查询接口类型
        /// </summary>
        /// 
        [DataMember]
        public string QueryInterface { get; set; }

        /// <summary>
        /// 查询模型VIEWID
        /// </summary>
        [DataMember]
        public string ViewID { get; set; }

        /// <summary>
        /// 查询模型名称
        /// </summary>
        /// 
        [DataMember]
        public string QueryModelName { get; set; }

        /// <summary>
        /// 查询模型显示名称
        /// </summary>
        /// 
        [DataMember]
        public string DisplayName { get; set; }
        /// <summary>
        /// 命名空间名称
        /// </summary>
        /// 
        [DataMember]
        public string NameSpaceName { get; set; }

        /// <summary>
        /// 查询模型全称
        /// </summary>
        /// 
        [DataMember]
        public string FullQueryModelName { get; set; }

        /// <summary>
        /// 主表定义
        /// </summary>
        /// 
        [DataMember]
        public MDModel_Table MainTable { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        [DataMember]
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 系统代码
        /// </summary>
        [DataMember]
        public string DWDM { get; set; }

        /// <summary>
        /// 子表列表
        /// </summary>
        /// 
        [DataMember]
        public List<MDModel_Table> ChildTables { get; set; }

        /// <summary>
        /// 关联的查询模型组
        /// </summary>
        [DataMember]
        public List<MDModel_View2ViewGroup> View2ViewGroups { get; set; }

        [DataMember]
        public List<MDModel_View2GL> View2GuideLines { get; set; }

        [DataMember]
        public List<MD_View2App> View2Application { get; set; }

        /// <summary>
        /// 子表字典,以TableName为字典键值
        /// </summary>
        [DataMember]
        public Dictionary<string, MDModel_Table> ChildTableDict { get; set; }




    }
}

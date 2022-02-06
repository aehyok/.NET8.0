using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Define
{
    /// <summary>
    /// 指标元数据定义 
    /// 
    /// Create by: Lintx
    /// </summary>
    public class MD_GuideLine
    {
        /// <summary>
        /// 指标ID
        /// </summary>
        /// 
        [DataMember]
        public string ID { get; set; }

        /// <summary>
        /// 指标名称
        /// </summary>
        [DataMember]
        public string GuideLineName { get; set; }

        /// <summary>
        /// 指标所在的组名称
        /// </summary>
        [DataMember]
        public string GroupName { get; set; }

        [DataMember]
        public string GuideLineMethod { get; set; }
        /// <summary>
        /// 父指标ID
        /// </summary>
        [DataMember]
        public string FatherID { get; set; }

        /// <summary>
        /// 指标显示顺序
        /// </summary>
        [DataMember]
        public int DisplayOrder { get; set; }

        [DataMember]
        public string GuideLineMeta { get; set; }
        /// <summary>
        /// 指标说明
        /// </summary>
        [DataMember]
        public string Description { get; set; }
        /// <summary>
        /// 子指标集合
        /// </summary>
        [DataMember]
        public List<MD_GuideLine> Children { get; set; }

        [DataMember]
        public MD_GuideLineGroup MD_GuideLineGroup { get; set; }

        [DataMember]
        public string DetailMeta { get; set; }
        /// <summary>
        /// 指标查询参数集合
        /// </summary>
        [DataMember]
        public List<MD_GuideLineParameter> Parameters { get; set; }

        /// <summary>
        /// 指标查询结果分组集合
        /// </summary>
        [DataMember]
        //public List<MD_GuideLineFieldGroup> ResultGroups { get; set; }
        public List<MD_GuideLineFieldName> ResultGroups { get; set; }
        /// <summary>
        /// 详细信息链接
        /// </summary>
        [DataMember]
        public List<MD_GuideLineDetailDefine> DetailDefines { get; set; }

        [DataMember]
        public string GuideLineQueryMethod { get; set; }

        public MD_GuideLine() { }
        public MD_GuideLine(string id, string name, string groupname, string fatherid, int displayorder, string descript)
        {
            ID = id;
            GuideLineName = name;
            GroupName = groupname;
            FatherID = fatherid;
            DisplayOrder = displayorder;
            Description = descript;
        }
        public MD_GuideLine(string id, string glName, string groupName, string glMethod, string glMeta, string fid, string glQueryMethod,
               string detailMeta, int order, string _des)
        {
            ID = id;
            GuideLineName = glName;
            GroupName = groupName;
            GuideLineMethod = glMethod;
            GuideLineMeta = glMeta + detailMeta;
            FatherID = fid;
            GuideLineQueryMethod = glQueryMethod;
            DetailMeta = detailMeta;
            DisplayOrder = order;
            this.Description = _des;
        }
        public override string ToString()
        {
            return GuideLineName;
        }
    }
}

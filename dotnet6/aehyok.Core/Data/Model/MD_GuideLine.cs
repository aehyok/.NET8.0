using aehyok.Core.Data.Entity.GuideLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Data.Model
{
    public class MD_GuideLine
    {
        /// <summary>
        /// 指标ID
        /// </summary>
        /// 
        
        public string Id { get; set; }

        /// <summary>
        /// 指标名称
        /// </summary>
        
        public string GuideLineName { get; set; }

        /// <summary>
        /// 指标所在的组名称
        /// </summary>
        
        public string GroupName { get; set; }

        
        public string GuideLineMethod { get; set; }
        /// <summary>
        /// 父指标ID
        /// </summary>
        
        public string FatherId { get; set; }

        /// <summary>
        /// 指标显示顺序
        /// </summary>
        
        public int DisplayOrder { get; set; }

        
        public string GuideLineMeta { get; set; }
        /// <summary>
        /// 指标说明
        /// </summary>
        
        public string Description { get; set; }
        /// <summary>
        /// 子指标集合
        /// </summary>
        
        public List<MD_GuideLine> Children { get; set; }

        
        public MD_GuideLineGroup MD_GuideLineGroup { get; set; }

        
        public string DetailMeta { get; set; }
        /// <summary>
        /// 指标查询参数集合
        /// </summary>
        
        public List<MD_GuideLineParameter> Parameters { get; set; }

        /// <summary>
        /// 指标查询结果分组集合
        /// </summary>
        
        public List<MD_GuideLineFieldGroup> ResultGroups { get; set; }
        /// <summary>
        /// 详细信息链接
        /// </summary>
        
        public List<MD_GuideLineDetailDefine> DetailDefines { get; set; }

        
        public string GuideLineQueryMethod { get; set; }

        public MD_GuideLine() { }
        public MD_GuideLine(string id, string name, string groupname, string fatherId, int displayorder, string descript)
        {
            Id = id;
            GuideLineName = name;
            GroupName = groupname;
            FatherId = fatherId;
            DisplayOrder = displayorder;
            Description = descript;
        }
        public MD_GuideLine(string id, string glName, string groupName, string glMethod, string glMeta, string fatherId, string glQueryMethod,
               string detailMeta, int order, string des)
        {
            Id = id;
            GuideLineName = glName;
            GroupName = groupName;
            GuideLineMethod = glMethod;
            GuideLineMeta = glMeta + detailMeta;
            FatherId = fatherId;
            GuideLineQueryMethod = glQueryMethod;
            DetailMeta = detailMeta;
            DisplayOrder = order;
            this.Description = des;
        }
        public override string ToString()
        {
            return GuideLineName;
        }

    }
}

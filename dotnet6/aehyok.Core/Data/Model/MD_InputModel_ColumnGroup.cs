using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aehyok.Core.Data.Model
{
    public class MD_InputModel_ColumnGroup
    {
        /// <summary>
        /// 录入模型Id
        /// </summary>
        
        public string ModelId { get; set; }

        /// <summary>
        /// 录入模型分组Id
        /// </summary>
        
        public string GroupId { get; set; }

        /// <summary>
        /// 分组显示名称
        /// </summary>
        
        public string DisplayTitle { get; set; }

        /// <summary>
        /// 分组显示顺序
        /// </summary>
        
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 分组中字段列表
        /// </summary>
        
        public List<MD_InputModel_Column> Columns { get; set; }

        /// <summary>
        /// 分组类型
        /// </summary>
        
        public string GroupType { get; set; }

        /// <summary>
        /// 应用注册Url
        /// </summary>
        
        public string AppRegUrl { get; set; }

        /// <summary>
        /// 分组中的参数
        /// </summary>
        
        public string GroupParam { get; set; }

        /// <summary>
        /// 模型名称
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return DisplayTitle;
        }

        public MD_InputModel_ColumnGroup() { }

        public MD_InputModel_ColumnGroup(string groupId, string modelId, string displayTitle, int displayOrder)
        {
            ModelId = modelId;
            GroupId = groupId;
            DisplayTitle = displayTitle;
            DisplayOrder = displayOrder;
        }
    }
}

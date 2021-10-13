using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aehyok.Core.Data.Model
{
    public class MD_InputModel_Child
    {
        /// <summary>
        /// 录入模型子模型Id
        /// </summary>
        
        public string Id { get; set; }

        /// <summary>
        /// 录入模型名称
        /// </summary>
        
        public string InputModelName { get; set; }

        /// <summary>
        /// 录入模型子模型名称
        /// </summary>
        
        public string ChildModelName { get; set; }

        /// <summary>
        /// 子模型定义
        /// </summary>
        
        public MD_InputModel ChildModel { get; set; }

        /// <summary>
        /// 子模型展示顺序
        /// </summary>
        
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        
        public List<MD_InputModel_ChildParam> Parameters { get; set; }

        /// <summary>
        /// 显示条件
        /// </summary>
        
        public string ShowCondition { get; set; }

        /// <summary>
        /// 选择模式（单选框，还是多选框）
        /// </summary>
        
        public int SelectMode { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        
        public string DisplayName { get; set; }

        /// <summary>
        /// 子模型名称
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ChildModelName;
        }

        /// <summary>
        /// 集成应用
        /// </summary>
        
        public string IntegratedApp { get; set; }

        /// <summary>
        /// 资源类型
        /// </summary>
        
        public string ResType { get; set; }

        public MD_InputModel_Child() { }
        public MD_InputModel_Child(string id, string inputModelName, string childModelName, int displayOrder)
        {
            Id = id;
            InputModelName = inputModelName;
            ChildModelName = childModelName;
            DisplayOrder = displayOrder;
        }
    }
}

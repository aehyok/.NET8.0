using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Core.Dtos
{
    public class WorkFlowDefineDto
    {
        /// <summary>
        /// 流程名称
        /// </summary>
        public string FlowName { get; set; }

        /// <summary>
        /// 流程Code唯一编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 流程分类
        /// </summary>
        public string FlowType { get; set; }

        /// <summary>
        /// 流程描述
        /// </summary>
        public string Descriptionn { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsEnable { get; set; }

        /// <summary>
        /// JSON定义包
        /// </summary>
        public string JsonDefine { get; set; }
    }

    public class CreateWorkFlowDefineDto
    {
        /// <summary>
        /// 流程名称
        /// </summary>
        public string FlowName { get; set; }

        /// <summary>
        /// 流程Code唯一编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 流程分类
        /// </summary>
        public string FlowType { get; set; }

        /// <summary>
        /// 流程描述
        /// </summary>
        public string Descriptionn { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsEnable { get; set; }

        /// <summary>
        /// JSON定义包
        /// </summary>
        public string JsonDefine { get; set; }
    }
}

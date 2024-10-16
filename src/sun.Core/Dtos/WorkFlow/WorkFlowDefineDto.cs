using sun.Core.Domains.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Core.Dtos.WorkFlow
{
    public class WorkFlowDefineDto
    {
        /// <summary>
        /// 流程定义Id
        /// </summary>
        public long Id { get; set; }
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
        public string Description { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }

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
        public bool IsEnable { get; set; }

        /// <summary>
        /// JSON定义包
        /// </summary>
        public string JsonDefine { get; set; }
    }

    public class WorkFlowStateDto
    {
        /// <summary>
        /// 流程状态Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 所在JSON元数据中的唯一ID（GUID）
        /// </summary>
        public string JsonDefineId { get; set; }
        /// <summary>
        /// 工作流Id
        /// </summary>
        public long WorkFlowId { get; set; }

        /// <summary>
        /// 状态名称
        /// </summary>
        public string StateName { get; set; }

        /// <summary>
        /// 状态Code唯一编码
        /// </summary>
        public string StateCode { get; set; }

        /// <summary>
        /// 状态描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 状态类型
        /// </summary>
        public StateType StateType { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }
    }

    public class CreateWorkFlowStateDto
    {
        /// <summary>
        /// 所在JSON元数据中的唯一ID（GUID）
        /// </summary>
        public string JsonDefineId { get; set; }
        /// <summary>
        /// 工作流Id
        /// </summary>
        public long WorkFlowId { get; set; }

        /// <summary>
        /// 状态名称
        /// </summary>
        public string StateName { get; set; }

        /// <summary>
        /// 状态Code唯一编码
        /// </summary>
        public string StateCode { get; set; }

        /// <summary>
        /// 状态描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 状态类型
        /// </summary>
        public StateType StateType { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }
    }

    public class WorkFlowActionDto
    {
        /// <summary>
        /// 流程动作Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 所在JSON元数据中的唯一ID（GUID）
        /// </summary>
        public string JsonDefineId { get; set; }

        /// <summary>
        /// 流程状态Id
        /// </summary>
        public long WorkFlowStateId { get; set; }

        /// <summary>
        /// 动作名称
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 动作使用者类型
        /// </summary>
        public ActionForUserType ActionForUserType { get; set; }

        /// <summary>
        /// 动作Code唯一编码
        /// </summary>
        public string ActionCode { get; set; }

        /// <summary>
        /// 目标状态Id(当前动作执行完后的状态)
        /// </summary>
        public long WorkFlowTargetStateId { get; set; }


        /// <summary>
        /// 动作类型
        /// </summary>
        public ActionType ActionType { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }
    }

    public class CreateWorkFlowActionDto
    {
        /// <summary>
        /// 所在JSON元数据中的唯一ID（GUID）
        /// </summary>
        public string JsonDefineId { get; set; }

        /// <summary>
        /// 流程状态Id
        /// </summary>
        public long WorkFlowStateId { get; set; }
        /// <summary>
        /// 动作名称
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 动作使用者类型
        /// </summary>
        public ActionForUserType ActionForUserType { get; set; }

        /// <summary>
        /// 动作Code唯一编码
        /// </summary>
        public string ActionCode { get; set; }

        /// <summary>
        /// 目标状态Id(当前动作执行完后的状态)
        /// </summary>
        public long WorkFlowTargetStateId { get; set; }

        /// <summary>
        /// 动作类型
        /// </summary>
        public ActionType ActionType { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }
    }
}

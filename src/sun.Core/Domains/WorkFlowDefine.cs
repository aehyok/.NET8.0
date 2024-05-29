using sun.EntityFrameworkCore.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Core.Domains
{
    /// <summary>
    /// 工作流定义
    /// </summary>
    public  class WorkFlowDefine: AuditedEntity
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

    /// <summary>
    /// 工作流下的状态表
    /// </summary>
    public class WorkFlowState : AuditedEntity
    {
        /// <summary>
        /// 所在JSON元数据中的唯一ID（GUID）
        /// </summary>
        public string JsonDefineId { get; set; }
        /// <summary>
        /// 工作流Id
        /// </summary>
        public long WorkFlowId { get; set; }

        public WorkFlowDefine WorkFlowDefine { get; set; }

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
        public string Description { get;set; }

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
        public int IsEnable { get; set; }
    }


    /// <summary>
    /// 工作流状态下的动作表
    /// </summary>
    public class WorkFlowAction: AuditedEntity
    {
        /// <summary>
        /// 所在JSON元数据中的唯一ID（GUID）
        /// </summary>
        public string JsonDefineId { get; set; }

        /// <summary>
        /// 流程状态Id
        /// </summary>
        public long WorkFlowStateId { get; set; }

        public WorkFlowState WorkFlowState { get; set; }
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
        /// 目标状态
        /// </summary>
        public WorkFlowState WorkFlowTargetState { get; set; }

        /// <summary>
        /// 动作类型
        /// </summary>
        public ActionType ActionType { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsEnable { get; set; }
    }

    /// <summary>
    /// 工作流中配置的生成文档（docx excel pdf等）
    /// </summary>
    public class WorkFlowDocument: AuditedEntity
    {
        /// <summary>
        /// 工作流定义Id
        /// </summary>
        public long WorkFlowDefineId { get; set; }

        /// <summary>
        /// 文书名称
        /// </summary>
        public string Name { get; set; }
    }
    
    public enum StateType
    {
        /// <summary>
        /// 开始状态
        /// </summary>
        Start = 1,

        /// <summary>
        /// 结束
        /// </summary>
        End = 2,

        /// <summary>
        /// 正常状态、普通状态
        /// </summary>
        Normal = 3,

        /// <summary>
        /// 审批类型
        /// </summary>
        Approval = 4,

        /// <summary>
        /// 审批文档类型加数据详情
        /// </summary>
        ApprovalDoc = 5,
    }

    public enum ActionForUserType
    {
        /// <summary>
        /// 所有人
        /// </summary>
        All=1,

        /// <summary>
        /// 有查看权限的人
        /// </summary>
        Detail = 2,

        /// <summary>
        /// 历史处理人
        /// </summary>
        History = 3,

        /// <summary>
        /// 当前处理人
        /// </summary>
         Current = 4
    }

    public enum ActionType
    {
        /// <summary>
        /// 正常的
        /// </summary>
        Normal = 1,

        /// <summary>
        /// 指派
        /// </summary>
        Assign = 2,
    }
}

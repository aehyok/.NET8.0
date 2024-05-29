using sun.EntityFrameworkCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Core.Domains
{
    /// <summary>
    /// 工作流动作日志表
    /// </summary>
    public class WorkFlowActionLog : AuditedEntity
    {
        /// <summary>
        /// 当前动作的Id
        /// </summary>
        public string WorkFlowActionId { get; set; }

        public WorkFlowAction WorkFlowAction { get; set; }

        /// <summary>
        /// 业务实体Id
        /// </summary>
        public long BusinessId { get; set; }

        /// <summary>
        /// 执行动作的用户Id
        /// </summary>
        public long UserId { get; set; }

        public User User { get; set; }

        /// <summary>
        /// 执行动作的区域Id
        /// </summary>
        public long RegionId { get; set; }

        public Region Region { get; set; }

        /// <summary>
        /// 执行动作时的原状态Id
        /// </summary>
        public WorkFlowState WorkFlowSourceStateId { get; set; }

        public WorkFlowState WorkFlowSourceState { get; set; }
        /// <summary>
        /// 执行动作时的目标状态Id
        /// </summary>
        public WorkFlowState WorkFlowTargetStateId { get; set; }

        public WorkFlowState WorkFlowTargetState { get; set; }
    }

    /// <summary>
    /// 工作流状态日志表
    /// </summary>
    public class WorkFlowStateLog: AuditedEntity
    {
        /// <summary>
        /// 执行具体动作的日志Id
        /// </summary>
        public WorkFlowActionLog WorkFlowActionLog { get; set; }

        /// <summary>
        /// 执行具体动作的日志Id
        /// </summary>
        public long WorkFlowActionLogId { get; set; }

        public WorkFlowAction WorkFlowAction { get; set; }

        /// <summary>
        /// 状态Id
        /// </summary>
        public long WorkFlowStateId { get; set; }

        public WorkFlowState WorkFlowState { get; set; }

        /// <summary>
        /// 当前操作用户Id
        /// </summary>
        public long UserId { get; set; }

        public User User { get; set; }

        /// <summary>
        /// 当前状态操作区域Id
        /// </summary>
        public long RegionId { get; set; }

        public Region Region { get; set; }

        /// <summary>
        /// 业务实体Id
        /// </summary>
        public long BusinessId { get; set; }

        /// <summary>
        /// 是否历史状态（0为当前状态 1为历史状态）
        /// </summary>
        public bool IsHistory { get; set; }

        /// <summary>
        /// 状态开始执行时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 状态结束执行时间
        /// </summary>
        public DateTime EndTime { get; set; }

        ///// <summary>
        ///// 状态要求的预警时间（比TimeLimited结束时间要小）
        ///// </summary>
        //public DateTime TimeWarning { get; set; }

        /// <summary>
        /// 状态要求的结束时间
        /// </summary>
        //public DateTime TimeLimited { get; set; }
    }

    //FLOW_CASETOASSIGNLOG

    /// <summary>
    /// 工作流 指派日志
    /// </summary>
    public class WorkFlowAssignLog: AuditedEntity
    {
        /// <summary>
        /// 所属业务Id
        /// </summary>
        public long BusinessId { get; set; }

        /// <summary>
        /// 工作流状态Id
        /// </summary>
        public long WorkFlowStateId { get; set; }

        /// <summary>
        /// 指派开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 指派结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 指派到的用户Id
        /// </summary>
        public long AssignUserId { get; set; }

        /// <summary>
        ///  指派到的区域Id 
        /// </summary>
        public long AssignRegionId { get; set; }

        /// <summary>
        /// 操作用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 操作区域Id
        /// </summary>
        public long CreateRegionId { get; set; }
    }
}

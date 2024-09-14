using sun.EntityFrameworkCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Core.Domains.WorkFlow
{
    /// <summary>
    /// Form表单默认值配置
    /// </summary>
    public class WorkFlowFormDefaultConfig : AuditedEntity
    {

    }

    /// <summary>
    /// 工作流状态配置
    /// </summary>
    public class WorkFlowStateConfig : AuditedEntity
    {
        /// <summary>
        /// 工作流状态Id
        /// </summary>
        public long WorkFlowStateId { get; set; }

        /// <summary>
        /// 工作流状态
        /// </summary>
        public WorkFlowState WorkFlowState { get; set; }

        /// <summary>
        /// 工作流操作区域Id
        /// </summary>
        public long RegionId { get; set; }

        /// <summary>
        /// 操作区域
        /// </summary>
        public Region Region { get; set; }

        ///// <summary>
        ///// 类型
        ///// </summary>
        //public StateConfigType Type { get; set; }

        /// <summary>
        /// 区域层级
        /// </summary>
        public int RegionLevel { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// 绑定角色Id
        /// </summary>
        public Role Role { get; set; }
    }

    /// <summary>
    /// 工作流动作配置
    /// </summary>
    public class WorkFlowActionConfig : AuditedEntity
    {
        /// <summary>
        /// 工作流程动作定义Id
        /// </summary>
        public long WorkFlowActionId { get; set; }

        public WorkFlowAction WorkFlowAction { get; set; }
        /// <summary>
        /// 区域层级
        /// </summary>
        public int RegionLevel { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// 绑定角色Id
        /// </summary>
        public Role Role { get; set; }
    }

    /// <summary>sub
    /// 工作流动作的流转配置（不配置默认就是当前继续流转）
    /// </summary>
    public class WorkFlowActionCirculateConfig : AuditedEntity
    {
        public long WorkFlowActionConfigId { get; set; }

        public WorkFlowActionConfig WorkFlowActionConfig { get; set; }

        public int RegionLevel { get; set; }

        public long RoleId { get; set; }

        /// <summary>
        /// 所在流程的动作Id
        /// </summary>
        //public long WorkFlowActionId { get; set; }

        //public WorkFlowAction WorkFlowAction { get; set; }

        /// <summary>
        /// 目标所属区域Id
        /// </summary>
        //public long TargetRegionId { get; set; }

        //public Region Region { get; set; }

        /// <summary>
        /// 工作流 状态配置Id
        /// </summary>
        //public long WorkFlowStateConfigId { get; set; }

        /// <summary>
        /// 包含状态Id和区域Id
        /// </summary>
        //public WorkFlowStateConfig WorkFlowStateConfig { get; set; }
    }

    /// <summary>
    /// 单位状态的类型
    /// </summary>
    public enum StateConfigType
    {
        /// <summary>
        /// 操作
        /// </summary>
        Operation = 1,

        /// <summary>
        /// 显示
        /// </summary>
        Display = 2
    }
}

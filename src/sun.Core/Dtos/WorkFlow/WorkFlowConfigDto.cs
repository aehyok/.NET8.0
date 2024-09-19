using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Core.Dtos.WorkFlow
{
    public class WorkFlowConfigDto
    {
        public long WorkFlowStateId { get; set; }

        public string WorkFlowStateName { get; set; }

        public long WorkFlowActionId { get; set; }

        public string WorkFlowActionName { get; set; }

        public bool IsSelectedState { get; set; }

        public bool IsSelectedAction { get; set; }



    }
    public class CreateWorkFlowStateConfigDto
    {
        /// <summary>
        /// 工作流程状态Id
        /// </summary>
        public long WorkFlowStateId { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// 区域Id
        /// </summary>
        public long RegionId { get; set; }
        /// <summary>
        /// 区域级别
        /// </summary>
        public int RegionLevel { get; set; }

        /// <summary>
        /// 是否被选中
        /// </summary>
        //public bool IsSelected { get; set; }
    }

    public class CreateWorkFlowActionConfigDto
    {
        /// <summary>
        /// 工作流程状态Id
        /// </summary>
        public long WorkFlowActionId { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// 区域级别
        /// </summary>
        public int RegionLevel { get; set; }

        public long RegionId { get; set; }

        /// <summary>
        /// 是否被选中
        /// </summary>
        //public bool IsSelected { get; set; }
    }

    public class CreateWorkFlowActionCirculateConfigDto
    {
        /// <summary>
        /// 工作流程状态Id
        /// </summary>
        public long WorkFlowActionId { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// 区域级别
        /// </summary>
        public int RegionLevel { get; set; }

        /// <summary>
        /// 区域Id
        /// </summary>
        public long RegionId { get; set; }

        /// <summary>
        /// 是否被选中
        /// </summary>
        //public bool IsSelected { get; set; }
    }
}

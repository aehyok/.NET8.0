using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sun.Core.Domains;
using sun.Core.Services.WorkFlow;

namespace sun.NCDP.Api.Controllers
{
    /// <summary>
    /// 工作流程状态配置
    /// </summary>
    public class WorkFlowStateConfigController(
        IWorkFlowStateConfigService stateConfigService,
        IWorkFlowActionConfigService actionConfigService,
        IWorkFlowStateService stateService,
        IWorkFlowActionService actionService
        ) : NCDPControllerBase
    {
        /// <summary>
        /// 测试接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task Test()
        {
            return Task.CompletedTask;
        }
        /// <summary>
        /// 根据工作流程定义Id获取当前角色下的可配置状态、动作数据
        /// </summary>
        /// <param name="workFlowDefineId"></param>
        /// <param name="roleId"></param>
        /// <param name="regionLevel"></param>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<List<WorkFlowStateConfig>> GetStateConfigListAsync(long workFlowDefineId, long roleId, int regionLevel)
        {
            var query = await (from state in stateService.GetQueryable()
                        join action in actionService.GetQueryable() on state.Id equals action.WorkFlowStateId
                        join stateConfig in stateConfigService.GetQueryable() on state.Id equals stateConfig.WorkFlowStateId into stateConfigList
                        from ss in stateConfigList.DefaultIfEmpty()
                        join actionConfig in actionConfigService.GetQueryable() on action.Id equals actionConfig.WorkFlowActionId into actionConfigList
                        from aa in actionConfigList.DefaultIfEmpty()
                        where state.WorkFlowDefineId == workFlowDefineId && ss.RoleId == roleId && ss.RegionLevel == regionLevel && aa.RoleId == roleId && aa.RegionLevel == regionLevel
                        select new
                        {
                            // 创建一个实体来返回数据
                        }).ToListAsync();

            return new List<WorkFlowStateConfig>();
        }
    }
}

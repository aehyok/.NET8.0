﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sun.Core.Domains.WorkFlow;
using sun.Core.Dtos.WorkFlow;
using sun.Core.Services.WorkFlow;
using sun.Infrastructure.Exceptions;

namespace sun.NCDP.Api.Controllers
{
    /// <summary>
    /// 工作流程状态配置
    /// </summary>
    public class WorkFlowConfigController(
        IWorkFlowStateConfigService stateConfigService,
        IWorkFlowActionConfigService actionConfigService,
        IWorkFlowActionCirculateConfigService circulateConfigService,
        IWorkFlowStateService stateService,
        IWorkFlowActionService actionService
        ) : NCDPControllerBase
    {
        /// <summary>
        /// 根据工作流程定义Id获取当前角色下的可配置状态、动作数据
        /// </summary>
        /// <param name="workFlowDefineId">工作流流程定义Id</param>
        /// <param name="RegionId">区域</param>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<List<WorkFlowConfigDto>> GetStateConfigListAsync(long workFlowDefineId, long RegionId)
        {
            var regionId = base.CurrentUser.RegionId;
            // 此处应该移除RoleId，或者变更为RegionId
            var query = (from state in stateService.GetQueryable()
                         join action in actionService.GetQueryable() on state.Id equals action.WorkFlowStateId
                         join stateConfig in stateConfigService.GetQueryable() on state.Id equals stateConfig.WorkFlowStateId into stateConfigList
                         from ss in stateConfigList.DefaultIfEmpty()
                         join actionConfig in actionConfigService.GetQueryable() on action.Id equals actionConfig.WorkFlowActionId into actionConfigList
                         from aa in actionConfigList.DefaultIfEmpty()
                         where
                             state.WorkFlowDefineId == workFlowDefineId &&
                             ss.RegionId == regionId  &&
                             aa.RegionId == regionId
                         select new WorkFlowConfigDto
                         {
                             // 创建一个实体来返回数据
                             // 查询该流程下的状态
                             // 通过状态查询出相应状态下的动作列表
                             // 通过状态与状态配置表查出状态配置关联
                             // 通过动作与动作配置表查出动作配置关联
                             // 查询条件为流程定义Id、角色Id、区域等级（状态配置表过滤和动作配置表过滤）
                             WorkFlowStateId = state.Id,
                             WorkFlowStateName = state.StateName,
                             IsSelectedState = ss == null ? false : true,
                             WorkFlowActionId = action.Id,
                             WorkFlowActionName = action.ActionName,
                             IsSelectedAction = aa == null ? false : true,
                         });

            return await query.ToListAsync();
        }

        /// <summary>
        /// 新增工作流程的状态配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("state")]
        public async Task<long> PostStateConfigAsync(CreateWorkFlowStateConfigDto model)
        {
            //&& a.RegionLevel == model.RegionLevel
            var entity = await stateConfigService.GetAsync(a => a.WorkFlowStateId == model.WorkFlowStateId && a.RegionId == model.RegionId);

            if (entity is null)
            {
                // 直接新增
                entity = this.Mapper.Map<WorkFlowStateConfig>(model);
                await stateConfigService.InsertAsync(entity);
                return entity.Id;

            }
            else
            {
                throw new ErrorCodeException(-1, "当前要修改的配置与数据库不一致，请刷新页面");
            }
        }

        /// <summary>
        /// 修改工作流程的状态配置
        /// </summary>
        /// <param name="workFlowStateConfigId">工作流状态配置Id</param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ErrorCodeException"></exception>
        [HttpPut("state/{workFlowStateConfigId}")]
        public async Task<StatusCodeResult> PutStateConfigAsync(long workFlowStateConfigId, CreateWorkFlowStateConfigDto model)
        {
            //&& a.RegionLevel == model.RegionLevel
            var entity = await stateConfigService.GetAsync(a => a.Id == workFlowStateConfigId);

            if (entity is not null)
            {
                entity.IsDeleted = true;
                await stateConfigService.UpdateAsync(entity);
                return Ok();
            }
            else
            {
                throw new ErrorCodeException(-1, "当前要修改的配置与数据库不一致，请刷新页面");
            }
        }

        /// <summary>
        /// 新增工作流程的动作配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("action")]
        public async Task<long> PostActionConfigAsync(CreateWorkFlowActionConfigDto model)
        {
            //&& a.RegionLevel == model.RegionLevel
            var entity = await actionConfigService.GetAsync(a => a.WorkFlowActionId == model.WorkFlowActionId && a.RegionId == model.RegionId );
            if (entity is null)
            {
                // 直接新增
                entity = this.Mapper.Map<WorkFlowActionConfig>(model);
                await actionConfigService.InsertAsync(entity);
                return entity.Id;
            }
            else
            {
                throw new ErrorCodeException(-1, "当前要修改的配置与数据库不一致，请刷新页面");
            }
        }

        /// <summary>
        /// 修改工作流程的动作配置
        /// </summary>
        /// <param name="workFlowActionConfigId">工作流动作配置Id</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("action/{workFlowActionConfigId}")]
        public async Task<StatusCodeResult> PutActionConfigAsync(long workFlowActionConfigId, CreateWorkFlowActionConfigDto model)
        {
            // && a.RegionLevel == model.RegionLevel
            var entity = await actionConfigService.GetAsync(a => a.Id == workFlowActionConfigId);
            if (model is not null)
            {
                entity.IsDeleted = true;
                await actionConfigService.UpdateAsync(entity);
                return Ok();
            }
            else
            {
                throw new ErrorCodeException(-1, "当前要修改的配置与数据库不一致，请刷新页面");
            }
        }

        /// <summary>
        /// 新增工作流程的动作流转配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("actioncirculate")]
        public async Task<long> PostActionCirculateConfigAsync(CreateWorkFlowActionCirculateConfigDto model)
        {
            //&& a.RegionLevel == model.RegionLevel
            var entity = await circulateConfigService.GetAsync(a => a.WorkFlowActionConfigId == model.WorkFlowActionId && a.RegionId == model.RegionId);
            if (entity is null)
            {
                // 直接新增
                entity = this.Mapper.Map<WorkFlowActionCirculateConfig>(model);
                await circulateConfigService.InsertAsync(entity);
                return entity.Id;
            }
            else
            {
                throw new ErrorCodeException(-1, "当前要修改的配置与数据库不一致，请刷新页面");
            }
        }

        /// <summary>
        /// 修改工作流程的动作配置
        /// </summary>
        /// <param name="workFlowActionCirculateConfigId">工作流动作流转配置Id</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("actioncirculate/{workFlowActionCirculateConfigId}")]
        public async Task<StatusCodeResult> PutActionCirculateConfigAsync(long workFlowActionCirculateConfigId, CreateWorkFlowActionCirculateConfigDto model)
        {
            // && a.RegionLevel == model.RegionLevel
            var entity = await circulateConfigService.GetAsync(a => a.Id == workFlowActionCirculateConfigId);
            if (model is not null)
            {
                entity.IsDeleted = true;
                await circulateConfigService.UpdateAsync(entity);
                return Ok();
            }
            else
            {
                throw new ErrorCodeException(-1, "当前要修改的配置与数据库不一致，请刷新页面");
            }
        }
    }
}

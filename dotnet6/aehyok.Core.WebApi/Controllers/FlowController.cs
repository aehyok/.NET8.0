using aehyok.Core.EntityFramework.MySql;
using aehyok.Core.EntityFramework.MySql.Models;
using aehyok.Core.IRepository;
using aehyok.Core.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aehyok.Core.WebApi.Controllers
{
    /// <summary>
    /// 流程引擎定义表
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FlowController : BaseApiController
    {
        //private readonly IFlowRepository _flowRepository;
        private readonly IRepository<FlowEntityType> _flowEntityTypeRepository;
        private readonly IRepository<FlowEntityState> _flowEntityStateRepository;
        private readonly IRepository<FlowStateTransition> _flowEntityActionRepository;

        public FlowController(
            IRepository<FlowEntityType> flowEntityTypeRepository,
            IRepository<FlowEntityState> flowEntityStateRepository,
            IRepository<FlowStateTransition> flowEntityActionRepository
            )
        {
            //this._flowRepository = flowRepository;
            this._flowEntityTypeRepository = flowEntityTypeRepository;
            this._flowEntityStateRepository = flowEntityStateRepository;
            this._flowEntityActionRepository = flowEntityActionRepository;
        }

        #region 流程管理

        /// <summary>
        /// 获取流程列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public List<FlowEntityType> GetFlowEntityTypeList()
        {
            this._logger.Info("aehyok"+ DateTime.Now.ToString());
            return this._flowEntityTypeRepository.GetList();
        }

        /// <summary>
        /// 保存流程
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<int> SaveFlowEntityType(FlowEntityType flowEntityType)
        {
            this._logger.Info(flowEntityType.FlowName);
            try
            {
                var item = await this._flowEntityTypeRepository.GetAsync(flowEntityType.Id);
                _flowEntityTypeRepository.EF.Entry(item).State = EntityState.Detached;

                if (item != null && item.Id != null)
                {
                    return await this._flowEntityTypeRepository.UpdateAsync(flowEntityType);
                }
                else
                {
                    return await this._flowEntityTypeRepository.InsertAsync(flowEntityType);
                    
                }
            }
            catch(Exception error)
            {
                this._logger.Error(error, error.Message);
                this._logger.Error(error, error.Message);
                return -1;
            }
        }

        /// <summary>
        /// 添加流程
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<int> AddFlowEntityType(FlowEntityType flowEntityType)
        {
            this._logger.Info(flowEntityType.FlowName);
            try
            {
                return await this._flowEntityTypeRepository.InsertAsync(flowEntityType);
            }
            catch (Exception error)
            {
                this._logger.Error(error, error.Message);
                this._logger.Error(error, error.Message);
                return -1;
            }
        }

        /// <summary>
        /// 修改流程
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<int> UpdateFlowEntityType(FlowEntityType flowEntityType)
        {
            var item = await this._flowEntityTypeRepository.GetAsync(flowEntityType.Id);
            _flowEntityTypeRepository.EF.Entry(item).State = EntityState.Detached;

            if (item != null && item.Id != null)
            {
                return await this._flowEntityTypeRepository.UpdateAsync(flowEntityType);
            }
            return -1;
        }

        /// <summary>
        /// 获取流程详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<FlowEntityType> GetFlowEntityType(string flowId)
        {
            return await this._flowEntityTypeRepository.GetAsync(flowId);
        }

        /// <summary>
        /// 通过流程id删除流程
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<int> DeleteFlowEntityType(string flowId)
        {
            
            return await this._flowEntityTypeRepository.DeleteAsync(flowId);
        }
        #endregion

        #region 流程状态管理

        /// <summary>
        /// 通过状态Id主键获取状态详情
        /// </summary>
        /// <param name="stateId"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<FlowEntityState> GetFlowEntityState(string stateId)
        {
            return await this._flowEntityStateRepository.GetAsync(stateId);
        }

        /// <summary>
        /// 通过状态id删除状态
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<int> DeleteFlowEntityState(string stateId)
        {
            return await this._flowEntityStateRepository.DeleteAsync(stateId);
        }

        /// <summary>
        /// 通过流程ID获取流程状态列表
        /// </summary>
        /// <param name="flowId"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<List<FlowEntityState>> GetFlowEntityStateList(string flowId)
        {
            return await this._flowEntityStateRepository.GetListAsync(item => item.FlowId == flowId);
        }

        /// <summary>
        /// 保存流程状态
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<int> SaveFlowEntityState(FlowEntityState flowEntityState)
        {
            var item = await this._flowEntityStateRepository.GetAsync(flowEntityState.Id);
            _flowEntityStateRepository.EF.Entry(item).State = EntityState.Detached;
            if (flowEntityState != null && flowEntityState.Id != null)
            {
                return await this._flowEntityStateRepository.UpdateAsync(flowEntityState);
            }
            else
            {
                var result = await this._flowEntityStateRepository.InsertAsync(flowEntityState);
                return result;
            }
        }
        #endregion

        #region 流程动作管理

        /// <summary>
        /// 通过动作Id主键获取动作详情
        /// </summary>
        /// <param name="actionId"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<FlowStateTransition> GetFlowEntityAction(string actionId)
        {
            return await this._flowEntityActionRepository.GetAsync(actionId);
        }

        /// <summary>
        /// 通过动作id删除动作
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<int> DeleteFlowEntityAction(string actionId)
        {
            return await this._flowEntityActionRepository.DeleteAsync(actionId);
        }

        /// <summary>
        /// 通过状态Id获取流程动作列表
        /// </summary>
        /// <param name="stateId"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<List<FlowStateTransition>> GetFlowEntityActionList(string stateId)
        {
            return await this._flowEntityActionRepository.GetListAsync(item => item.StateId == stateId);
        }

        /// <summary>
        /// 保存流程动作
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<int> SaveFlowEntityAction(FlowStateTransition flowStateTransition)
        {
            var item = await this._flowEntityActionRepository.GetAsync(flowStateTransition.Id);
            _flowEntityActionRepository.EF.Entry(item).State = EntityState.Detached;
            if (flowStateTransition != null && flowStateTransition.Id != null)
            {
                return await this._flowEntityActionRepository.UpdateAsync(flowStateTransition);
            }
            else
            {
                var result = await this._flowEntityActionRepository.InsertAsync(flowStateTransition);
                return result;
            }
        }
        #endregion

    }
}

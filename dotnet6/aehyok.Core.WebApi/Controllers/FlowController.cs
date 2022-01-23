using aehyok.Core.EntityFrameCore.MySql;
using aehyok.Core.EntityFrameCore.MySql.Models;
using aehyok.Core.IRepository;
using aehyok.Core.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IFlowRepository _flowRepository;
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
            return await this._flowRepository.SaveFlowEntityType(flowEntityType);
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
        #endregion

    }
}

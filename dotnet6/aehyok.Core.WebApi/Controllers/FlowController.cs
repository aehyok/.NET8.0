using aehyok.Core.EntityFrameCore.MySql.Models;
using aehyok.Core.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aehyok.Core.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FlowController : BaseApiController
    {
        private readonly IFlowRepository _flowRepository;

        public FlowController(IFlowRepository flowRepository)
        {
            this._flowRepository = flowRepository;
        }

        /// <summary>
        /// 获取流程列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<List<FlowEntityType>> GetFlowEntityTypeList()
        {
            return await this._flowRepository.GetFlowEntityTypeList();
        }

        /// <summary>
        /// 保存流程
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<int> SaveFlowEntityType (FlowEntityType flowEntityType)
        {
            return await this._flowRepository.SaveFlowEntityType(flowEntityType);
        }

        /// <summary>
        /// 获取流程详情
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<FlowEntityType> GetFlowEntityType(string flowId)
        {
            return await this._flowRepository.GetFlowEntityTypeById(flowId);
        }

        /// <summary>
        /// 删除流程
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<int> DeleteFlowEntityType(string flowId)
        {
            return await this._flowRepository.DeleteFlowEntityType(flowId);
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
            return await this._flowRepository.GetFlowEntityStateList(flowId);
        }

        /// <summary>
        /// 通过状态Id获取流程动作列表
        /// </summary>
        /// <param name="stateId"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<List<FlowStateTransition >> GetFlowEntityActionList(string stateId)
        {
            return await this._flowRepository.GetFlowEntityTransitions(stateId);
        }
    }
}

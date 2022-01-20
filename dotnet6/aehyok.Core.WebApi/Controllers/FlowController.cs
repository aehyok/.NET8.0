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
    }
}

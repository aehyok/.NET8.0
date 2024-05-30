using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sun.Core.Services.WorkFlow;

namespace sun.NCDP.Api.Controllers
{
    /// <summary>
    /// 工作流程状态配置
    /// </summary>
    public class WorkFlowStateConfigController(IWorkFlowStateConfigService stateConfigService) : NCDPControllerBase
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
    }
}

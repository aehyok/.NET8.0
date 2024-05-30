using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace sun.NCDP.Api.Controllers
{
    /// <summary>
    /// 工作流程动作配置
    /// </summary>
    public class WorkFlowActionConfigController : NCDPControllerBase
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

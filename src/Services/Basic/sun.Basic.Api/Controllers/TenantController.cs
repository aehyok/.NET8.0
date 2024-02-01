using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace sun.Basic.Api.Controllers
{
    /// <summary>
    /// 租户管理
    /// </summary>
    public class TenantController : BasicControllerBase
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

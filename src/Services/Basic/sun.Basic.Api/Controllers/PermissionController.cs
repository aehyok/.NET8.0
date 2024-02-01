using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace sun.Basic.Api.Controllers
{

    /// <summary>
    /// 授权管理
    /// </summary>
    public class PermissionController : BasicControllerBase
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

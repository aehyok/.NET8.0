using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aehyok.NCDP.Api.Controllers
{

    /// <summary>
    /// 授权管理
    /// </summary>
    public class PermissionController : NCDPControllerBase
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

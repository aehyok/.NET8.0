using aehyok.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aehyok.NCDP.Api.Controllers
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public class RoleController : NCDPControllerBase
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

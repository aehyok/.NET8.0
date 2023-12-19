using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aehyok.Basic.Api.Controllers
{

    /// <summary>
    /// Token 管理
    /// </summary>
    public class TokenController : BasicControllerBase
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

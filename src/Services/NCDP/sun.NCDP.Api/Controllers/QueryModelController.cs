using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace sun.NCDP.Api.Controllers
{

    /// <summary>
    /// 查询模型
    /// </summary>
    public class QueryModelController : NCDPControllerBase
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

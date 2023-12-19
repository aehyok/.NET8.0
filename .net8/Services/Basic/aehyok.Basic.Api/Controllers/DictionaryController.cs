using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aehyok.Basic.Api.Controllers
{

    /// <summary>
    /// 字典管理
    /// </summary>
    public class DictionaryController : BasicControllerBase
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

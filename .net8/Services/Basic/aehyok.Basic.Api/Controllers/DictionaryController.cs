using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aehyok.Basic.Api.Controllers
{

    /// <summary>
    /// 字典管理
    /// </summary>
    public class DictionaryController : BasicControllerBase
    {
        private readonly  ILogger<DictionaryController> logger;

        public DictionaryController(ILogger<DictionaryController> logger)
        {
            this.logger = logger;
        }
        /// <summary>
        /// 测试接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task Test()
        {
            this.logger.LogError("测试方法报错");
            return Task.CompletedTask;
        }
    }
}

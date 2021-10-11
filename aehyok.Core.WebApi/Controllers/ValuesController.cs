using aehyok.Core.Data;
using aehyok.Core.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aehyok.Core.WebApi.Controllers
{
    /// <summary>
    /// 测试控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ITestRepository testRepository;
        private readonly ILogger<ValuesController> logger;
        public ValuesController(ITestRepository _testRepository, ILogger<ValuesController> _logger)
        {
            this.testRepository = _testRepository;
            this.logger = _logger;
        }
        /// <summary>
        /// 获取列表数据get
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public dynamic Get()
        {
            this.logger.LogDebug("sssssssssssssssss");
            JsonResultModel result = new JsonResultModel
            {
                Code = 200,
                Message = "OK",
                Data = new Dictionary<string, string>()
            };
            testRepository.GetTest();
            return result;
        }

        /// <summary>
        /// 获取列表数据GetTest
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetTest()
        {
            var result = new Dictionary<string,object>();
            result.Add("name", "aehyok");
            result.Add("age", 33);
            return result;
        }
    }
}

using aehyok.Core.Data;
using aehyok.Core.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public ValuesController(ITestRepository _testRepository)
        {
            this.testRepository = _testRepository;
        }
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public dynamic Get()
        {
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
        /// 获取列表数据
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

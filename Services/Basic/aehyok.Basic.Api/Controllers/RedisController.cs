using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aehyok.Basic.Api.Controllers
{
    public class RedisController : BasicControllerBase
    {
        /// <summary>
        /// 获取redis所有keys
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<dynamic> GetListAsync()
        {
            return null;
        }
    }
}

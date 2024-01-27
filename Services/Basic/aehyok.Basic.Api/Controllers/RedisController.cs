using aehyok.Redis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aehyok.Basic.Api.Controllers
{

    /// <summary>
    /// 缓存管理
    /// </summary>
    /// <param name="redisService"></param>
    public class RedisController(IRedisService redisService) : BasicControllerBase
    {
        /// <summary>
        /// 获取redis所有keys
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<dynamic> GetListAsync()
        {
            return await redisService.ScanAsync();




        }
    }
}

using sun.Redis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using sun.Infrastructure.Enums;
using sun.Infrastructure.Models;

namespace sun.Basic.Api.Controllers
{

    /// <summary>
    /// 缓存管理
    /// </summary>
    /// <param name="redisService"></param>
    //[Authorize(Roles = SystemRoles.ROOT)]
    public class RedisController(IRedisService redisService) : BasicControllerBase
    {
        /// <summary>
        /// 获取redis所有keys
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<dynamic> GetListAsync(PagedQueryModelBase model)
        {
            return await redisService.ScanAsync(model);




        }
    }
}

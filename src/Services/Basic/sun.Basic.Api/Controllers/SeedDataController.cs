using sun.Core.Domains;
using sun.Core.Dtos;
using sun.EntityFrameworkCore.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;
using sun.Infrastructure.Enums;

namespace sun.Basic.Api.Controllers
{
    /// <summary>
    /// 种子数据初始化
    /// </summary>
    //[Authorize(Roles = SystemRoles.ROOT)]
    public class SeedDataTaskController(IServiceBase<SeedDataTask> seedDataTaskService) : BasicControllerBase
    {
        /// <summary>
        /// 种子数据任务列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<SeedDataTaskDto>> GetListAsync()
        {
            return await seedDataTaskService.GetListAsync<SeedDataTaskDto>();
        }
    }
}

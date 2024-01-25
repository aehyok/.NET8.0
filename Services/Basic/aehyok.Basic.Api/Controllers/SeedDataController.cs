using aehyok.Core.Domains;
using aehyok.Core.Dtos;
using aehyok.EntityFrameworkCore.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace aehyok.Basic.Api.Controllers
{
    /// <summary>
    /// 种子数据初始化
    /// </summary>
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

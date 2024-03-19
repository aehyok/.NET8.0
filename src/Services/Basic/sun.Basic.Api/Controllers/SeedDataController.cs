using sun.Core.Domains;
using sun.Core.Dtos;
using sun.EntityFrameworkCore.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;
using sun.Infrastructure.Enums;
using sun.Core.Services;
using sun.Infrastructure.Exceptions;

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

        /// <summary>
        /// 启用种子数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("Enable/{id}")]
        public async Task<StatusCodeResult> ItemEnableAsync(long id)
        {
            var entity = await seedDataTaskService.GetByIdAsync(id);

            if (entity is null)
            {
                throw new ErrorCodeException(-1, "您要启用的数据不存在");
            }

            entity.IsEnable = true;

            await seedDataTaskService.UpdateAsync(entity);

            return Ok();
        }

        /// <summary>
        /// 禁用种子数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("Disable/{id}")]
        public async Task<StatusCodeResult> ItemDisableAsync(long id)
        {
            var entity = await seedDataTaskService.GetByIdAsync(id);

            if (entity is null)
            {
                throw new ErrorCodeException(-1, "您要禁用的数据不存在");
            }

            entity.IsEnable = false;

            await seedDataTaskService.UpdateAsync(entity);

            return Ok();
        }
    }
}

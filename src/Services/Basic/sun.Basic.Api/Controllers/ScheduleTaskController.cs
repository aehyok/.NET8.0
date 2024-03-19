using sun.Core.Dtos;
using sun.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using sun.Infrastructure.Enums;
using sun.Basic.Services;
using sun.Infrastructure.Exceptions;

namespace sun.Basic.Api.Controllers
{
    /// <summary>
    /// 定时任务
    /// </summary>
    /// <param name="scheduleTaskService"></param>
    //[Authorize(Roles = SystemRoles.ROOT)]
    public class ScheduleTaskController(IScheduleTaskService scheduleTaskService) : BasicControllerBase
    {
        /// <summary>
        /// 定时任务列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<ScheduleTaskDto>> GetListAsync()
        {
            return await scheduleTaskService.GetListAsync<ScheduleTaskDto>();
        }

        /// <summary>
        /// 启用定时任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("Enable/{id}")]
        public async Task<StatusCodeResult> ItemEnableAsync(long id)
        {
            var entity = await scheduleTaskService.GetByIdAsync(id);

            if (entity is null)
            {
                throw new ErrorCodeException(-1, "您要启用的数据不存在");
            }

            entity.IsEnable = true;

            await scheduleTaskService.UpdateAsync(entity);

            return Ok();
        }

        /// <summary>
        /// 禁用定时任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("Disable/{id}")]
        public async Task<StatusCodeResult> ItemDisableAsync(long id)
        {
            var entity = await scheduleTaskService.GetByIdAsync(id);

            if (entity is null)
            {
                throw new ErrorCodeException(-1, "您要禁用的数据不存在");
            }

            entity.IsEnable = false;

            await scheduleTaskService.UpdateAsync(entity);

            return Ok();
        }
    }
}

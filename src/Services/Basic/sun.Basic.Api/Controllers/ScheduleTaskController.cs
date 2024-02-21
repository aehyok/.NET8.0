using sun.Core.Dtos;
using sun.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using sun.Infrastructure.Enums;

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
    }
}

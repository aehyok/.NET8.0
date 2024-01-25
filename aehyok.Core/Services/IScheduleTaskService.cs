using aehyok.Core.Domains;
using aehyok.EntityFrameworkCore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Services
{
    public interface IScheduleTaskService: IServiceBase<ScheduleTask>
    {
        /// <summary>
        /// 初始化定时任务
        /// </summary>
        /// <returns></returns>
        Task InitializeAsync();
    }
}

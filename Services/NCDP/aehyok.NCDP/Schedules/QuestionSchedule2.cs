using aehyok.Core.Schedule;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.NCDP.Schedules
{
    /// <summary>
    /// 测试调查问卷的功能
    /// </summary>
    public class QuestionSchedule2(IServiceScopeFactory serviceFactory) : CronScheduleService(serviceFactory)
    {
        protected override string Expression { get; set; } = "0/2 * * * * ?";

        protected override bool Singleton => true;

        protected override Task ProcessAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("实现调查问卷的功能");
            return Task.CompletedTask;
        }
    }
}

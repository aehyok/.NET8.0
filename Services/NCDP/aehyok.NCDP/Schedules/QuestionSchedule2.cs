using aehyok.CronTask;
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
    public class QuestionSchedule2 : CronScheduleService
    {
        protected override string Expression => "10 * * * * ?";

        protected override Task ProcessAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine(this.Expression, "表达式");
            Console.WriteLine("实现调查问卷的功能");
            return Task.CompletedTask;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Schedules
{
    public class QuestionSchedule2 : CronScheduleService
    {
        protected override string Expression => "* 50 * * * ?";

        protected override Task ProcessAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine(this.Expression, "表达式");
            Console.WriteLine("实现调查问卷的功能");
            return Task.CompletedTask;
        }
    }
}

using aehyok.CronTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.NCDP.Schedules
{
    public class SelfReportSchedule : CronScheduleService
    {
        protected override string Expression => "0 50 * * * ?";

        protected override Task ProcessAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine(this.Expression, "表达式");
            Console.WriteLine("实现自主填报的功能");
            return Task.CompletedTask;
        }
    }
}

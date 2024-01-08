using aehyok.Core.Domains;
using aehyok.EntityFrameworkCore.Mapping;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Mapping
{
    public class ScheduleTaskMapping : MapBase<ScheduleTask>
    {
        public override void Configure(EntityTypeBuilder<ScheduleTask> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.LastWriteTime).HasPrecision(7);
        }
    }
}

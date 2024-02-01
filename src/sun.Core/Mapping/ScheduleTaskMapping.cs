using sun.Core.Domains;
using sun.EntityFrameworkCore.Mapping;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Core.Mapping
{
    public class ScheduleTaskMapping : MapBase<SeedDataTask>
    {
        public override void Configure(EntityTypeBuilder<SeedDataTask> builder)
        {
            base.Configure(builder);

            // 可通过此设置DateTime类型的精度，但是不能超过数据库的精度6
            //builder.Property(x => x.LastWriteTime).HasPrecision(7);
        }
    }
}

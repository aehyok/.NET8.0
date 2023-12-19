using aehyok.Core.Mapping;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Task = aehyok.NCDP.Domains.Task;

namespace aehyok.NCDP.Mapping
{
    public class TaskMapping : MapBase<Task>
    {
        public override void Configure(EntityTypeBuilder<Task> builder)
        {
            base.Configure(builder);
            builder.Property(a => a.Name).HasMaxLength(50).IsRequired();
        }
    }
}

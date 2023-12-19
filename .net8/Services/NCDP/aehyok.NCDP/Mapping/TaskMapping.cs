using aehyok.Core.Mapping;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AutoTask = aehyok.NCDP.Domains.AutoTask;

namespace aehyok.NCDP.Mapping
{
    public class TaskMapping : MapBase<AutoTask>
    {
        public override void Configure(EntityTypeBuilder<AutoTask> builder)
        {
            base.Configure(builder);
            builder.Property(a => a.Name).HasMaxLength(50).IsRequired();
        }
    }
}

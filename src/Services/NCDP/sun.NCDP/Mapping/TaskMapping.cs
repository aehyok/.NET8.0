using sun.EntityFrameworkCore.Mapping;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AutoTask = sun.Core.Domains.Auto.AutoTask;

namespace sun.NCDP.Mapping
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

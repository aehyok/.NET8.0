using aehyok.Core.EntityFramework.MySql.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.EntityFramework.MySql.ModelBuildConfiguration
{
    public static class FlowConfiguration
    {
        public static void FlowInit(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FlowEntityState>(entity =>
            {
                entity.ToTable("FlowEntityState");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasColumnName("id");

                entity.Property(e => e.DisplayOrder)
                    .HasColumnType("int(11)")
                    .HasColumnName("displayOrder");

                entity.Property(e => e.FlowId)
                    .HasMaxLength(50)
                    .HasColumnName("flowId");

                entity.Property(e => e.StateDescript)
                    .HasMaxLength(1000)
                    .HasColumnName("stateDescript");

                entity.Property(e => e.StateDisplayName)
                    .HasMaxLength(100)
                    .HasColumnName("stateDisplayName");

                entity.Property(e => e.StateName)
                    .HasMaxLength(50)
                    .HasColumnName("stateName");

                entity.Property(e => e.StateType)
                    .HasMaxLength(10)
                    .HasColumnName("stateType");
            });

            modelBuilder.Entity<FlowEntityType>(entity =>
            {
                entity.ToTable("FlowEntityType");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .HasColumnName("description");

                entity.Property(e => e.DisplayOrder)
                    .HasColumnType("int(11)")
                    .HasColumnName("displayOrder");

                entity.Property(e => e.FlowName)
                    .HasMaxLength(100)
                    .HasColumnName("flowName");
            });

            modelBuilder.Entity<FlowStateTransition>(entity =>
            {
                entity.ToTable("FlowStateTransition");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasColumnName("id");

                entity.Property(e => e.ActionName)
                    .HasMaxLength(50)
                    .HasColumnName("actionName");

                entity.Property(e => e.ActionParameter)
                    .HasMaxLength(1000)
                    .HasColumnName("actionParameter");

                entity.Property(e => e.ActionTitle)
                    .HasMaxLength(50)
                    .HasColumnName("actionTitle");

                entity.Property(e => e.ActionType)
                    .HasColumnType("int(11)")
                    .HasColumnName("actionType");

                entity.Property(e => e.DisplayOrder)
                    .HasColumnType("int(11)")
                    .HasColumnName("displayOrder");

                entity.Property(e => e.StateId)
                    .HasMaxLength(50)
                    .HasColumnName("stateId");

                entity.Property(e => e.TargetStateId)
                    .HasMaxLength(50)
                    .HasColumnName("targetStateId");

                entity.Property(e => e.UserType)
                    .HasColumnType("int(11)")
                    .HasColumnName("userType");
            });
        }
    }
}

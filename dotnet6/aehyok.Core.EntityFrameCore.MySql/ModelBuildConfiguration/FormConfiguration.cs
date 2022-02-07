using aehyok.Core.EntityFramework.MySql.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.EntityFramework.MySql.ModelBuildConfiguration
{
    public static class FormConfiguration
    {
        public static void FormInit(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SystemForm>(entity =>
            {
                entity.ToTable("SystemForm");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasColumnName("id");

                entity.Property(e => e.DisplayOrder)
                    .HasColumnType("int(11)")
                    .HasColumnName("displayOrder");

                entity.Property(e => e.FormName)
                    .HasMaxLength(50)
                    .HasColumnName("formName");

                entity.Property(e => e.Remark)
                    .HasMaxLength(1000)
                    .HasColumnName("remark");

                entity.Property(e => e.ColumnList)
                    .HasMaxLength(50000)
                    .HasColumnName("columnList");

                entity.Property(e => e.TableList)
                    .HasMaxLength(50000)
                    .HasColumnName("tableList");
            });
        }
    }
}

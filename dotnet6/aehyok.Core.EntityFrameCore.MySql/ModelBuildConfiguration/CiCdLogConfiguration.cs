using aehyok.Core.EntityFramework.MySql.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.EntityFramework.MySql.ModelBuildConfiguration
{
    public static class CiCdLogConfiguration
    {
        public static void LogInit(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CiCdLog>(entity =>
            {
                entity.ToTable("CiCdLog");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasColumnName("id");


                entity.Property(e => e.Project)
                            .HasMaxLength(50)
                            .HasColumnName("project");

                entity.Property(e => e.Content)
                            .HasMaxLength(1000)
                            .HasColumnName("content");

                entity.Property(e => e.Version)
                            .HasMaxLength(20)
                            .HasColumnName("version");

                entity.Property(e => e.CreateTime)
                            .HasColumnType("datetime")
                            .HasColumnName("createTime");

                entity.Property(e => e.Type)
                            .HasMaxLength(20)
                            .HasColumnName("type");
            });
        }
    }
}

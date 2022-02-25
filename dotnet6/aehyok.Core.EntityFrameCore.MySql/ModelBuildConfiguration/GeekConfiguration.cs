using aehyok.Core.EntityFramework.MySql.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.EntityFramework.MySql.ModelBuildConfiguration
{
    public static class GeekConfiguration
    {
        public static void GeekInit(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GeekArticle>(entity =>
            {
                entity.ToTable("GeekArticle");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasColumnName("id");

                entity.Property(e => e.Json)
                    .HasMaxLength(50000)
                    .HasColumnName("json");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("createTime");

                entity.Property(e => e.ProductId)
                .HasMaxLength(50)
                .HasColumnName("productId");

                entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
                entity.Property(e => e.AuthorName)
                .HasMaxLength(50)
                .HasColumnName("authorName");
            });

            modelBuilder.Entity<GeekProduct>(entity =>
            {
                entity.ToTable("GeekProduct");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasColumnName("id");

                entity.Property(e => e.Json)
                    .HasMaxLength(50000)
                    .HasColumnName("json");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .HasColumnName("title");
                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .HasColumnName("type");
            });
        }
    }
}

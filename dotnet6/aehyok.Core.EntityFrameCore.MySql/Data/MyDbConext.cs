using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using aehyok.Core.EntityFrameCore.MySql.Models;

namespace aehyok.Core.EntityFrameCore.MySql.Data
{
    public partial class MyDbConext : DbContext
    {
        public MyDbConext()
        {
        }

        public MyDbConext(DbContextOptions<MyDbConext> options)
            : base(options)
        {
        }

        public virtual DbSet<BaseUser> BaseUsers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=139.186.205.7;port=3306;uid=aehyok;pwd=M9y2512!;database=metadata;allowzerodatetime=True;convertzerodatetime=True;charset=utf8mb4;sslmode=none", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.2.32-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_unicode_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<BaseUser>(entity =>
            {
                entity.ToTable("BaseUser");

                entity.HasComment("用户")
                    .HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.Account, "account")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(10)")
                    .HasColumnName("id");

                entity.Property(e => e.Account)
                    .HasMaxLength(256)
                    .HasColumnName("account")
                    .HasComment("用户账号，兼容微信id");

                entity.Property(e => e.Address)
                    .HasMaxLength(2048)
                    .HasColumnName("address")
                    .HasDefaultValueSql("''")
                    .HasComment("地址");

                entity.Property(e => e.AreaId)
                    .HasColumnType("int(10)")
                    .HasColumnName("areaId")
                    .HasComment("所属区域");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("createdAt")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.DepartmentIds)
                    .HasMaxLength(256)
                    .HasColumnName("departmentIds")
                    .HasDefaultValueSql("''")
                    .HasComment("所属部门Id");

                entity.Property(e => e.Description)
                    .HasMaxLength(2048)
                    .HasColumnName("description")
                    .HasDefaultValueSql("''")
                    .HasComment("描述/职务");

                entity.Property(e => e.Email)
                    .HasMaxLength(64)
                    .HasColumnName("email")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.HouseholdId)
                    .HasColumnType("int(11)")
                    .HasColumnName("householdId")
                    .HasDefaultValueSql("'0'")
                    .HasComment("户码Id");

                entity.Property(e => e.IsAuth)
                    .HasColumnType("int(10)")
                    .HasColumnName("isAuth")
                    .HasDefaultValueSql("'0'")
                    .HasComment("公众用户是否已认证，0未审核， 1待审核，2审核通过，3审核不通过");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.IsGrid)
                    .HasColumnType("int(10)")
                    .HasColumnName("isGrid")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否网格员，0否， 1一级网格员，2二级网格员(网格长)");

                entity.Property(e => e.IsLeader)
                    .HasColumnType("tinyint(2)")
                    .HasColumnName("isLeader")
                    .HasDefaultValueSql("'0'")
                    .HasComment("是否主管人员");

                entity.Property(e => e.LoginedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("loginedAt")
                    .HasDefaultValueSql("'0000-00-00 00:00:00'");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(64)
                    .HasColumnName("mobile")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.NickName)
                    .HasMaxLength(256)
                    .HasColumnName("nickName")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.ParkAreaId)
                    .HasColumnType("int(10)")
                    .HasColumnName("parkAreaId")
                    .HasDefaultValueSql("'0'")
                    .HasComment("园区id");

                entity.Property(e => e.Password)
                    .HasMaxLength(256)
                    .HasColumnName("password");

                entity.Property(e => e.PopulationId)
                    .HasColumnType("int(10)")
                    .HasColumnName("populationId")
                    .HasDefaultValueSql("'0'")
                    .HasComment("户籍人口表Id");

                entity.Property(e => e.PortraitFileId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("portraitFileId")
                    .HasDefaultValueSql("'0'")
                    .HasComment("头像id");

                entity.Property(e => e.RoleIds)
                    .HasMaxLength(64)
                    .HasColumnName("roleIds")
                    .HasDefaultValueSql("'0'")
                    .HasComment("角色id");

                entity.Property(e => e.Sex)
                    .HasColumnName("sex")
                    .HasDefaultValueSql("'0'")
                    .HasComment("男性1，女性2，未知0");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Test)
                    .HasMaxLength(255)
                    .HasColumnName("test");

                entity.Property(e => e.Type)
                    .HasColumnType("tinyint(2)")
                    .HasColumnName("type")
                    .HasComment("公众1村委2政务3企业4");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("updatedAt")
                    .HasDefaultValueSql("current_timestamp()");

                entity.Property(e => e.Wxopenid)
                    .HasMaxLength(256)
                    .HasColumnName("wxopenid")
                    .HasComment("小程序openid");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

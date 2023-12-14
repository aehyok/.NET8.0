using aehyok.EntityFramework.Entities;
using aehyok.EntityFramework.Extensions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.EntityFramework.DbContexts
{
    /// <summary>
    /// 基础的数据库上下文DvsContext
    /// </summary>
    public partial class DvsContext : DbContext
    {
        public DvsContext(DbContextOptions<DvsContext> options) : base(options)
        {

        }

        /// <summary>
        /// 指定连接字符串来连接到特定的数据库
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// 配置和修改 EFCore 的默认约定
        /// </summary>
        /// <param name="configurationBuilder"></param>
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
        }

        /// <summary>
        /// 于配置模型（实体和关系）
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Console.WriteLine("OnModelCreating");
            // 只有继承了IEntity的实体才会被注册到数据库上下文中,并且排除了NotMappedAttribute特性的实体
            modelBuilder.RegisterFromAssembly<IEntity>(a => !a.IsDefined(typeof(NotMappedAttribute), true));
            base.OnModelCreating(modelBuilder);
        }
    }
}

using aehyok.EntityFrameworkCore.Entities;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.EntityFrameworkCore.DbContexts
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
            // 如果执行数据迁移失败，可以打开下面的代码，然后在程序启动时，会弹出一个对话框，选择调试器，然后就可以调试了
            //Debugger.Launch();
            Console.WriteLine("OnModelCreating");
            // 只有继承了IEntity的实体才会被注册到数据库上下文中,并且排除了NotMappedAttribute特性的实体
            modelBuilder.RegisterFromAssembly<IEntity>(a => !a.IsDefined(typeof(NotMappedAttribute), true));

            //查询时全局统一过滤软删除的数据
            modelBuilder.ApplyGlobalFilterAsDeleted<ISoftDelete>(a => !a.IsDeleted);

            base.OnModelCreating(modelBuilder);

            modelBuilder.AddEntityComments();
        }
    }
}

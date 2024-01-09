using aehyok.EntityFrameworkCore.DbContexts;
using aehyok.EntityFrameworkCore.Repository;
using aehyok.Infrastructure;
using aehyok.Infrastructure.TypeFinders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace aehyok.EntityFrameworkCore
{
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// 初始化Mysql配置,将EFCore、DvsContext注入到容器中,
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddEFCoreAndMySql(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MySQL");
            services.AddTransient<DvsSaveChangeInterceptor>();

            services.AddDbContextPool<DbContext, DvsContext>((sp, options) =>
            {
                // 添加保存更改拦截器，处理软删除和数据审计，注入 ICurrentUser 以自动处理数据 CreateBy 和 UpdateBy
                //var interceptors = FindTypes.InAllAssemblies.Where(a => a.IsAssignableTo(typeof(Microsoft.EntityFrameworkCore.Diagnostics.ISaveChangesInterceptor)) && !a.IsInterface && !a.IsAbstract && a.Assembly.GetName().Name.StartsWith("DVS")).ToArray();
                //foreach (var interceptor in interceptors)
                //{
                //    var constructor = interceptor.GetConstructor(new[] { typeof(IServiceScopeFactory) });
                //    if (constructor != null)
                //    {
                //        var saveChangeInterceptor = constructor.Invoke(new[] { sp.GetService<IServiceScopeFactory>() }) as Microsoft.EntityFrameworkCore.Diagnostics.ISaveChangesInterceptor;
                //        options.AddInterceptors(saveChangeInterceptor);
                //    }
                //}
                // 注册自定义拦截器
                options.AddInterceptors(sp.GetRequiredService<DvsSaveChangeInterceptor>());
                

                // 移除外键
                options.UseRemoveForeignKeys();

                // 禁止跟踪
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

                var server = new MariaDbServerVersion(new Version(10, 2, 36));

                options.UseMySql(connectionString, server, mysqlOptions =>
                {
                    // 设置数据迁移的程序集名称
                    mysqlOptions.MigrationsAssembly("aehyok.SystemService");
                    //if (moduleKey == AppConstants.SYSTEM_MIGRATIONS_MODULE_KEY)
                    //{
                    //    mysqlOptions.MigrationsAssembly("DVS.SystemService");
                    //}

                    //mysqlOptions.UseNetTopologySuite();  // 操作GEO空间数据的时候需要开启

                    // EnableSensitiveDataLogging(true) 可设置为true 开启敏感数据的记录
                }).EnableSensitiveDataLogging().EnableDetailedErrors();
            }, poolSize: 1024);
            services.AddScoped(typeof(IServiceBase<,>), typeof(ServiceBase<,>));
            services.AddScoped(typeof(IServiceBase<>), typeof(ServiceBase<>));

            return services;
        }

        /// <summary>
        /// 移除表结构中的外键约束
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static DbContextOptionsBuilder UseRemoveForeignKeys(this DbContextOptionsBuilder builder)
        {
            builder.ReplaceService<IMigrationsSqlGenerator, MigrationsSqlGenerator>();
            return builder;
        }
    }
}

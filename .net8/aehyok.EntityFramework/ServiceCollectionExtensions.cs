using aehyok.Core;
using aehyok.EntityFramework.DbContexts;
using aehyok.EntityFramework.Repository;
using aehyok.Infrastructure.TypeFinders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.EntityFramework
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

                // 移除外键
                //options.UseRemoveForeignKeys();

                // 禁止跟踪
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

                var server = new MariaDbServerVersion(new Version(10, 2, 36));

                options.UseMySql(connectionString, server, mysqlOptions =>
                {
                    // 设置数据迁移的程序集名称
                    mysqlOptions.MigrationsAssembly("aehyok.Schedules");
                    //if (moduleKey == AppConstants.SYSTEM_MIGRATIONS_MODULE_KEY)
                    //{
                    //    mysqlOptions.MigrationsAssembly("DVS.SystemService");
                    //}

                    //mysqlOptions.UseNetTopologySuite();
                }).EnableSensitiveDataLogging().EnableDetailedErrors();
            }, poolSize: 1024);
            services.AddScoped(typeof(IServiceBase<,>), typeof(ServiceBase<,>));
            services.AddScoped(typeof(IServiceBase<>), typeof(ServiceBase<>));

            services.AddServices();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            var types = TypeFinders.SearchTypes(typeof(IScopedDependency), TypeFinders.TypeClassification.Interface);

            foreach (var type in types)
            {
                var interfaces = type.GetInterfaces().Where(t => !t.IsGenericType && t != typeof(IScopedDependency));

                foreach (var interfaceType in interfaces)
                {
                    services.AddService(interfaceType, type, ServiceLifetime.Scoped);
                }
            }

            return services;
        }

        public static IServiceCollection AddService(this IServiceCollection services, Type interfaceType, Type implementationType, ServiceLifetime lifetime)
        {
            services.Add(new ServiceDescriptor(implementationType, implementationType, lifetime));
            services.Add(new ServiceDescriptor(interfaceType, implementationType, lifetime));

            return services;
        }
    }
}

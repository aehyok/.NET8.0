using aehyok.Core.Options;
using aehyok.Infrastructure.TypeFinders;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core
{
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// 设置开发环境项目启动后的默认启动路由
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSetStartDefaultRoute(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.Use(async (context, next) =>
                {
                    if (context.Request.Path.Value.Equals("/"))
                    {
                        context.Response.Redirect("/docs/index.html");
                        return;
                    }
                    await next(context);
                });
            }
            return app;
        }

        /// <summary>
        /// 自动注册所有实现 IOptions 的配置选项
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
        {
            PrintConfigurationProvider(configuration);

            services.AddOptions();

            // 注册所有 Options

            var types = TypeFinders.SearchTypes(typeof(IOptions), TypeFinders.TypeClassification.Interface);

            //using reflection to invoke the Configure<TOption> extension
            Type extensionClass = typeof(OptionsConfigurationServiceCollectionExtensions);
            //get the desired extension method by name and using the expected arguments
            Type[] parameterTypes = new[] { typeof(IServiceCollection), typeof(IConfiguration) };
            string extensionName = nameof(OptionsConfigurationServiceCollectionExtensions.Configure);
            MethodInfo configureExtension = extensionClass.GetMethod(extensionName, parameterTypes);

            foreach (var optionType in types)
            {
                var instance = (IOptions)Activator.CreateInstance(optionType);

                IConfiguration section = instance.SectionName.IsNullOrEmpty() ? configuration : configuration.GetSection(instance.SectionName);
                MethodInfo extensionMethod = configureExtension.MakeGenericMethod(optionType);
                extensionMethod.Invoke(services, new object[] { services, section });
            }

            return services;
        }

        /// <summary>
        /// 程序启动时打印配置文件地址
        /// </summary>
        /// <param name="configuration"></param>
        public static void PrintConfigurationProvider(IConfiguration configuration)
        {
            var root = (IConfigurationBuilder)configuration;

            foreach (JsonConfigurationSource source in root.Sources.Where(a => a.GetType() == typeof(JsonConfigurationSource)))
            {
                var path = Path.Combine(((PhysicalFileProvider)source.FileProvider).Root, source.Path);
                //Log.Information($"配置文件({(File.Exists(path) ? "有效" : "无效")}):{path}");
                Console.WriteLine($"配置文件({(File.Exists(path) ? "有效" : "无效")}):{path}");
            }
        }
    }
}

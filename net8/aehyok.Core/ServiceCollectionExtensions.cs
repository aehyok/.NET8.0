using aehyok.Infrastructure.TypeFinders;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using aehyok.RabbitMQ;
using aehyok.EntityFrameworkCore;
using aehyok.Swagger;
using aehyok.Redis;
using aehyok.CronTask;
using System.Runtime.Loader;
using aehyok.Infrastructure.Options;
using aehyok.Infrastructure;
using aehyok.Serilog;
using AutoMapper;
using aehyok.Infrastructure.Filters;
using Microsoft.AspNetCore.StaticFiles;
using aehyok.Core.HostedServices;
using System.Text.Json;
using aehyok.Infrastructure.Middlewares;
using aehyok.Infrastructure.TypeFinders;
using Microsoft.Extensions.Options;

namespace aehyok.Core
{
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// 项目初始化函数
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="moduleKey"></param>
        /// <param name="moduleTitle"></param>
        /// <param name="isSystemService">是否是SystemService这个服务</param>
        /// <returns></returns>
        public static async Task InitAppliation(this WebApplicationBuilder builder, string moduleKey, string moduleTitle, bool isSystemService = false)
        {
            builder.AddBuilderServices(moduleKey, moduleTitle, isSystemService);

            var app = builder.Build();

            app.UseApp(moduleKey, moduleTitle, isSystemService);

            await app.RunAsync();
        }

        /// <summary>
        /// 应用程序启动时初始化
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="moduleKey"></param>
        /// <param name="moduleTitle"></param>
        /// <param name="isSystemService">是否是SystemService这个服务</param>
        /// <returns></returns>
        public static WebApplicationBuilder AddBuilderServices(this WebApplicationBuilder builder, string moduleKey, string moduleTitle, bool isSystemService = false)
        {
            builder.Host.InitHostAndConfig(moduleKey);

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(moduleKey, moduleTitle);

            builder.Services.ConfigureOptions(builder.Configuration);

            // 注册IHttpContextAccessor
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddHttpLogging(options =>
            {
                options.RequestBodyLogLimit = 1024 * 1024;
                options.ResponseBodyLogLimit = 1024 * 1024;
                options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
                options.MediaTypeOptions.AddText("application/json");
            });

            builder.Services.AddEFCoreAndMySql(builder.Configuration);

            builder.Services.AddServices<ITransientDependency>(ServiceLifetime.Transient);
            builder.Services.AddServices<IScopedDependency>(ServiceLifetime.Scoped);
            //builder.Services.AddServices</*ISingletonDependency*/>(ServiceLifetime.Singleton);

            builder.Services.AddControllers(options =>
            {
                //统一接口返回的处理
                options.Filters.Add<RequestAsyncResultFilter>();

                //接口异常统一处理
                options.Filters.Add<ApiAsyncExceptionFilter>();
            }).AddJsonOptions(options =>
            {
                // 针对字段 long 类型，序列化时转换为字符串
                options.JsonSerializerOptions.Converters.Add(new JsonLongConverter());
            });

            builder.Services.AddAllAutoMapper();

            builder.Services.AddRabbitMQ(builder.Configuration);

            builder.Services.AddSingleton<IContentTypeProvider, FileExtensionContentTypeProvider>();

            //builder.Services.AddTransient<ITransientDependency>();
            //builder.Services.AddScoped<IScopedDependency>();

            if (isSystemService) 
            {
                builder.Services.AddCronTask();

                builder.Services.AddHostedService<ExecuteSeedDataService>();
            }
            else
            {
                // 非系统服务才需要初始化ApiResource
                //开发环境就不每次执行了，因为会重复执行，部署后每次执行问题不大(每个微服务单独执行)
                if (!builder.Environment.IsDevelopment())
                {
                    builder.Services.AddHostedService<InitApiResourceService>();
                }
            }

            return builder;
        }

        /// <summary>
        /// 应用程序启动时 注册中间件
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseApp(this WebApplication app, string moduleKey, string moduleTitle, bool isSystemService = false)
        {
            App.Init(app.Services);

            app.UseSetStartDefaultRoute();

            app.UseSwagger(moduleKey, moduleTitle);

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseHttpLogging();

            app.UseRedis(app.Configuration);

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseStaticFileServer();

            if(isSystemService)
            {
                app.AddRabbitMQEventBus();
            }

            return app;
        }

        /// <summary>
        /// 初始化 Host，加载配置文件
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="moduleKey"></param>
        /// <returns></returns>
        public static IHostBuilder InitHostAndConfig(this IHostBuilder builder, string moduleKey)
        {
            Thread.CurrentThread.Name = moduleKey;

            // 例如 aehyok.NCDP 最开始代码中没有使用到，是不会加载到内存中的，所以需要手动加载
            var assemblyFiles = Directory.GetFiles(AppContext.BaseDirectory, "aehyok.*.dll");
            foreach (var assemblyFile in assemblyFiles)
            {
                AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblyFile);
            }

            builder.ConfigureAppConfiguration((context, options) =>
            {
                // 正式环境配置文件路径
                options.AddJsonFile(Path.Combine(AppContext.BaseDirectory, $"../etc/appsettings.json"), true, true);
                options.AddJsonFile(Path.Combine(AppContext.BaseDirectory, $"../etc/{moduleKey}-appsettings.json"), true, true);

                // 本地开发环境配置文件路径
                options.AddJsonFile(Path.Combine(AppContext.BaseDirectory, $"../../../../../../etc/appsettings.json"), true, true);
                options.AddJsonFile(Path.Combine(AppContext.BaseDirectory, $"../../../../../../etc/{moduleKey}-appsettings.json"), true, true);
            });

            builder.UseLog();

            return builder;
        }

        /// <summary>
        /// 上传文件的静态文件服务
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseStaticFileServer(this IApplicationBuilder app)
        {
            var storageOptions = app.ApplicationServices.GetService<IOptions<StorageOptions>>();
            var staticDirectory = Path.Combine(AppContext.BaseDirectory, storageOptions.Value.Path.IsNullOrEmpty() ? "uploads" : storageOptions.Value.Path);

            Directory.CreateDirectory(staticDirectory);

            app.UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = true,
                RequestPath = "/static",
                FileProvider = new PhysicalFileProvider(staticDirectory)
            });

            return app;
        }

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
        /// 注册所有 AutoMapper 配置信息
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAllAutoMapper(this IServiceCollection services)
        {
            var types = TypeFinders.SearchTypes(typeof(Profile), TypeFinders.TypeClassification.Class) .ToArray();
            services.AddAutoMapper(types);

            return services;
        }

        /// <summary>
        /// 自动注册所有实现 IOptions 的配置选项
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        private static IServiceCollection ConfigureOptions(this IServiceCollection services, ConfigurationManager configuration)
        {
            PrintConfigurationProvider(configuration);

            services.AddOptions();

            // 注册所有 Options
            var types = TypeFinders.SearchTypes(typeof(IOptions), TypeFinders.TypeClassification.Interface);

            //using reflection to invoke the Configure<TOption> extension
            Type extensionClass = typeof(OptionsConfigurationServiceCollectionExtensions);
            //get the desired extension method by name and using the expected arguments
            Type[] parameterTypes = [typeof(IServiceCollection), typeof(IConfiguration)];
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
        private static void PrintConfigurationProvider(IConfiguration configuration)
        {
            var root = (IConfigurationBuilder)configuration;

            foreach (JsonConfigurationSource source in root.Sources.Where(a => a.GetType() == typeof(JsonConfigurationSource)).Cast<JsonConfigurationSource>())
            {
                var path = Path.Combine(((PhysicalFileProvider)source.FileProvider).Root, source.Path);
                //Log.Information($"配置文件({(File.Exists(path) ? "有效" : "无效")}):{path}");
                Console.WriteLine($"配置文件({(File.Exists(path) ? "有效" : "无效")}):{path}");
            }
        }
    }
}

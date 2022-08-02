using aehyok.Base;
using aehyok.Core.Config;
using aehyok.Core.Data;
using aehyok.Core.EntityFramework.MySql;
using aehyok.Core.IRepository;
using aehyok.Core.MySql;
using aehyok.Core.MySqlDataAccessor;
using aehyok.Core.Repository;
using aehyok.Core.WebApi.Utils;
using aehyok.Lib;
using aehyok.Lib.Config;
using aehyok.Lib.Services;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace aehyok.Core.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //ConfigInitialize.ConnectionString = Configuration.GetConnectionString("MySQL");
            //ConfigInitialize.ConnectionString = ConfigurationManager.GetConfig("ConnectionStrings:MySQL");
            MDEConfig.ConnectionString = Configuration.GetConnectionString("MySQL");
            MDEConfig.QueryString = Configuration.GetConnectionString("Query");
            //MDEConfig.RedisConnectionString = Configuration.GetSection("Redis:ConnectionString").Value;
            MDEConfig.DbUser = Configuration.GetSection("DbUser").Value;
            // 添加Swagger
            services.AddSwaggerGen(optios =>
            {
                optios.SwaggerDoc("v1", new OpenApiInfo { Title = "Tally.so私服接口API", Version = "v1" });

                optios.OperationFilter<SwaggerOperationFilter>();
                // 获取xml文件名
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                // 获取xml文件路径
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                // 添加控制器层注释，true表示显示控制器注释
                optios.IncludeXmlComments(xmlPath, true);
            });
            services.AddScoped<IEncryptionService, EncryptionService>();

            services.AddScoped<IMetaDataManager, MyDA_MetaDataManager>();

            services.AddScoped<IMetaDataQuery, MyDA_MetaDataQuery>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });
            services.AddControllers(options =>
            {
                options.Filters.Add<JsonResultFilter>();
                options.Filters.Add<AuthorizationFilter>();
            }).AddControllersAsServices();

            services.AddCors(options => {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.AllowAnyMethod()
                        .SetIsOriginAllowed(_ => true)
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });
        }

        /// <summary>
        /// Autofac容器使用
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // 在这里添加服务注册
            builder.RegisterType<HttpContextAccessor>();
            //builder.RegisterType<TestRepository>();

            var baseType = typeof(IDependency);
            List<Assembly> assemblyList = new();

            ////var list = AppDomain.CurrentDomain.GetAssemblies().OrderBy(item => item.FullName).ToList();
            ////var list= AssemblyLoadContext.Default.Assemblies.OrderBy(item => item.FullName).ToList();


            var dlls = DependencyContext.Default.CompileLibraries
            .SelectMany(x => x.ResolveReferencePaths())
            .Distinct()
            .Where(x => x.Contains(Directory.GetCurrentDirectory()))
            .ToList();
            foreach (var item in dlls)
            {
                AssemblyLoadContext.Default.LoadFromAssemblyPath(item);
            }
            var list = AssemblyLoadContext.Default.Assemblies.Where(item => item.FullName.Contains("aehyok.Core")).OrderBy(item => item.FullName).ToList();

            builder.RegisterAssemblyTypes(list.ToArray())
                .Where(t => baseType.IsAssignableFrom(t) && t != baseType)
                .AsImplementedInterfaces();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // 添加Swagger有关中间件
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "通用接口API");
            });

            app.UseHttpsRedirection();


            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
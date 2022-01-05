using aehyok.Core.Config;
using aehyok.Core.Data;
using aehyok.Core.IRepository;
using aehyok.Core.Repository;
using aehyok.Core.WebApi.Utils;
using aehyok.Lib.Config;
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

namespace aehyok.Core.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

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
            // ���Swagger
            services.AddSwaggerGen(optios =>
            {
                optios.SwaggerDoc("v1", new OpenApiInfo { Title = "���ϰ�˽���ӿ�API", Version = "v1" });

                optios.OperationFilter<SwaggerOperationFilter>();
                // ��ȡxml�ļ���
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                // ��ȡxml�ļ�·��
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                // ��ӿ�������ע�ͣ�true��ʾ��ʾ������ע��
                optios.IncludeXmlComments(xmlPath, true);
            });

            services.AddControllers(options =>
            {
                options.Filters.Add<JsonResultFilter>();
                options.Filters.Add<AuthorizationFilter>();
            }).AddControllersAsServices();
        }

        /// <summary>
        /// Autofac����ʹ��
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // ��������ӷ���ע��
            builder.RegisterType<HttpContextAccessor>();
            //builder.RegisterType<TestRepository>();

            var baseType = typeof(IDependency);
            List<Assembly> assemblyList = new List<Assembly>();

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

            // ���Swagger�й��м��
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "���ϰ�˽���ӿ�API");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

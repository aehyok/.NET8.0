using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using aehyok.Core.Data;
using aehyok.Core.DataBase;
using aehyok.Core.IRepository;
using aehyok.Core.MySql;
using aehyok.Core.Repository;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;

namespace aehyok.Core.Web
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
            services.AddControllers()
                    .AddNewtonsoftJson(options =>
                    {
                        //������֧�ַ������ݼ�����ĸ���պ������
                        //options.SerializerSettings.ContractResolver = new DefaultContractResolver();   
                        
                        //����ѭ�����õ�����
                        //options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                        
                        //����ʱ���ʽ
                        //options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
                    });

            services.AddControllersWithViews()
                .AddControllersAsServices()      //
                .AddRazorRuntimeCompilation();   //��������ʱ����

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
                AddCookie(x =>
                {
                    x.Cookie.Name = "aehyok_login";
                    //��¼��ַ
                    x.LoginPath = "/Account/Login";
                    x.AccessDeniedPath = "/Home/Error";
                    x.Cookie.MaxAge =new TimeSpan(0,30,0);  //����Cookie��Чʱ��
                });

            //��ӻ���
            services.AddMemoryCache();
        }

        /// <summary>
        /// Autofac����ʹ��
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // ��������ӷ���ע��
            builder.RegisterType<HttpContextAccessor>();
            //builder.RegisterType<MySqlDbAccessor>();
            //builder.RegisterType<AccountRepository>();
            //builder.RegisterType<MenuRepository>();

            var baseType = typeof(IDependency);
            List<Assembly> assemblyList = new List<Assembly>();
            
            //var list = AppDomain.CurrentDomain.GetAssemblies().OrderBy(item => item.FullName).ToList();
            //var list= AssemblyLoadContext.Default.Assemblies.OrderBy(item => item.FullName).ToList();

            var dlls = DependencyContext.Default.CompileLibraries
                .SelectMany(x => x.ResolveReferencePaths())
                .Distinct()
                .Where(x => x.Contains(Directory.GetCurrentDirectory()))
                .ToList();
            foreach (var item in dlls)
            {
                AssemblyLoadContext.Default.LoadFromAssemblyPath(item);
            }
            var list = AssemblyLoadContext.Default.Assemblies.Where(item=>item.FullName.Contains("aehyok.Core")).OrderBy(item => item.FullName).ToList();

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
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aehyok.Core.DataBase;
using aehyok.Core.IRepository;
using aehyok.Core.MySql;
using aehyok.Core.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
                        //可设置支持返回数据键首字母按照后端设置
                        //options.SerializerSettings.ContractResolver = new DefaultContractResolver();   
                        
                        //忽略循环引用得问题
                        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                        
                        //设置时间格式
                        options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
                    });

            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();   //启用运行时编译
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IDbAccossor,MySqlDbAccessor>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IMenuRepository, MenuRepository>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
                AddCookie(x =>
                {
                    x.Cookie.Name = "aehyok_login";
                    //登录地址
                    x.LoginPath = "/Account/Login";
                    x.AccessDeniedPath = "/Home/Error";
                    x.Cookie.MaxAge =new TimeSpan(0,30,0);  //设置Cookie有效时间
                });
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

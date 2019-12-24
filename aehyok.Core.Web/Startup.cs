using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using aehyok.Core.AutoMapper;
using aehyok.Core.Data;
using aehyok.Core.DataBase;
using aehyok.Core.IRepository;
using aehyok.Core.MySql;
using aehyok.Core.Repository;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Senparc.CO2NET;
using Senparc.CO2NET.RegisterServices;
using Senparc.Weixin;
using Senparc.Weixin.Entities;
using Senparc.Weixin.RegisterServices;

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

            services.AddSenparcWeixinServices(Configuration);

            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "��ѯ����ƽ̨",
                    Description = "��ѯ����ƽ̨Api���⿪�Žӿ�",
                    //TermsOfService = new Uri("https://example.com/terms"),
                    //Contact = new OpenApiContact
                    //{
                    //    Name = "Shayne Boyer",
                    //    Email = string.Empty,
                    //    Url = new Uri("https://twitter.com/spboyer"),
                    //},
                    //License = new OpenApiLicense
                    //{
                    //    Name = "Use under LICX",
                    //    Url = new Uri("https://example.com/license"),
                    //}
                });
                c.IncludeXmlComments(xmlPath);
            });


            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapping());
            });
            //����AutoMapper
            AutoMapperExtension.AddAutoMapperProfiles(services);

            //services.AddScoped<IMapper, Mapper>();
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IOptions<SenparcSetting> senparcSetting, IOptions<SenparcWeixinSetting> senparcWeixinSetting)
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

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "aehyok Web Api V1");
                //c.RoutePrefix = string.Empty;
            });

 
                                                      ;

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // ���� CO2NET ȫ��ע�ᣬ���룡
            // ���� UseSenparcGlobal() �ĸ����÷��� CO2NET Demo��https://github.com/Senparc/Senparc.CO2NET/blob/master/Sample/Senparc.CO2NET.Sample.netcore3/Startup.cs
            var registerService = app.UseSenparcGlobal(env, senparcSetting.Value, globalRegister =>
            {
                #region CO2NET ȫ������

                #region ȫ�ֻ������ã����裩

                //��ͬһ���ֲ�ʽ����ͬʱ�����ڶ����վ��Ӧ�ó���أ�ʱ������ʹ�������ռ佫����루�Ǳ��룩
                globalRegister.ChangeDefaultCacheNamespace("DefaultCO2NETCache");

                #region ���ú�ʹ�� Redis          -- DPBMARK Redis

                //����ȫ��ʹ��Redis���棨���裬������
                //if (UseRedis(senparcSetting.Value, out string redisConfigurationStr))//����Ϊ�˷��㲻ͬ�����Ŀ����߽������ã��������жϵķ�ʽ��ʵ�ʿ�������һ����ȷ���ģ������if�������Ժ���
                //{
                //    /* ˵����
                //    * 1��Redis �������ַ�����Ϣ��� Config.SenparcSetting.Cache_Redis_Configuration �Զ���ȡ��ע�ᣬ�粻��Ҫ�޸ģ��·��������Ժ���
                //    /* 2�������ֶ��޸ģ�����ͨ���·� SetConfigurationOption �����ֶ����� Redis ������Ϣ�����޸����ã����������ã�
                //    */
                //    Senparc.CO2NET.Cache.Redis.Register.SetConfigurationOption(redisConfigurationStr);

                //    //���»�������ȫ�ֻ�������Ϊ Redis
                //    Senparc.CO2NET.Cache.Redis.Register.UseKeyValueRedisNow();//��ֵ�Ի�����ԣ��Ƽ���
                //                                                              //Senparc.CO2NET.Cache.Redis.Register.UseHashRedisNow();//HashSet�����ʽ�Ļ������

                //    //Ҳ����ͨ�����·�ʽ�Զ��嵱ǰ��Ҫ���õĻ������
                //    //CacheStrategyFactory.RegisterObjectCacheStrategy(() => RedisObjectCacheStrategy.Instance);//��ֵ��
                //    //CacheStrategyFactory.RegisterObjectCacheStrategy(() => RedisHashSetObjectCacheStrategy.Instance);//HashSet
                //}
                //������ﲻ����Redis�������ã���Ŀǰ����Ĭ��ʹ���ڴ滺�� 

                #endregion                        // DPBMARK_END

                #region ���ú�ʹ�� Memcached      -- DPBMARK Memcached

                //����Memcached���棨���裬������
                //if (UseMemcached(senparcSetting.Value, out string memcachedConfigurationStr)) //����Ϊ�˷��㲻ͬ�����Ŀ����߽������ã��������жϵķ�ʽ��ʵ�ʿ�������һ����ȷ���ģ������if�������Ժ���
                //{
                //    app.UseEnyimMemcached();

                //    /* ˵����
                //    * 1��Memcached �������ַ�����Ϣ��� Config.SenparcSetting.Cache_Memcached_Configuration �Զ���ȡ��ע�ᣬ�粻��Ҫ�޸ģ��·��������Ժ���
                ///* 2�������ֶ��޸ģ�����ͨ���·� SetConfigurationOption �����ֶ����� Memcached ������Ϣ�����޸����ã����������ã�
                //    */
                //    Senparc.CO2NET.Cache.Memcached.Register.SetConfigurationOption(memcachedConfigurationStr);

                //    //���»�������ȫ�ֻ�������Ϊ Memcached
                //    Senparc.CO2NET.Cache.Memcached.Register.UseMemcachedNow();

                //    //Ҳ����ͨ�����·�ʽ�Զ��嵱ǰ��Ҫ���õĻ������
                //    CacheStrategyFactory.RegisterObjectCacheStrategy(() => MemcachedObjectCacheStrategy.Instance);
                //}

                #endregion                        //  DPBMARK_END

                #endregion

                #region ע����־�����裬���飩

                //globalRegister.RegisterTraceLog(ConfigTraceLog);//����TraceLog

                #endregion

                #region APM ϵͳ����״̬ͳ�Ƽ�¼����

                //����APM�������ʱ�䣨Ĭ������¿��Բ������ã�
                //CO2NET.APM.Config.EnableAPM = true;//Ĭ���Ѿ�Ϊ�����������Ҫ�رգ�������Ϊ false
                //CO2NET.APM.Config.DataExpire = TimeSpan.FromMinutes(60);

                #endregion

                #endregion
            }, true)
                //ʹ�� Senparc.Weixin SDK
                .UseSenparcWeixin(senparcWeixinSetting.Value, weixinRegister =>
                {
                    #region ΢���������

                    /* ΢�����ÿ�ʼ
                    * 
                    * ���鰴������˳�����ע�ᣬ�����뽫������ڵ�һλ��
                    */

                    #region ΢�Ż��棨���裬����������ÿ�ͷ����ȷ���������������������ע�����ʹ����ȷ�����ã�

                    //΢�ŵ� Redis ���棬�����ʹ����ע�͵�������ǰ���뱣֤������Ч��������״�         -- DPBMARK Redis
                    //if (UseRedis(senparcSetting.Value, out _))
                    //{
                    //    app.UseSenparcWeixinCacheRedis();
                    //}                                                                                     // DPBMARK_END

                    //// ΢�ŵ� Memcached ���棬�����ʹ����ע�͵�������ǰ���뱣֤������Ч��������״�    -- DPBMARK Memcached
                    //if (UseMemcached(senparcSetting.Value, out _))
                    //{
                    //    app.UseSenparcWeixinCacheMemcached();
                    //}                                                                                      // DPBMARK_END

                    #endregion

                        ;

                    /* ΢�����ý��� */

                    #endregion
                });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

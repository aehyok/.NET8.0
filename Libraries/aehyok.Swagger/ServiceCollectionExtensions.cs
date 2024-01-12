using aehyok.Infrastructure.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;
using Unchase.Swashbuckle.AspNetCore.Extensions.Options;

namespace aehyok.Swagger
{
    public class SwaggerOptions : IOptions
    {
        public string SectionName => "Swagger";

        /// <summary>
        /// 启用
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 生成 Swagger 文档
        /// </summary>
        public bool SwaggerGen { get; set; }

        /// <summary>
        /// 启用 Swagger UI
        /// </summary>
        public bool SwaggerUI { get; set; }

        public List<SwaggerEndpoint> Endpoints { get; set; }
    }

    public class SwaggerEndpoint
    {
        public string Url { get; set; }

        public string Name { get; set; }
    }


    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加 Swagger 文档
        /// </summary>
        /// <param name="services"></param>
        /// <param name="name"></param>
        /// <param name="title"></param>
        /// <param name="version"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerGen(this IServiceCollection services, string name, string title, string version = "v1", string description = "")
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(name.Replace("aehyok-", ""), new OpenApiInfo { Title = title, Version = version, Description = description });

                options.AddSecurityDefinition("Token", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Description = "Token"
                });

                var action = new Action<FixEnumsOptions>(o =>
                {
                    o.IncludeDescriptions = true;
                    o.IncludeXEnumRemarks = true;
                    o.DescriptionSource = DescriptionSources.DescriptionAttributesThenXmlComments;
                });
                options.AddEnumsWithValuesFixFilters(action);

                // 加载程序运行目录下的所有 xml 注释文档
                Directory.GetFiles(AppContext.BaseDirectory, "*.xml").ToList().ForEach(comment => options.IncludeXmlComments(comment, true));

                options.OperationFilter<HttpHeaderFilter>(Array.Empty<object>());
            });

            return services;
        }


        public static IApplicationBuilder UseSwagger(this WebApplication app, string name, string title, string routePrefix = "docs", string documentTilte = "接口文档")
        {
            var swaggerOptions = app.Services.GetService<IOptions<SwaggerOptions>>();

            app.UseSwagger(options =>
            {
                options.RouteTemplate = "api/{documentName}/swagger.json";
            });

            if (app.Environment.IsDevelopment() || swaggerOptions.Value.SwaggerUI)
            {
                app.UseSwaggerUI(options =>
                {
                    options.RoutePrefix = routePrefix;
                    options.DocumentTitle = documentTilte;

                    options.Interceptors.RequestInterceptorFunction = "function(request){return dvs.auth.requestInterceptor(request);}";

                    if (app.Environment.IsDevelopment() || swaggerOptions.Value.Endpoints == null)
                    {
                        options.SwaggerEndpoint($"/api/{name.Replace("aehyok-", "")}/swagger.json", title);
                    }
                    else
                    {
                        swaggerOptions.Value?.Endpoints?.ForEach(a => options.SwaggerEndpoint(a.Url, a.Name));
                    }

                    var commonOptions = app.Services.GetService<IOptions<CommonOptions>>();
                    // 将 配置文件中的 Host 赋值给 Swagger UI 的 window.dvs.host，主要是为了让非 Basic 项目运行是可以使用线上开发环境登录获取 Token。
                    options.HeadContent = $"<script type='text/javascript'>var dvs = dvs || {{}};dvs.host='{commonOptions.Value.Host}'?'{commonOptions.Value.Host}':location.origin;</script>";

                    options.InjectJavascript("/docs/static/dvs-swagger.js");
                    options.InjectStylesheet("/docs/static/dvs-swagger.css");

                    //此处需要将文件设置为嵌入的资源
                    options.IndexStream = () => Assembly.GetExecutingAssembly().GetManifestResourceStream("aehyok.Swagger.Resources.dvs-swagger.html");
                });

                app.MapGet("/docs/static/dvs-swagger.js", async () =>
                {
                    var fileProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());

                    var file = fileProvider.GetFileInfo("Resources/dvs-swagger.js");

                    if (file.Exists)
                    {
                        using var stream = file.CreateReadStream();

                        var bytes = new byte[stream.Length];
                        await stream.ReadAsync(bytes);

                        return Results.File(bytes, contentType: "application/javascript");
                    }
                    return Results.NotFound();
                });

                app.MapGet("/docs/static/dvs-swagger.css", async () =>
                {
                    var fileProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());    
                    
                    var file = fileProvider.GetFileInfo("Resources/dvs-swagger.css");

                    if (file.Exists)
                    {
                        using var stream = file.CreateReadStream();

                        var bytes = new byte[stream.Length];
                        await stream.ReadAsync(bytes);

                        return Results.File(bytes, contentType: "text/css");
                    }
                    return Results.NotFound();
                });
            }

            return app;
        }
    }
}

using aehyok.Core.Domains;
using aehyok.Core.Services;
using aehyok.Infrastructure;
using aehyok.Redis;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Reflection;

namespace aehyok.Core.HostedServices
{
    /// <summary>
    /// 程序启动后通过后台任务初始化 API 接口资源
    /// </summary>
    public class InitApiResourceService(IServiceProvider services, ILogger<InitApiResourceService> logger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation($"开始执行{nameof(InitApiResourceService)}后台任务");
            var watch = new Stopwatch();
            watch.Start();

            using var scope = services.CreateScope();
            var redisDatabaseProvider = scope.ServiceProvider.GetRequiredService<IRedisService>();

            var lockKey = $"Task.RegisterApi.{AppDomain.CurrentDomain.FriendlyName}";
            var token = Guid.NewGuid().ToString("N");

            // 获取锁
            if (await redisDatabaseProvider.SetAsync(lockKey, token, TimeSpan.FromSeconds(10), CSRedis.RedisExistence.Nx))
            {
                var actionDescriptorProvider = scope.ServiceProvider.GetRequiredService<IActionDescriptorCollectionProvider>();
                var actions = actionDescriptorProvider.ActionDescriptors.Items;

                var assemblyName = Assembly.GetEntryAssembly().GetName().Name;
                var apiResourceService = scope.ServiceProvider.GetRequiredService<IApiResrouceCoreService>();

                var mapper = scope.ServiceProvider.GetService<IMapper>();

                foreach (ControllerActionDescriptor descriptor in actions)
                {
                    var resource = new ApiResource
                    {
                        NameSpace = descriptor.ControllerTypeInfo.Namespace,
                        ActionName = descriptor.ActionName,
                        ControllerName = descriptor.ControllerName,
                        RoutePattern = descriptor.AttributeRouteInfo.Template,
                        // 获取 Action 注释
                        Name = DocsHelper.GetMethodComments(assemblyName, descriptor.MethodInfo)
                    };
                    if (resource.Name.IsNullOrEmpty())
                    {
                        resource.Name = descriptor.ActionName;
                    }

                    // 获取 Controller 注释
                    resource.GroupName = DocsHelper.GetTypeComments(assemblyName, descriptor.ControllerTypeInfo);
                    if (resource.GroupName.IsNullOrEmpty())
                    {
                        resource.GroupName = descriptor.ControllerName;
                    }

                    var httpMethod = descriptor.EndpointMetadata.FirstOrDefault(a => a.GetType() == typeof(HttpMethodMetadata));
                    if (httpMethod != null && httpMethod is HttpMethodMetadata metadata)
                    {
                        resource.RequestMethod = metadata.HttpMethods.FirstOrDefault();
                    }

                    // 生成接口 API 唯一 Code
                    resource.Code = $"{resource.NameSpace}.{resource.ControllerName}.{resource.ActionName}";

                    await apiResourceService.InsertOrUpdateAsync(resource, a => a.Code == resource.Code);
                }

                watch.Stop();
                logger.LogInformation($"后台任务{nameof(InitApiResourceService)}执行完成，耗时:{watch.ElapsedMilliseconds}ms");
            }
        }
    }
}

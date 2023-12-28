using aehyok.Basic.Domains;
using aehyok.Basic.Dtos;
using aehyok.Basic.Dtos.Create;
using aehyok.Basic.Dtos.Query;
using aehyok.Basic.Services;
using aehyok.Core.Domains;
using aehyok.Core.Services;
using aehyok.EntityFramework.Repository;
using aehyok.Infrastructure;
using aehyok.Infrastructure.Enums;
using Ardalis.Specification;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;

namespace aehyok.Basic.Api.Controllers
{

    /// <summary>
    /// 菜单管理
    /// </summary>
    public class MenuController(IMenuService menuService,
        IServiceProvider services) : BasicControllerBase
    {

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="platformType"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("tree/{platformType}")]
        public async Task<List<MenuTreeDto>> GetTreeAsync(PlatformType platformType, [FromQuery] MenuTreeQueryModel model)
        {
            return await menuService.GetTreeListAsync(platformType, model);
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<long> PostAsync(CreateMenuModel model)
        {
            return await menuService.PostAsync(model);
        }

        /// <summary>
        /// 获取菜单详情
        /// </summary>
        /// <param name="id">菜单编号</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Task<MenuDto> GetByIdAsync(long id)
        {
            return menuService.GetByIdAsync<MenuDto>(id);
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="id">菜单Id</param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPut("{id}")]
        public async Task<StatusCodeResult> PutAsync(long id, CreateMenuModel model)
        {
            await menuService.PutAsync(id, model);

            return Ok();
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpDelete("{id}")]
        public async Task<StatusCodeResult> DeleteAsync(long id)
        {
            await menuService.DeleteAsync(id);

            return Ok();
        }

        /// <summary>
        /// 获取菜单绑定接口
        /// </summary>
        /// <param name="menuResourceService"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/Resources")]
        public async Task<List<MenuResource>> GetResourcesAsync([FromServices] IServiceBase<MenuResource> menuResourceService, long id)
        {
            using var scope = services.CreateScope();

            var apiResourceService = scope.ServiceProvider.GetRequiredService<IApiResrouceService>();

            var list = await apiResourceService.GetListAsync();

            return await menuResourceService.GetListAsync(a => a.MenuId == id);
        }



        /// <summary>
        /// api 资源同步
        /// </summary>
        /// <returns></returns>
        [HttpPost("set")]
        public async Task<StatusCodeResult> Set()
        {
            using var scope = services.CreateScope();
            var actionDescriptorProvider = scope.ServiceProvider.GetRequiredService<IActionDescriptorCollectionProvider>();
            var actions = actionDescriptorProvider.ActionDescriptors.Items;

            var assemblyName = Assembly.GetEntryAssembly()?.GetName().Name;
            var apiResourceService = scope.ServiceProvider.GetRequiredService<IApiResrouceService>();

            var mapper = scope.ServiceProvider.GetService<IMapper>();

            foreach (ControllerActionDescriptor descriptor in actions)
            {
                var resource = new ApiResource
                {
                    NameSpace = descriptor.ControllerTypeInfo.Namespace,
                    ActionName = descriptor.ActionName,
                    ControllerName = descriptor.ControllerName,
                    RoutePattern = descriptor.AttributeRouteInfo?.Template,
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
            return Ok();
        }
    }
}

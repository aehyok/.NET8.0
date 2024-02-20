using sun.Basic.Domains;
using sun.Basic.Dtos;
using sun.Basic.Dtos.Create;
using sun.Basic.Dtos.Query;
using sun.Basic.Services;
using sun.Core.Attributes;
using sun.Core.Domains;
using sun.Core.Dtos;
using sun.Core.Dtos.Create;
using sun.Core.Dtos.Query;
using sun.Core.Services;
using sun.EntityFrameworkCore.Repository;
using sun.Infrastructure.Enums;
using Ardalis.Specification;
using Microsoft.AspNetCore.Mvc;

namespace sun.Basic.Api.Controllers
{
    /// <summary>
    /// 菜单管理
    /// </summary>
    public class MenuController(IMenuService menuService,
        IApiResrouceCoreService apiResourceService, IServiceBase<MenuResource> menuResourceService) : BasicControllerBase
    {
        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="platformType">平台类型</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("tree/{platformType}")]
        [OperationLogAction("获取菜单列表: {model.ParentId}")]
        public async Task<List<MenuTreeDto>> GetTreeAsync(PlatformType platformType, [FromQuery] MenuTreeQueryDto model)
        {
            return await menuService.GetTreeListAsync(platformType, model);
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [OperationLogAction("新增菜单，菜单名称为:{model.Name}")]
        public async Task<long> PostAsync(CreateMenuDto model)
        {
            return await menuService.PostAsync(model);
        }

        /// <summary>
        /// 获取菜单详情
        /// </summary>
        /// <param name="id">菜单Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [OperationLogAction("获取菜单详情，菜单Id为:{id}")]
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
        [OperationLogAction("修改菜单，菜单Id为:{id}，菜单Code为{model.Code}")]
        public async Task<StatusCodeResult> PutAsync(long id, CreateMenuDto model)
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
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/Resources")]
        public async Task<List<MenuResourceDto>> GetResourcesAsync(long id)
        {
            var resources = await apiResourceService.GetListAsync<ApiResourceDto>();

            var checkedList = await menuResourceService.GetListAsync(a => a.MenuId == id);

            var array = checkedList.Select(item => item.ApiResourceId);

            resources.ForEach(item => {
                if(array.Contains(item.Id))
                {
                    item.IsChecked = true;
                }
            });


            return resources.GroupBy(a => new { a.NameSpace, a.ControllerName, a.GroupName }).OrderBy(a => a.Key.NameSpace).Select(a =>
            {
                var resource = new MenuResourceDto
                {
                    Name = a.Key.GroupName,
                    Code = $"{a.Key.NameSpace}.{a.Key.ControllerName}",
                    Operations = a.Select(c => this.Mapper.Map<MenuResourceDto>(c)).ToList()
                };

                return resource;
            }).ToList();
        }

        /// <summary>
        /// 绑定接口
        /// </summary>
        /// <param name="id">菜单Id</param>
        /// <param name="resources">接口资源id数组</param>
        /// <returns></returns>
        [HttpPut("{id}/bind")]
        public async Task<StatusCodeResult> BindResourceAsync(long id, long[] resources)
        {
            var existLists = await menuResourceService.GetListAsync(a => a.MenuId == id);
            await menuResourceService.DeleteAsync(existLists);

            var newResources = resources.Select(a => new MenuResource
            {
                MenuId = id,
                ApiResourceId = a
            });

            await menuResourceService.InsertAsync(newResources);

            return Ok();
        }
    }
}

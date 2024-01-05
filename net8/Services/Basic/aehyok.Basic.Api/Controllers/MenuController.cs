using aehyok.Basic.Domains;
using aehyok.Basic.Dtos;
using aehyok.Basic.Dtos.Create;
using aehyok.Basic.Dtos.Query;
using aehyok.Basic.Services;
using aehyok.Core.Domains;
using aehyok.Core.Services;
using aehyok.EntityFrameworkCore.Repository;
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
        IApiResourceService apiResourceService) : BasicControllerBase
    {

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="platformType">平台类型</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("tree/{platformType}")]
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
        /// <param name="menuResourceService"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/Resources")]
        public async Task<List<MenuResourceDto>> GetResourcesAsync([FromServices] IServiceBase<MenuResource> menuResourceService, long id)
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
        /// <param name="menuResourceService"></param>
        /// <param name="id">菜单Id</param>
        /// <param name="resources">接口资源id数组</param>
        /// <returns></returns>
        [HttpPut("{id}/bind")]
        public async Task<StatusCodeResult> BindResourceAsync([FromServices] IServiceBase<MenuResource> menuResourceService, long id, long[] resources)
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

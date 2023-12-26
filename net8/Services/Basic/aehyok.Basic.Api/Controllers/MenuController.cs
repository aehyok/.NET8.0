using aehyok.Basic.Domains;
using aehyok.Basic.Dtos;
using aehyok.Basic.Dtos.Create;
using aehyok.Basic.Dtos.Query;
using aehyok.Basic.Services;
using aehyok.EntityFramework.Repository;
using aehyok.Infrastructure.Enums;
using Ardalis.Specification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aehyok.Basic.Api.Controllers
{

    /// <summary>
    /// 菜单管理
    /// </summary>
    public class MenuController(IMenuService menuService) : BasicControllerBase
    {
        private readonly IMenuService menuService = menuService;

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <param name="platformType"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("tree/{platformType}")]
        public async Task<List<MenuTreeDto>> GetTreeAsync(PlatformType platformType, [FromQuery] MenuTreeQueryModel model)
        {
            return await this.menuService.GetTreeListAsync(platformType, model);
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<long> PostAsync(CreateMenuModel model)
        {
            return await this.menuService.PostAsync(model);
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
            await this.menuService.PutAsync(id, model);

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
            await this.menuService.DeleteAsync(id);

            return Ok();
        }
    }
}

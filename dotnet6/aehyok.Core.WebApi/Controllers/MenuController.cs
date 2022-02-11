using aehyok.Core.EntityFramework.MySql;
using aehyok.Core.EntityFramework.MySql.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aehyok.Core.WebApi.Controllers
{
    /// <summary>
    /// 菜单管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MenuController : BaseApiController
    {
        private readonly IRepository<SystemMenu> _menuRepository;
        public MenuController(IRepository<SystemMenu> menuRepository)
        {
            this._menuRepository = menuRepository;
        }

        /// <summary>
        /// 获取所有菜单列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public List<SystemMenu> GetMenuList()
        {
            var list = this._menuRepository.GetList();
            return list;
        }

        /// <summary>
        /// 通过菜单Id获取菜单详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<SystemMenu> GetMenu(string id)
        {
            return await this._menuRepository.GetAsync(id);
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<int> AddMenu(SystemMenu systemMenu)
        {
            return await this._menuRepository.InsertAsync(systemMenu);
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<int> UpdateMenu(SystemMenu systemMenu)
        {
            return await this._menuRepository.UpdateAsync(systemMenu);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<int> DeleteMenu(string menuId)
        {
            var list = await this._menuRepository.GetListAsync(item => item.FatherId == menuId);
            // 删除的-1代表无法删除
            return list == null || list.Count == 0 ? await this._menuRepository.DeleteAsync(menuId) : -1;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aehyok.Core.Data;
using aehyok.Core.Data.Model;
using aehyok.Core.IRepository;
using aehyok.Core.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aehyok.Core.Web.Controllers
{
    /// <summary>
    /// 菜单Api
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MenuApiController : ControllerBase
    {
        private readonly IMenuRepository _menuRepository;

        public MenuApiController(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetMenuList()
        {
            var result = new ResultModel
            {
                Code = "0",
                Msg = "ok"
            };

            var list = _menuRepository.GetMenuList();
            List<MenuModel> menuList = new List<MenuModel>();
            foreach (var item in list)
            {
                MenuModel model = new MenuModel
                {
                    Id = item.Id,
                    Name = item.MenuTitle,
                    Pid = item.FatherId,
                    Sex = "man"
                };
                menuList.Add(model);
            }
            result.Data = menuList;
            return Ok(result);
        }
    }
}
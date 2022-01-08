using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aehyok.Core.Data;
using aehyok.Core.Data.Model;
using aehyok.Core.IRepository;
using aehyok.Core.Repository;
using Microsoft.AspNetCore.Mvc;

namespace aehyok.Core.Web.Controllers
{
    /// <summary>
    /// 菜单控制器
    /// </summary>
    public class MenuController : Controller
    {
        private readonly IMenuRepository _menuRepository;

        public MenuController(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public dynamic GetMenuList()
        {
            ResultModel result = new ResultModel();
            result.Code = "0";
            result.Msg = "ok";

            var list = _menuRepository.GetMenuList();
            List<MenuModel> menuList= new List<MenuModel>();
            foreach (var item in list)
            {
                MenuModel model = new MenuModel();
                model.Id = item.Id;
                model.Name = item.MenuTitle;
                model.Pid = item.FatherId;
                model.Sex = "man";
                menuList.Add(model);
            }
            result.Data = new List<MenuModel>();
            result.Data = menuList;
            return Ok(result);
        }
    }
}
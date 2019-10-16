using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aehyok.Core.Model;
using aehyok.Core.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aehyok.Core.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MenuApiController : ControllerBase
    {
        private readonly IMenuRepository _menuRepository;

        public MenuApiController(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public dynamic GetMenuList()
        {

            ResultModel result = new ResultModel();
            result.Code = "0";
            result.Msg = "ok";

            var list = _menuRepository.GetMenuList();
            List<MenuModel> menuList = new List<MenuModel>();
            foreach (var item in list)
            {
                MenuModel model = new MenuModel();
                model.Id = item.Id;
                model.Name = item.MenuTitle;
                model.Pid = item.FatherId;
                model.Sex = "man";
                menuList.Add(model);
            }
            //result.Data = new List<MenuModel>();
            //result.Data = menuList;
            List<ResultModel> resultList = new List<ResultModel>();
            resultList.Add(result);
            return Ok(resultList);

            //return "Hello World";

            Dictionary<string, object> pairs = new Dictionary<string, object>();
            pairs.Add("1", "teemo");
            pairs.Add("2", "Jolinson");
            return pairs;
        }
    }
}
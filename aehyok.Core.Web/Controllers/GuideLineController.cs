using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace aehyok.Core.Web.Controllers
{

    /// <summary>
    /// 指标定义器
    /// </summary>
    public class GuideLineController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using aehyok.Core.Web.Models;
using iText.Kernel.Font;
using iText.IO.Font;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout;
using aehyok.Core.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace aehyok.Core.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAccountRepository _test;

        public HomeController(ILogger<HomeController> logger, IAccountRepository test)
        {
            _logger = logger;
            _test = test;
        }

        public IActionResult Index()
        {
            _logger.LogError("测试代码");
            var result=_test.CheckLogin("", "");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

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
using aehyok.Core.IRepository;
using Microsoft.Extensions.Caching.Memory;
using AutoMapper;
using aehyok.Core.AutoMapper;
using aehyok.Core.RabbitMQ;

namespace aehyok.Core.Web.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAccountRepository _test;
        private readonly IMemoryCache _memoryCache;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IAccountRepository test, IMemoryCache memoryCache, IMapper mapper)
        {
            _logger = logger;
            _test = test;
            _memoryCache = memoryCache;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            _logger.LogDebug("Debugger测试代码");
            _logger.LogError("测试代码");

            User user = new User();
            var userDTO = _mapper.Map<UserDTO>(user);
            //var value=_memoryCache.Get("key");

            //if(value==null)
            //{
            //    _memoryCache.Set("key", "test", new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(10)));
            //}

            //value=_memoryCache.Get("key");

            //value = _memoryCache.Get("key");
            var result=_test.CheckLogin("", "");

            RabbitDemo.Consumer();
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

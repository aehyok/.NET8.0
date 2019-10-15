using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace aehyok.Core.Web.Controllers
{
    public class TableController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TreeTable()
        {
            return View();
        }
    }
}
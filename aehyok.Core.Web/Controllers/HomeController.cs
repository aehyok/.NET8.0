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

namespace aehyok.Core.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            PdfFont font = PdfFontFactory.CreateFont(@"AdobeSongStd-Light.otf", PdfEncodings.IDENTITY_H, false);
            PdfWriter pdfWriter = new PdfWriter("hello.pdf");
            PdfDocument pdfDocument = new PdfDocument(pdfWriter);
            Document document = new Document(pdfDocument);
            document.Add(new Paragraph("你好!").SetFont(font));
            document.Close();
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

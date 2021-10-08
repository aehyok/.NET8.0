using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Mvc;

namespace aehyok.Core.Web.Controllers
{
    public class PDFController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public ActionResult ShowPdf()
        {
            return RedirectToAction("ShowPdfResult", "PDF", new { });
        }

        public FileContentResult ShowPdfResult()
        {
            FileStream fileStream = new FileStream("hello.pdf", FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] bytes = new byte[fileStream.Length];

            fileStream.Read(bytes, 0, bytes.Length);

            fileStream.Close();
            return File(bytes, "application/pdf", "hello.pdf");
        }
    }
}
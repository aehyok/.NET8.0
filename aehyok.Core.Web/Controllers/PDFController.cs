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
            PdfFont font = PdfFontFactory.CreateFont(@"AdobeSongStd-Light.otf", PdfEncodings.IDENTITY_H, false);
            PdfWriter pdfWriter = new PdfWriter("hello.pdf");
            PdfDocument pdfDocument = new PdfDocument(pdfWriter);
            Document document = new Document(pdfDocument);
            document.Add(new Paragraph("你好!").SetFont(font));
            document.Close();
            return View();
        }

        public FileContentResult ShowPdf()
        {
            FileStream fileStream = new FileStream("hello.pdf", FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] bytes = new byte[fileStream.Length];

            fileStream.Read(bytes, 0, bytes.Length);

            fileStream.Close();
            return File(bytes, "application/pdf", "hello.pdf");
        }
    }
}
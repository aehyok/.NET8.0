using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System;

namespace aehyok.Core.IText
{
    public class ITextHelper
    {
        public static void  CreatePdf()
        {
            PdfFont font = PdfFontFactory.CreateFont(@"Font/AdobeSongStd-Light.otf", PdfEncodings.IDENTITY_H, false);
            PdfWriter pdfWriter = new PdfWriter("hello.pdf");
            PdfDocument pdfDocument = new PdfDocument(pdfWriter);
            Document document = new Document(pdfDocument);
            document.Add(new Paragraph("你好!").SetFont(font));
            document.Close();
        }
    }
}

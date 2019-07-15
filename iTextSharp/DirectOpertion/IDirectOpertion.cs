using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTextSharp.DirectOpertion
{
    public interface IDirectOpertion
    {
        void Write(text.Document document, text.pdf.PdfWriter writer, PdfContentByte pdfContent);
    }
}

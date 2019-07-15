using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTextSharp.DirectOpertion
{
    public class DirectAddParagraph : IDirectOpertion
    {
        private Font _font = null;
        private string _paragraph = string.Empty;
        public DirectAddParagraph(string paragraph, Font font)
        {
            _font = font;
            _paragraph = paragraph;
        }

        public void Write(Document document, PdfWriter writer, PdfContentByte pdfContent)
        {
            Phrase header = new Phrase(_paragraph, _font);

            ColumnText.ShowTextAligned(pdfContent, Element.ALIGN_CENTER, header,
                       document.Right - ColumnText.GetWidth(header), document.Bottom - 5, 0);  // document.Top 为Page的margin.top
        }
    }
}

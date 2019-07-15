using iTextSharp.DirectOpertion;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace iTextSharp.DirectOpertion
{
    internal class DirectAddPageNumber : IDirectOpertion
    {
        private Font _font;

        public DirectAddPageNumber(Font font)
        {
            this._font = font;
        }

        public void Write(Document document, PdfWriter writer, PdfContentByte pdfContent)
        {

            Phrase header = new Phrase(document.PageNumber.ToString(), _font);

            float horizonCenter = (document.Right - ColumnText.GetWidth(header)) / 2;
            float verticalTop = document.Bottom;

            ColumnText.ShowTextAligned(pdfContent, Element.ALIGN_CENTER, header,
                      horizonCenter, verticalTop, 0);  // document.Top 为Page的margin.top
        }
    }
}
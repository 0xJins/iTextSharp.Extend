using iTextSharp.DirectOpertion;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace iTextSharp.DirectOpertion
{
    internal class DirectAddPageHeader : IDirectOpertion
    {
        private string _header;
        private Font _font;

        public DirectAddPageHeader(string header, Font font)
        {
            this._header = header;
            this._font = font;
        }

        public void Write(Document document, PdfWriter writer, PdfContentByte pdfContent)
        {
            Phrase header = new Phrase(_header, _font);

            float horizonCenter = (document.Right - ColumnText.GetWidth(header)) / 2 ;
            float verticalTop = document.Top;

            ColumnText.ShowTextAligned(pdfContent, Element.ALIGN_CENTER, header,
                      horizonCenter , verticalTop, 0);  // document.Top 为Page的margin.top
        }
    }
}
using iTextSharp.Constants;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTextSharp
{
    public class DefaultPageEvent : PdfPageEventHelper, IPdfPageEvent
    {
        private DirectContent _directContent = null;
        public DefaultPageEvent(DirectContent directContent)
        {
            _directContent = directContent;
        }
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);

            PdfContentByte pdfContent =
                _directContent.ContentLayer == ContentLayer.Content ?
                writer.DirectContent :
                writer.DirectContentUnder;

            foreach (var oper in _directContent._eachDirectOpertions)
            {
                oper.Write(document, writer, pdfContent);
            }
        }
    }
}

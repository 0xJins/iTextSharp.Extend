using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTextSharp.DirectOpertion
{
    public class DirectAddImage : IDirectOpertion
    {
        private Uri _imgUri = null;
        public DirectAddImage(Uri imgUri)
        {
            _imgUri = imgUri;
        }

        public void Write(Document document, PdfWriter writer, PdfContentByte pdfContent)
        {

        }
    }
}

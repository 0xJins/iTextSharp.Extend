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
        private float _absoluteX;
        private float _absoluteY;
        public DirectAddImage(Uri imgUri, float absoluteX,float absoluteY)
        {
            _imgUri = imgUri;
            _absoluteX = absoluteX;
            _absoluteY = absoluteY;
        }

        public void Write(Document document, PdfWriter writer, PdfContentByte pdfContent)
        {
            Image img = Image.GetInstance(_imgUri);
            img.SetAbsolutePosition(_absoluteX, _absoluteY);
            pdfContent.AddImage(img);
        }
    }
}

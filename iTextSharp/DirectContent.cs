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
    public class DirectContent
    {
        #region Fields
        private iTextSharp.Utility _utility = null;
        #endregion

        #region Internal Fields
        internal List<DirectOpertion> _eachDirectOpertions = null;
        #endregion

        #region Properties
        public ContentLayer ContentLayer { get; }
        #endregion

        #region Constructors

        public DirectContent(iTextSharp.Utility utility, ContentLayer layer)
        {
            _eachDirectOpertions = new List<DirectOpertion>();

            ContentLayer = layer;

            _utility = utility;
            _utility.OnSave += _utility_OnSave;
        }
        #endregion

        #region Public Methods

        public DirectContent EachPageAddParagraph()
        {
            _eachDirectOpertions.Add(new DirectAddParagraph(_utility.Font));
            return this;
        }

        public DirectContent EachPageBackground()
        {
            _eachDirectOpertions.Add(new DirectAddParagraph(_utility.Font));
            return this;
        }
        #endregion

        #region Event Handler

        private void _utility_OnSave(text.Document document, text.pdf.PdfWriter writer, string saveDir)
        {
            writer.PageEvent = new DefaultPageEvent(this);
        }
        #endregion
    }

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


    public interface DirectOpertion
    {
        void Write(text.Document document, text.pdf.PdfWriter writer, PdfContentByte pdfContent);
    }


    public class DirectAddParagraph : DirectOpertion
    {
        private Font _font = null;
        public DirectAddParagraph(Font font)
        {
            _font = font;
        }

        public void Write(Document document, PdfWriter writer, PdfContentByte pdfContent)
        {
            Phrase header = new Phrase("签字确认: ____________", _font);

            //页眉显示的位置 
            ColumnText.ShowTextAligned(pdfContent, Element.ALIGN_CENTER, header,
                       document.Right - ColumnText.GetWidth(header), document.Bottom - 5, 0);  // document.Top 为Page的margin.top
        }
    }

    public class DirectAddImage : DirectOpertion
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

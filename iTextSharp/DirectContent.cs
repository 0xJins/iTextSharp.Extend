using iTextSharp.Constants;
using iTextSharp.DirectOpertion;
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
        internal List<IDirectOpertion> _eachDirectOpertions = null;
        #endregion

        #region Properties
        public ContentLayer ContentLayer { get; }
        #endregion

        #region Constructors

        public DirectContent(iTextSharp.Utility utility, ContentLayer layer)
        {
            _eachDirectOpertions = new List<IDirectOpertion>();

            ContentLayer = layer;

            _utility = utility;
            _utility.OnSave += _utility_OnSave;
        }
        #endregion

        #region Public Methods

        public DirectContent EachPageAddParagraph(string paragraph)
        {
            _eachDirectOpertions.Add(new DirectAddParagraph(paragraph,_utility.Font));
            return this;
        }

        public DirectContent EachPageBackground(Uri imgUri,float absoluteX, float absoluteY)
        {
            _eachDirectOpertions.Add(new DirectAddImage(imgUri, absoluteX, absoluteY));
            return this;
        }

        public DirectContent EachPageHeader(string header)
        {
            _eachDirectOpertions.Add(new DirectAddPageHeader(header,_utility.Font));
            return this;
        }

        public DirectContent EachPageNumber()
        {
            _eachDirectOpertions.Add(new DirectAddPageNumber(_utility.Font));
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

    

    
}

using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTextSharp.TextSharpOperation
{

    public class AddAuthorTextSharpOperation : iTextSharpOperation
    {
        private string _author = string.Empty;
        public AddAuthorTextSharpOperation(string author)
        {
            _author = author;
        }
        public void Write(Document doc)
        {
            doc.AddAuthor(_author);
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;

namespace iTextSharp.TextSharpOperation
{
    public class AddSubjectTextSharpOperation : iTextSharpOperation
    {
        private string _author = string.Empty;
        public AddSubjectTextSharpOperation(string author)
        {
            _author = author;
        }
        public void Write(Document doc)
        {
            doc.AddAuthor(_author);
        }
    }
}

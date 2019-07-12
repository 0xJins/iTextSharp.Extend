using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;

namespace iTextSharp.TextSharpOperation
{
    public class AddKeywordsTextSharpOperation : iTextSharpOperation
    {
        private string _keywords = string.Empty;
        public AddKeywordsTextSharpOperation(string keywords)
        {
            _keywords = keywords;
        }
        public void Write(Document doc)
        {
            doc.AddKeywords(_keywords);
        }
    }
}

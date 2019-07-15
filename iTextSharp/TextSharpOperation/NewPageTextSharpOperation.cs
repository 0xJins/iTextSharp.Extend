using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;

namespace iTextSharp.TextSharpOperation
{
    public class NewPageTextSharpOperation : iTextSharpOperation
    {
        public void Write(Document doc)
        {
            doc.NewPage();
        }
    }
}

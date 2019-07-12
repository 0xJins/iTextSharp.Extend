using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;

namespace iTextSharp.TextSharpOperation
{
    public class AddTitleTextSharpOperation : iTextSharpOperation
    {
        private string _title;

        public AddTitleTextSharpOperation(string title)
        {
            this._title = title;
        }
        public void Write(Document doc)
        {
            doc.AddTitle(_title);
        }
    }
}

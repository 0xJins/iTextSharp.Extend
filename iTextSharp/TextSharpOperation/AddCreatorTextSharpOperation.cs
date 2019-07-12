using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;

namespace iTextSharp.TextSharpOperation
{
    public class AddCreatorTextSharpOperation : iTextSharpOperation
    {
        private string _creator;

        public AddCreatorTextSharpOperation(string creator)
        {
            _creator = creator;
        }
        public void Write(Document doc)
        {
            doc.AddCreator(_creator);
        }
    }
}

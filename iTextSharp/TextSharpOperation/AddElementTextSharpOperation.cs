using iTextSharp.text;
using iTextSharp.TextSharpOperation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTextSharp.TextSharpOperation
{

    public class AddElementTextSharpOperation : iTextSharpOperation
    {
        private IElement _ele = null;
        public AddElementTextSharpOperation(IElement ele)
        {
            _ele = ele;
        }
        public void Write(Document doc)
        {
            doc.Add(_ele);
        }
    }
}

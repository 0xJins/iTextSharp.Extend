using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.TextSharpOperation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTextSharp.TextSharpOperation
{

    public class AddDataTableTextSharpOperation : iTextSharpOperation
    {
        private DataTable _dataTable = null;
        private System.Drawing.Color _borderColor;
        private Font _font = null;
        public AddDataTableTextSharpOperation(DataTable dataTable, System.Drawing.Color borderColor, Font font)
        {
            _dataTable = dataTable;
            _borderColor = borderColor;
            _font = font;
        }
        public void Write(Document doc)
        {
            doc.Add(CreatePTabel(_dataTable, _borderColor));
        }

        #region Private Methods

        private PdfPTable CreatePTabel(DataTable table, System.Drawing.Color borderColor)
        {
            PdfPTable ptable = new PdfPTable(table.Columns.Count);

            // 写入标题
            for (int i = 0; i < table.Columns.Count; i++)
            {
                DataColumn col = table.Columns[i];
                ptable.AddCell(CreatePCell(col.ColumnName, borderColor));
            }

            // 写入数据
            foreach (DataRow row in table.Rows)
            {
                for (int colIndex = 0; colIndex < table.Columns.Count; colIndex++)
                {
                    string cell = row[colIndex]?.ToString() ?? "";
                    PdfPCell pcell = CreatePCell(cell, borderColor);
                    ptable.AddCell(pcell);
                }
            }
            return ptable;
        }

        private PdfPCell CreatePCell(string content, System.Drawing.Color borderColor)
        {
            PdfPCell cell = new PdfPCell(new Paragraph(content, _font));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.PaddingBottom = 10;
            cell.PaddingTop = 10;
            cell.BorderColor = new BaseColor(borderColor);
            cell.SetLeading(1.2f, 1.2f);
            return cell;
        }
        #endregion
    }
}

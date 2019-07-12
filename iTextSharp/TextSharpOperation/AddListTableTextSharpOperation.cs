using iTextSharp.Attributes;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace iTextSharp.TextSharpOperation
{
    public class AddListTableTextSharpOperation<T> : iTextSharpOperation
    {
        private IEnumerable<T> _obj = null;
        private System.Drawing.Color _borderColor;
        private Font _font;
        public AddListTableTextSharpOperation(IEnumerable<T> obj, System.Drawing.Color borderColor, Font font)
        {
            this._obj = obj;
            this._borderColor = borderColor;
            this._font = font;
        }

        public void Write(Document doc)
        {
            PdfPTable ptable = CreatePTabel(_obj, _borderColor);
            if(ptable != null)
            {
                doc.Add(ptable);
            }
        }

        #region Private Methods

        private PdfPTable CreatePTabel(IEnumerable<T> obj, System.Drawing.Color borderColor)
        {
            Dictionary<PropertyInfo, ColumnAttribute> props = GetPropertiesDict(typeof(T));

            float[] columnWidths = props.Where(p => p.Value != null && !p.Value.Ignore).Select(p => (float)p.Value.ColumnWidth).ToArray();
            PdfPTable ptable = new PdfPTable(columnWidths.Length);
            ptable.SetTotalWidth(columnWidths);
            ptable.LockedWidth = true;
            // 写入列头
            foreach (var prop in props)
            {
                if(prop.Value == null || prop.Value.Ignore)
                {
                    continue;
                }

                string columnName = prop.Value.ColumnName ?? prop.Key.Name;
                // 添加行头单元格
                ptable.AddCell(CreatePCell(columnName));
            }

            // 写入数据
            foreach (var row in obj)
            {
                foreach (var prop in props)
                {
                    if (prop.Value == null || prop.Value.Ignore)
                    {
                        continue;
                    }

                    string cell = prop.Key.GetValue(row)?.ToString() ?? "";
                    PdfPCell pcell = CreatePCell(cell);
                    ptable.AddCell(pcell);
                }
            }
            return ptable;
        }

        private PdfPCell CreatePCell(string content)
        {
            PdfPCell cell = new PdfPCell(new Paragraph(content, _font));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.PaddingBottom = 10;
            cell.PaddingTop = 10;
            cell.BorderColor = new BaseColor(_borderColor);
            cell.SetLeading(1.2f, 1.2f);
            return cell;
        }

        private Dictionary<PropertyInfo, ColumnAttribute> GetPropertiesDict(Type type)
        {
            Dictionary<PropertyInfo, ColumnAttribute> dict = new Dictionary<PropertyInfo, ColumnAttribute>();

            PropertyInfo[] props = type.GetProperties(
                BindingFlags.Public |
                BindingFlags.GetProperty |
                BindingFlags.Instance |
                BindingFlags.DeclaredOnly);

            for (int i = 0; i < props.Length; i++)
            {
                PropertyInfo prop = props[i];
                dict.Add(prop, prop.GetCustomAttribute<ColumnAttribute>());
            }

            return dict;
        }
        #endregion
    }
}

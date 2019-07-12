using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTextSharp.Attributes
{
    public class ColumnAttribute : Attribute
    {
        private const double DefaultColumnWidth = 80;
        public string ColumnName { get;  }
        public double ColumnWidth { get; }
        public bool Ignore { get; set; }
        public ColumnAttribute(bool ignore)
            : this("", DefaultColumnWidth, ignore)
        { }

        public ColumnAttribute(string columnName)
            :this(columnName, DefaultColumnWidth, false)
        { }

        public ColumnAttribute(string columnName, double columnWidth)
            : this(columnName, columnWidth, false)
        { }

        public ColumnAttribute(string columnName, double columnWidth, bool ignore)
        {
            this.ColumnName = columnName;
            this.ColumnWidth = columnWidth;
            this.Ignore = ignore;
        }
    }
}

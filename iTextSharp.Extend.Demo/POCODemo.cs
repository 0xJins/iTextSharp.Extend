using iTextSharp.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTextSharp.Extend.Demo
{
    public class POCODemo
    {
        [Column("序号", 200)]
        public string Index { get; set; }
        [Column("标题", 200)]
        public string Title { get; set; }
        [Column("时间", 200)]
        public DateTime Time { get; set; }

        [Column(ignore: true)]
        public string LeaveOut { get; set; }
    }
}

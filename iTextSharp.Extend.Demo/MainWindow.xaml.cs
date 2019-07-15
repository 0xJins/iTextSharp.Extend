using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace iTextSharp.Extend.Demo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnHelloWorld_OnClick(object sender, RoutedEventArgs e)
        {
            iTextSharp.Utility utility = new Utility();
            utility
                .SetPageSize(1000, 400)
                .SetPagePadding(20)
                .WriteParagraph("Hello world", Constants.Alignment.Center);
            utility.DirectContent.EachPageHeader("XXXXXXXXXXX");
            utility.DirectContent.EachPageAddParagraph("签字确认: ____________");
            utility.DirectContent.EachPageNumber();

            utility.NewPage().WriteParagraph("Hello world", Constants.Alignment.Center);
            utility.NewPage().WriteParagraph("Hello world", Constants.Alignment.Center);
            utility.NewPage().WriteParagraph("Hello world", Constants.Alignment.Center);
            utility.NewPage().WriteParagraph("Hello world", Constants.Alignment.Center);
            string dir = ShowFileDialog("HelloWorld.pdf");
            if(!string.IsNullOrEmpty(dir))
            {
                await utility.SaveAsync(dir);
            }
        }

        private async void btnListTable_OnClick(object sender, RoutedEventArgs e)
        {
            List<POCODemo> pocos = new List<POCODemo>();
            pocos.Add(new POCODemo() { Index = "1", Time = DateTime.Now, Title = "Title1" });
            pocos.Add(new POCODemo() { Index = "2", Time = DateTime.Now, Title = "Title2" });
            pocos.Add(new POCODemo() { Index = "3", Time = DateTime.Now, Title = "Title3" });

            iTextSharp.Utility utility = new Utility();
            utility
                .SetPageSize(1000, 400)
                .SetPagePadding(20)
                .WriteListTable<POCODemo>(pocos, Color.Red);

            string dir = ShowFileDialog("ListTable.pdf");
            if (!string.IsNullOrEmpty(dir))
            {
                await utility.SaveAsync(dir);
            }
        }

        private async void btnDataTable_OnClick(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("序号");
            dt.Columns.Add("时间");
            dt.Columns.Add("标题");
            dt.Rows.Add(new object[] { "1", DateTime.Now, Title = "Title 1" });
            dt.Rows.Add(new object[] { "2", DateTime.Now, Title = "Title 2" });
            dt.Rows.Add(new object[] { "3", DateTime.Now, Title = "Title 3" });
            iTextSharp.Utility utility = new Utility();
            utility
                .SetPageSize(1000, 400)
                .SetPagePadding(20)
                .WriteDataTable(dt, Color.Red);

            string dir = ShowFileDialog("DataTable.pdf");
            if (!string.IsNullOrEmpty(dir))
            {
                await utility.SaveAsync(dir);
            }
        }

        private string ShowFileDialog(string defaultFileName)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.InitialDirectory = Environment.CurrentDirectory;
            fileDialog.AddExtension = true;
            fileDialog.FileName = defaultFileName;
            fileDialog.Title = "保存";
            fileDialog.Filter = "PDF文件(*.pdf)|*.pdf";

            if (fileDialog.ShowDialog().GetValueOrDefault(false))
            {
                return fileDialog.FileName;
            }
            return "";
        }

    }
}

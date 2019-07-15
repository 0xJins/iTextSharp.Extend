using iTextSharp.Constants;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.TextSharpOperation;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace iTextSharp
{
    public class Utility
    {
        #region Constants
        /// <summary>
        /// 宋体
        /// </summary>
        private const string SIMSUN = "simsun.ttc";
        /// <summary>
        /// 微软雅黑
        /// </summary>
        private const string MSYH = "msyh.ttc";
        #endregion

        #region Fields

        private List<iTextSharpOperation> _docOperations = null;
        /// <summary>
        /// 页面大小
        /// </summary>
        private Rectangle _pageRect = null;

        #endregion

        #region Properties
        private Thickness _pagePadding;

        /// <summary>
        /// 页面边距
        /// </summary>
        public Thickness PagePadding
        {
            get { return _pagePadding; }
            set { _pagePadding = value; }
        }

        private DirectContent _directContent;

        public DirectContent DirectContent
        {
            get { return _directContent; }
            private set { _directContent = value; }
        }

        private DirectContent _directContentUnder;

        public DirectContent DirectContentUnder
        {
            get { return _directContentUnder; }
            private set { _directContentUnder = value; }
        }


        private Font _font;

        internal Font Font
        {
            get { return _font; }
            set { _font = value; }
        }

        #endregion

        #region Constructors

        public Utility()
        {
            _docOperations = new List<iTextSharpOperation>();
            DirectContent = new DirectContent(this, ContentLayer.Content);
            DirectContentUnder = new DirectContent(this, ContentLayer.ContentUnder);

            // 解决中文字体
            string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), MSYH);
            BaseFont baseFont = BaseFont.CreateFont($"{fontPath},0", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font defaultFont = new Font(baseFont, 14);
            _font = defaultFont;

            SetPageSize("A4");
            SetPagePadding(0);
        }


        #endregion

        #region SetPageSize

        public Utility SetPageSize(float width, float height)
        {
            return SetPageSize(0f, 0f, width, height);
        }

        public Utility SetPageSize(float left, float top, float width, float height)
        {
            _pageRect = new Rectangle(left, top, width, height);
            return this;
        }

        public Utility SetPageSize(string pageSizeName)
        {
            _pageRect = PageSize.GetRectangle(pageSizeName);
            return this;
        }
        #endregion

        #region SetPagePadding
        public Utility SetPagePadding(float uniformsThickness)
        {
            _pagePadding = new Thickness(uniformsThickness);
            return this;
        }

        public Utility SetPagePadding(float left, float top, float right, float bottom)
        {
            _pagePadding = new Thickness(left, top, right, bottom);
            return this;
        }

        #endregion

        #region WriteDataTable

        /// <summary>
        /// 写入DataTable
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public Utility WriteDataTable(
            DataTable table,
            System.Drawing.Color borderColor)
        {
            if (table == null)
            {
                throw new ArgumentNullException("table");
            }
            this._docOperations.Add(new AddDataTableTextSharpOperation(table, borderColor, this._font));
            return this;
        }

        #endregion

        #region WriteListTable

        /// <summary>
        /// 解析List转换成表格
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="borderColor"></param>
        /// <returns></returns>
        public Utility WriteListTable<T>(IList<T> list, System.Drawing.Color borderColor)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
            this._docOperations.Add(new AddListTableTextSharpOperation<T>(list, borderColor, this._font));
            return this;
        }
        #endregion

        #region WriteParagraph

        /// <summary>
        /// 写入段落
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        public Utility WriteParagraph(string paragraph)
        {
            return WriteParagraph(paragraph, Alignment.Left, _font.Size);
        }

        public Utility WriteParagraph(string paragraph, Alignment align)
        {
            return WriteParagraph(paragraph, align, _font.Size);
        }

        public Utility WriteParagraph(string paragraph, Alignment align, float fontsize)
        {
            Font font = new Font(_font);
            font.Size = fontsize;
            Paragraph para = new Paragraph(paragraph, font);
            if (align == Alignment.Right)
            {
                para.Alignment = Element.ALIGN_RIGHT;
            }
            else if (align == Alignment.Center)
            {
                para.Alignment = Element.ALIGN_CENTER;
            }
            else
            {
                para.Alignment = Element.ALIGN_LEFT;
            }

            this._docOperations.Add(new AddElementTextSharpOperation(para));
            return this;
        }
        #endregion

        #region AddAuthor


        /// <summary>
        /// 写入作者
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        public Utility AddAuthor(string author)
        {
            this._docOperations.Add(new AddAuthorTextSharpOperation(author));
            return this;
        }
        #endregion

        #region AddTitle

        /// <summary>
        /// 添加标题
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        public Utility AddTitle(string title)
        {
            this._docOperations.Add(new AddTitleTextSharpOperation(title));
            return this;
        }
        #endregion

        #region AddCreator
        public Utility AddCreator(string creator)
        {

            this._docOperations.Add(new AddCreatorTextSharpOperation(creator));
            return this;
        }
        #endregion

        #region AddKeywords
        public Utility AddKeywords(string keywords)
        {

            this._docOperations.Add(new AddKeywordsTextSharpOperation(keywords));
            return this;
        }
        #endregion

        #region AddSubject
        public Utility AddSubject(string subject)
        {
            this._docOperations.Add(new AddSubjectTextSharpOperation(subject));
            return this;
        }
        #endregion

        public Utility NewPage()
        {
            this._docOperations.Add(new NewPageTextSharpOperation());
            return this;
        }
        #region Save

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="saveDir"></param>
        public void Save(string saveDir)
        {
            if (string.IsNullOrEmpty(saveDir))
            {
                throw new ArgumentNullException("saveDir");
            }

            using (Document document = new Document(_pageRect, (float)_pagePadding.Left, (float)_pagePadding.Right, (float)_pagePadding.Top, (float)_pagePadding.Bottom))
            {
                using (PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(saveDir, FileMode.Create)))
                {
                    document.Open();

                    OnSave?.Invoke(document, writer, saveDir);
                    foreach (var ele in _docOperations)
                    {
                        ele.Write(document);
                    }
                    document.Close();
                }
            }
        }

        internal event Action<Document, PdfWriter, string> OnSave;

        /// <summary>
        /// 异步保存
        /// </summary>
        /// <param name="saveDir"></param>
        /// <returns></returns>
        public async Task SaveAsync(string saveDir)
        {
            await Task.Run(() => Save(saveDir));
        }
        #endregion
    }

}

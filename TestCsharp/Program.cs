using iTextSharp.text;
using iTextSharp.text.pdf;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Drawing.Chart;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web.Script.Serialization;
using System.Drawing;
using OfficeOpenXml.Style.XmlAccess;

namespace TestCsharp
{
    class Program
    {
        static void Main1(string[] args)
        {
            //while (true)
            //{
            //    decimal p = 123456.123000M; 
            //    string str = p.ToString("N2");//第一种 
            //    Console.WriteLine(str);
            //    string str1 = p.ToString("C");//第二种 
            //    Console.WriteLine(str1);
            //    string str2 = p.ToString("#,###.00;-#,##0.00;Zero");//第三种 
            //    Console.WriteLine(str2);

            //    string str3 = p.ToString("#,###.00;-#,##0.00;-");//第四种 
            //    Console.WriteLine(str3);

            //    string str4 = Console.ReadLine();
            //    //Console.WriteLine(string.Format("{0:C}", str));
            //    //Console.WriteLine(decimal.Parse(str).ToString("###,###,###") + "---------" + decimal.Parse(str).ToString("###,###,###.00"));
            //}

            //Document:（文档）生成pdf必备的一个对象,生成一个Document示例
            Document document = new Document(PageSize.A4, 30, 30, 5, 5);
            //为该Document创建一个Writer实例： 
            string strPdfFileName = System.AppDomain.CurrentDomain.BaseDirectory + "PDFOutput" + System.DateTime.Now.ToString("yyyyMMddhhmmssff") + ".pdf";
            //string strDocFileName = System.AppDomain.CurrentDomain.BaseDirectory   "WordOutput\\"   "测试PDF文件_"   System.DateTime.Now.ToString("yyyyMMddhhmmssff")   ".doc";
            iTextSharp.text.pdf.PdfWriter.GetInstance(document, new FileStream(strPdfFileName, FileMode.Create));
            //iTextSharp.text.rtf.RtfWriter.getInstance(document, new FileStream(strDocFileName, FileMode.Create));
            //打开当前Document
            document.Open();

            //为当前Document添加内容：
            document.Add(new Paragraph("Hello World"));
            //另起一行。有几种办法建立一个段落，如： 
            Paragraph p1 = new Paragraph(new Chunk("This is my first paragraph.\n", FontFactory.GetFont(FontFactory.HELVETICA, 12)));
            Paragraph p2 = new Paragraph(new Phrase("This is my second paragraph.", FontFactory.GetFont(FontFactory.HELVETICA, 12)));
            Paragraph p3 = new Paragraph("This is my third paragraph.", FontFactory.GetFont(FontFactory.HELVETICA, 12));
            //所有有些对象将被添加到段落中：
            p1.Add("you can add string here\n\t");
            p1.Add(new Chunk("you can add chunks \n")); p1.Add(new Phrase("or you can add phrases.\n"));
            document.Add(p1); document.Add(p2); document.Add(p3);

            //创建了一个内容为“hello World”、红色、斜体、COURIER字体、尺寸20的一个块： 
            Chunk chunk = new Chunk("Hello world", FontFactory.GetFont(FontFactory.COURIER, 20, iTextSharp.text.Font.NORMAL, new iTextSharp.text.BaseColor(255, 0, 0)));
            document.Add(chunk);
            //如果你希望一些块有下划线或删除线，你可以通过改变字体风格简单做到： 
            Chunk chunk1 = new Chunk("This text is underlined", FontFactory.GetFont(FontFactory.HELVETICA, 12, iTextSharp.text.Font.UNDEFINED));
            Chunk chunk2 = new Chunk("This font is of type ITALIC | STRIKETHRU", FontFactory.GetFont(FontFactory.HELVETICA, 12, iTextSharp.text.Font.ITALIC | iTextSharp.text.Font.STRIKETHRU));
            //改变块的背景
            chunk2.SetBackground(new iTextSharp.text.BaseColor(0xFF, 0xFF, 0x00));
            //上标/下标
            chunk1.SetTextRise(5);
            document.Add(chunk1);
            document.Add(chunk2);

            //外部链接示例： 
            //BaseFont bfSun1 = BaseFont.createFont(@"C:\WINDOWS\Fonts\SIMHEI.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            //iTextSharp.text.Font font1 = new iTextSharp.text.Font(bfSun1, 16);
            //string text = "iTextSharp网站";
            Anchor anchor = new Anchor("iTextSharp网站", FontFactory.GetFont(@"C:\WINDOWS\Fonts\SIMHEI.TTF", 12, iTextSharp.text.Font.UNDEFINED, new iTextSharp.text.BaseColor(0, 0, 255)));
            anchor.Reference = "http://itextsharp.sourceforge.net/";
            anchor.Name = "website";
            //内部链接示例：
            //BaseFont bfSun = BaseFont.createFont(@"c:\winnt\fonts\SIMSUN.TTC,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            BaseFont bfSun = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\SIMSUN.TTC,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(bfSun, 16);
            string text = "这是字体集合中的新宋体测试和一个内部链接！\n\n";
            //Anchor anchor1 = new Anchor(new Paragraph(text, font));
            //anchor1.Name = "link1";
            //Anchor anchor2 = new Anchor("点击将跳转到内部链接\n\f");
            //anchor2.Reference = "#link1";
            //document.Add(anchor); document.Add(anchor1); document.Add(anchor2);
            BaseFont bfHei = BaseFont.CreateFont(@"c:\WINDOWS\fonts\SIMHEI.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            font = new iTextSharp.text.Font(bfHei, 32);
            text = "这是黑体字测试！";
            document.Add(new Paragraph(text, font));

            //TextWordPDF wordpdf = new TextWordPDF();
            //string sFontName = wordpdf.GetFontName(TextWordPDF.FontName.黑体);
            //bfSun = BaseFont.createFont(sFontName, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            //font = new iTextSharp.text.Font(bfSun, 16);
            //text = "这是字体集合中的新宋体测试！";
            //document.Add(new Paragraph(text, font));

            //排序列表示例： 
            List list = new List(true, 20);
            list.Add(new iTextSharp.text.ListItem("First line"));
            list.Add(new iTextSharp.text.ListItem("The second line is longer to see what happens once the end of the line is reached. Will it start on a new line?"));
            document.Add(list);

            //文本注释： 
            Annotation a = new Annotation("authors", "Maybe its because I wanted to be an author myself that I wrote iText.");
            document.Add(a);

            ////包含页码没有任何边框的页脚。 
            //HeaderFooter footer = new HeaderFooter(new Phrase("This is page: "), true);
            //footer.Border = iTextSharp.text.Rectangle.NO_BORDER;
            //document.footer = footer;


            //Chapter对象和Section对象自动构建一个树：
            iTextSharp.text.Font f1 = new iTextSharp.text.Font();
            f1.SetStyle(iTextSharp.text.Font.BOLD.ToString());
            Paragraph cTitle = new Paragraph("This is chapter 1", f1);
            Chapter chapter = new Chapter(cTitle, 1);
            Paragraph sTitle = new Paragraph("This is section 1 in chapter 1", f1);
            Section section = chapter.AddSection(sTitle, 1);
            document.Add(chapter);

            ////构建了一个简单的表： 
            //iTextSharp.text.pdf.PdfPTable aTable = new PdfPTable(4);
            //aTable.AutoFillEmptyCells = true;
            //aTable.addCell("2.2", new Point(2, 2));
            //aTable.addCell("3.3", new Point(3, 3));
            //aTable.addCell("2.1", new Point(2, 1));
            //aTable.addCell("1.3", new Point(1, 3));
            //document.Add(aTable);
            ////构建了一个不简单的表：
            //iTextSharp.text.Table table = new iTextSharp.text.Table(3);
            //table.BorderWidth = 1;
            //table.BorderColor = new iTextSharp.text.Color(0, 0, 255);
            //table.Cellpadding = 5;
            //table.Cellspacing = 5;
            //Cell cell = new Cell("header");
            //cell.Header = true;
            //cell.Colspan = 3;
            //table.addCell(cell);
            //cell = new Cell("example cell with colspan 1 and rowspan 2");
            //cell.Rowspan = 2;
            //cell.BorderColor = new iTextSharp.text.Color(255, 0, 0);
            //table.addCell(cell);
            //table.addCell("1.1");
            //table.addCell("2.1");
            //table.addCell("1.2");
            //table.addCell("2.2");
            //table.addCell("cell test1");
            //cell = new Cell("big cell");
            //cell.Rowspan = 2;
            //cell.Colspan = 2;
            //cell.BackgroundColor = new iTextSharp.text.Color(0xC0, 0xC0, 0xC0);
            //table.addCell(cell);
            //table.addCell("cell test2");
            //// 改变了单元格“big cell”的对齐方式： 
            //cell.HorizontalAlignment = Element.ALIGN_CENTER;
            //cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            //document.Add(table);

            //Graphic g = new Graphic();
            //g.setBorder(3f, 5f);
            //document.Add(g);
            //document.Add(new Paragraph("Hello World"));
            //document.Add(new Paragraph("Hello World\n\n"));
            //g = new Graphic();
            //g.setHorizontalLine(5f, 100f);
            //document.Add(g);
            //document.Add(new Paragraph("Hello World"));
            //document.Add(new Paragraph("Hello World\n\n"));
            //g = new Graphic();
            //g.setHorizontalLine(2f, 80f, new iTextSharp.text.Color(0xFF, 0x00, 0x00));
            //document.Add(g);
            //g = new Graphic();
            //g.setHorizontalLine(2f, 80f,new iTextSharp.text.Color(System.Drawing.Color.Black));
            //document.Add(g);

            //关闭Document
            document.Close();


        }
        //static void Main(string[] args)
        //{
        //    //Console.WriteLine(DateTime.Now.ToString("MM", DateTimeFormatInfo.InvariantInfo));
        //    //Console.WriteLine(DateTime.Now.ToString("MMM", new System.Globalization.CultureInfo("en-us")));

        //    for (int i = 'a'; i < 'z'; i++)
        //        Console.WriteLine(Encoding.ASCII.GetString(new byte[]{(byte)i}));
        //        Console.ReadKey();
        //}

        static void Main(string[] args)
        {
            //TestNpoi();
            ExcelPackage ex = new ExcelPackage(new FileInfo("Epplus.xlsx"));
            ExcelWorkbook wb= ex.Workbook;
            ExcelWorksheet ws = wb.Worksheets["TestWorkSheet"];
            //配置文件属性  
            wb.Properties.Category = "类别";
            wb.Properties.Author = "作者";
            wb.Properties.Comments = "备注";
            wb.Properties.Company = "公司";
            wb.Properties.Keywords = "关键字";
            wb.Properties.Manager = "管理者";
            wb.Properties.Status = "内容状态";
            wb.Properties.Subject = "主题";
            wb.Properties.Title = "标题";
            wb.Properties.LastModifiedBy = "最后一次保存者";  

            //写工作表
            ws.Cells[1, 1].Value = "Hello";
            //ws.Cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
            //ws.Cells.Style.Fill.BackgroundColor.Indexed =NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
            ws.Cells["B1"].Value = "World";
            ws.Cells[3, 3, 3, 5].Merge = true;
            ws.Cells[3, 3].Value = "Cells[3, 3, 3, 5]合并";
            ws.Cells["A4:D5"].Merge = true;
            ws.Cells["A4"].Value = "Cells[\"A4:D5\"]合并";
            ex.Save();
        }

        private static void TestNpoi()
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFSheet sheet = workbook.CreateSheet("Sheet1") as HSSFSheet;
            HSSFRow dataRow = sheet.CreateRow(1) as HSSFRow;
            dataRow = sheet.CreateRow(1) as HSSFRow;
            CellRangeAddress region = new CellRangeAddress(1, 2, 1, 2);
            sheet.AddMergedRegion(region);
            ICell cell = dataRow.CreateCell(1);
            cell.SetCellValue("test");

            ICellStyle style = workbook.CreateCellStyle();
            style.BorderBottom = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;
            style.BorderTop = BorderStyle.Thin;
            style.BottomBorderColor = HSSFColor.Black.Index;
            style.LeftBorderColor = HSSFColor.Black.Index;
            style.RightBorderColor = HSSFColor.Black.Index;
            style.TopBorderColor = HSSFColor.Black.Index;

            //cell.CellStyle = style;  
            for (int i = region.FirstRow; i <= region.LastRow; i++)
            {
                IRow row = HSSFCellUtil.GetRow(i, sheet);
                for (int j = region.FirstColumn; j <= region.LastColumn; j++)
                {
                    ICell singleCell = HSSFCellUtil.GetCell(row, (short)j);
                    singleCell.CellStyle = style;
                }
            }

            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                using (FileStream fs = new FileStream("E:\\TestConsole.xls", FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }


        public string GetResponse<T>(string requestUrl, string requestMethod, T inPutModel)
        {
            string retString = null;
            var postDataStr = new JavaScriptSerializer().Serialize(inPutModel);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
            request.Method = requestMethod;
            request.ContentType = "application/json";
            request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            //request.Headers.Add("Token", "123");//添加验证
            Stream myRequestStream = request.GetRequestStream();
            //StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("utf-8"));
            //myStreamWriter.Write(postDataStr);
            //myStreamWriter.Close();

            byte[] byteArray = Encoding.UTF8.GetBytes(postDataStr);
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            if (myResponseStream != null)
            {
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                retString = myStreamReader.ReadToEnd();
                // ar retObj = new JavaScriptSerializer().Deserialize<AccountResultModel>(retString.Remove(retString.Length - 1, 1).Remove(0, 1).Replace("\\", ""));
                myStreamReader.Close();
            }
            if (myResponseStream != null) myResponseStream.Close();
            return retString;
        }

    }


    public static class ListExt
    {

        public static IEnumerable<T> ForeachOne<T>(this IEnumerable<T> eles, Action<T> act)
        {
            if (act != null)
                foreach (T e in eles)
                {
                    act(e);
                }
            return eles;
        }

        ////根据model将Table转成list
        public static IEnumerable<T> TabeToList<T>(this DataTable dt) where T : class,new()
        {
            List<T> lt = new List<T>();
            if (dt == null || dt.Rows.Count == 0) { return lt; }
            int i = 0;
            PropertyInfo[] pros = typeof(T).GetProperties(BindingFlags.Public);
            //StringBuilder sb = new StringBuilder();
            //pros.ForeachOne<PropertyInfo>(e =>
            //{
            //    sb.Append(e.Name.ToLower()).Append(",");
            //});
            //if (sb.Length > 1)
            //    sb.Remove(sb.Length - 2, 1);
            //string sourceProstr = sb.ToString();
            //List<string> usedItem = new List<string>();
            //
            //for (; i < dt.Rows[0].ItemArray.Length; i++)//取出对象属性用到的列
            //{
            //    if (sourceProstr.Contains(dt.Columns[i].ColumnName.ToLower())) { usedItem.Add(dt.Columns[i].ColumnName); }
            //}
            //if (usedItem.Count == 0) { return lt; }
            for (i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                T o = new T();
                pros = o.GetType().GetProperties(BindingFlags.Public);
                for (int j = 0; j < pros.Length; j++)
                {
                    //dt.Columns.AsQueryable() .Where(e => e.ToLower() == pros[i].Name.ToLower());
                }
            }
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    DataRow dr = dt.Rows[i];
            //    //T t = default(T);//获得当前对象的默认值
            //    T s = new T();
            //    for (int j = 0; j < dr.ItemArray.Length; j++)
            //    {
            //        PropertyInfo[] pis = s.GetType().GetProperties();
            //        pis.Where(e => e.Name.ToLower().Equals(dt.Columns[j].ColumnName.ToLower()))
            //           .ForeachOne(e =>
            //           {
            //               object ores = null;
            //               if (dr[j].Equals(DBNull.Value))
            //               {
            //                   switch (e.PropertyType.Name)//如果数据库是DBNull类型数值就0，引用就null,下面可能没有考虑完全，可以自己在添加上
            //                   {
            //                       case "int":
            //                       case "Int16":
            //                       case "Int32":
            //                       case "Int64":
            //                       case "float":
            //                       case "double": ores = 0; break;
            //                       default: ores = default(object); break;
            //                   }
            //               }
            //               else
            //               {
            //                   ores = dr[j];
            //               }
            //               e.SetValue(s, ores, null);
            //               //e.SetValue();
            //           });
            //    }
            //    lt.Add(s);
            //}
            return lt;
        }

        ////根据model将Table转成list
        //public static IEnumerable<T> TabeToList<T>(this DataTable dt) where T : class,new()
        //{
        //    if (dt == null) { return null; }

        //    List<T> lt = new List<T>();
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        DataRow dr = dt.Rows[i];
        //        //T t = default(T);//获得当前对象的默认值
        //        T s = new T();
        //        for (int j = 0; j < dr.ItemArray.Length; j++)
        //        {
        //            PropertyInfo[] pis = s.GetType().GetProperties();
        //            pis.Where(e => e.Name.ToLower().Equals(dt.Columns[j].ColumnName.ToLower()))
        //               .ForeachOne(e =>
        //               {
        //                   object ores = null;
        //                   if (dr[j].Equals(DBNull.Value))
        //                   {
        //                       switch (e.PropertyType.Name)//如果数据库是DBNull类型数值就0，引用就null,下面可能没有考虑完全，可以自己在添加上
        //                       {
        //                           case "int":
        //                           case "Int16":
        //                           case "Int32":
        //                           case "Int64":
        //                           case "float":
        //                           case "double": ores = 0; break;
        //                           default: ores = default(object); break;
        //                       }
        //                   }
        //                   else
        //                   {
        //                       ores = dr[j];
        //                   }
        //                   e.SetValue(s, ores, null);
        //                   //e.SetValue();
        //               });
        //        }
        //        lt.Add(s);
        //    }
        //    return lt;
        //}

        ////根据model将Table转成list
        //public static IEnumerable<T> TabeToList<T>(this DataTable dt) where T : class,new()
        //{
        //    if (dt != null)
        //    {
        //        List<T> lt = new List<T>();

        //        //PropertyInfo[] pis = s.GetType().GetProperties();
        //        PropertyInfo[] pros = typeof(T).GetProperties(BindingFlags.Public);
        //        Dictionary<string, string> dict = new Dictionary<string, string>();
        //        pros.ForeachOne(e =>
        //        {
        //            dict.Add(e.Name.ToLower(), e.PropertyType.Name);
        //        });
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            DataRow dr = dt.Rows[i];
        //            T s = new T();
        //            PropertyInfo[] sPro=s.GetType().GetProperties(BindingFlags.Public);
        //            foreach (string key in dict.Keys)
        //            {
        //                if(dr[key]!=null)sPro.
        //            }
        //            for (int j = 0; j < dr.ItemArray.Length; j++)
        //            {
        //                PropertyInfo[] pis = s.GetType().GetProperties();
        //                pis.Where(e => e.Name.ToLower().Equals(dt.Columns[j].ColumnName.ToLower()))
        //                   .ForeachOne(e =>
        //                   {
        //                       object ores = null;
        //                       if (dr[j].Equals(DBNull.Value))
        //                       {
        //                           switch (e.PropertyType.Name)//如果数据库是DBNull类型数值就0，引用就null,下面可能没有考虑完全，可以自己在添加上
        //                           {
        //                               case "int":
        //                               case "Int16":
        //                               case "Int32":
        //                               case "Int64":
        //                               case "float":
        //                               case "double": ores = 0; break;
        //                               default: ores = default(object); break;
        //                           }
        //                       }
        //                       else
        //                       {
        //                           ores = dr[j];
        //                       }
        //                       e.SetValue(s, ores, null);
        //                       //e.SetValue();
        //                   });
        //            }
        //            lt.Add(s);
        //        }
        //        return lt;
        //    }
        //    return null;
        //}

    }

}

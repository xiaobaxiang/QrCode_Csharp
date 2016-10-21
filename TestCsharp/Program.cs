using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

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
            string strPdfFileName = System.AppDomain.CurrentDomain.BaseDirectory+"PDFOutput"+ System.DateTime.Now.ToString("yyyyMMddhhmmssff")+".pdf";
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
        static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Now.ToString("MM", DateTimeFormatInfo.InvariantInfo));
            Console.WriteLine(DateTime.Now.ToString("MMM", new System.Globalization.CultureInfo("en-us")));
            Console.ReadKey();
        }
    }
}

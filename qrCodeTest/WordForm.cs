using Spire.Doc;
using Spire.Doc.Documents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace qrCodeTest
{
    public partial class WordForm : Form
    {
        public WordForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!File.Exists("Spird.docx")) File.Create("Spird.docx");
            Spire.Doc.Document doc = new Spire.Doc.Document("Spird.docx");
            Section section = doc.AddSection();
            Paragraph paragraph = section.AddParagraph();
            paragraph.AppendText("Hello world！Spire.doc测试生成");
            doc.SaveToFile("Spird.docx");
            doc.Dispose();
            MessageBox.Show("生成成功!");
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}

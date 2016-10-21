using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Gma.QrCodeNet.Encoding;
using System.Drawing.Drawing2D;
using System.IO;

namespace qrCodeTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void generate_Click(object sender, EventArgs e)
        {
            ////if (this.content.Text.Trim() == string.Empty)
            ////{
            ////    MessageBox.Show("Data must not be empty.");
            ////}
            ////else
            ////{
            ////    QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            ////    switch (this.cboEncoding.Text)
            ////    {
            ////        case "Byte":
            ////            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            ////            break;

            ////        case "AlphaNumeric":
            ////            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
            ////            break;

            ////        case "Numeric":
            ////            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
            ////            break;

            ////        default:
            ////            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            ////            break;
            ////    }
            ////    try
            ////    {
            ////        qrCodeEncoder.QRCodeScale = 0;
            ////    }
            ////    catch (Exception)
            ////    {
            ////        MessageBox.Show("Invalid size!");
            ////        return;
            ////    }
            ////    try
            ////    {
            ////        //int version = Convert.ToInt16(this.cboVersion.Text);
            ////        //qrCodeEncoder.QRCodeVersion = version;
            ////        qrCodeEncoder.QRCodeVersion = 0;
            ////    }
            ////    catch (Exception)
            ////    {
            ////        MessageBox.Show("Invalid version !");
            ////    }
            ////    //string errorCorrect = this.cboCorrectionLevel.Text;
            ////    //if (errorCorrect == "L")
            ////    //{
            ////    //    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            ////    //}
            ////    //else if (errorCorrect == "M")
            ////    //{
            ////    //    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            ////    //}
            ////    //else if (errorCorrect == "Q")
            ////    //{
            ////    //    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
            ////    //}
            ////    //else if (errorCorrect == "H")
            ////    //{
            ////    //    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
            ////    //}
            ////    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            ////    string data = this.content.Text;
            ////    Image image = qrCodeEncoder.Encode(data);
            ////    this.generate.Image = image;
            ////}

            //QrCodeImgControl qci = new QrCodeImgControl();
            //qci.Text = this.content.Text.Trim();
            //qci.SizeMode = PictureBoxSizeMode.StretchImage;
            ////qci.Height = 800;
            ////qci.Width = 800;
            //this.picQy.Image = qci.Image;





            this.picQy.Image = CreateQr(this.content.Text.Trim());

        }

        public Bitmap CreateQr(string context)
        {
            QrEncoder qrEncoder = new QrEncoder();
            //String data = "0150204whxtl/?type=invate";
            String data = context;
            //String data = context.Request.QueryString["qrdata"].ToString();
            System.Drawing.Bitmap image;
            QrCode qrCode = qrEncoder.Encode(data);//根据传进去的data返回最终生成的二维矩阵
            int wh = 5;
            image = new Bitmap(qrCode.Matrix.Width * wh, qrCode.Matrix.Height * wh);//二维码的长宽和版本有关17+n*4  ,版本与内容大小有关系
            Graphics gdiobj = Graphics.FromImage(image);
            gdiobj.CompositingQuality = CompositingQuality.HighQuality;
            gdiobj.SmoothingMode = SmoothingMode.HighQuality;
            gdiobj.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gdiobj.PixelOffsetMode = PixelOffsetMode.HighQuality;

            for (Int32 i = 0; i < qrCode.Matrix.Height; i++)
            {
                for (Int32 j = 0; j < qrCode.Matrix.Width; j++)
                {
                    if (qrCode.Matrix.InternalArray[i, j])
                    {
                        gdiobj.FillRectangle(new SolidBrush(Color.Black), i * wh, j * wh, wh, wh);
                    }
                }
            }

            //System.Drawing.Imaging.EncoderParameters ep = new System.Drawing.Imaging.EncoderParameters(1);
            //ep.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 800L);

            //System.IO.MemoryStream MStream = new System.IO.MemoryStream();
            //image.Save(MStream, System.Drawing.Imaging.ImageFormat.Png);
            // image.Save(MStream, null, ep);
            //context.Response.ClearContent();
            //context.Response.ContentType = "image/Png";
            //context.Response.BinaryWrite(MStream.ToArray());
            image.Save("test.bmp");//先保存到bin文件夹下面
            return image;
        }
    }
}

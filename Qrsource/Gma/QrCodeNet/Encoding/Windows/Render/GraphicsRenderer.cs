namespace Gma.QrCodeNet.Encoding.Windows.Render
{
    using Gma.QrCodeNet.Encoding;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Windows.Forms;

    public class GraphicsRenderer
    {
        private Brush m_DarkBrush;
        private ISizeCalculation m_iSize;
        private Brush m_LightBrush;

        public GraphicsRenderer(ISizeCalculation iSize) : this(iSize, Brushes.Black, Brushes.White)
        {
        }

        public GraphicsRenderer(ISizeCalculation iSize, Brush darkBrush, Brush lightBrush)
        {
            this.m_iSize = iSize;
            this.m_DarkBrush = darkBrush;
            this.m_LightBrush = lightBrush;
        }

        private void CreateMetaFile(BitMatrix QrMatrix, Stream stream)
        {
            using (Control control = new Control())
            {
                using (Graphics graphics = control.CreateGraphics())
                {
                    IntPtr hdc = graphics.GetHdc();
                    using (Metafile metafile = new Metafile(stream, hdc))
                    {
                        using (Graphics graphics2 = Graphics.FromImage(metafile))
                        {
                            this.Draw(graphics2, QrMatrix);
                        }
                    }
                }
            }
        }

        public void Draw(Graphics graphics, BitMatrix QrMatrix)
        {
            this.Draw(graphics, QrMatrix, new Point(0, 0));
        }

        public void Draw(Graphics graphics, BitMatrix QrMatrix, Point offset)
        {
            int matrixWidth = (QrMatrix == null) ? 0x15 : QrMatrix.Width;
            DrawingSize size = this.m_iSize.GetSize(matrixWidth);
            graphics.FillRectangle(this.m_LightBrush, offset.X, offset.Y, size.CodeWidth, size.CodeWidth);
            if ((QrMatrix != null) && (size.ModuleSize != 0))
            {
                int num2 = (size.CodeWidth - (size.ModuleSize * matrixWidth)) / 2;
                int num3 = -1;
                for (int i = 0; i < matrixWidth; i++)
                {
                    for (int j = 0; j < matrixWidth; j++)
                    {
                        if (QrMatrix[j, i])
                        {
                            if (num3 == -1)
                            {
                                num3 = j;
                            }
                            if (j == (matrixWidth - 1))
                            {
                                Point point = new Point(((num3 * size.ModuleSize) + num2) + offset.X, ((i * size.ModuleSize) + num2) + offset.Y);
                                Size size2 = new Size(((j - num3) + 1) * size.ModuleSize, size.ModuleSize);
                                graphics.FillRectangle(this.m_DarkBrush, point.X, point.Y, size2.Width, size2.Height);
                                num3 = -1;
                            }
                        }
                        else if (!QrMatrix[j, i] && (num3 != -1))
                        {
                            Point point2 = new Point(((num3 * size.ModuleSize) + num2) + offset.X, ((i * size.ModuleSize) + num2) + offset.Y);
                            Size size3 = new Size((j - num3) * size.ModuleSize, size.ModuleSize);
                            graphics.FillRectangle(this.m_DarkBrush, point2.X, point2.Y, size3.Width, size3.Height);
                            num3 = -1;
                        }
                    }
                }
            }
        }

        public void WriteToStream(BitMatrix QrMatrix, ImageFormat imageFormat, Stream stream)
        {
            this.WriteToStream(QrMatrix, imageFormat, stream, new Point(0x60, 0x60));
        }

        public void WriteToStream(BitMatrix QrMatrix, ImageFormat imageFormat, Stream stream, Point DPI)
        {
            if ((imageFormat == ImageFormat.Emf) || (imageFormat == ImageFormat.Wmf))
            {
                this.CreateMetaFile(QrMatrix, stream);
            }
            else if (((imageFormat != ImageFormat.Exif) && (imageFormat != ImageFormat.Icon)) && (imageFormat != ImageFormat.MemoryBmp))
            {
                DrawingSize size = this.m_iSize.GetSize((QrMatrix == null) ? 0x15 : QrMatrix.Width);
                using (Bitmap bitmap = new Bitmap(size.CodeWidth, size.CodeWidth))
                {
                    if ((DPI.X != 0x60) || (DPI.Y != 0x60))
                    {
                        bitmap.SetResolution((float) DPI.X, (float) DPI.Y);
                    }
                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        this.Draw(graphics, QrMatrix);
                        bitmap.Save(stream, imageFormat);
                    }
                }
            }
        }

        public Brush DarkBrush
        {
            get
            {
                return this.m_DarkBrush;
            }
            set
            {
                this.m_DarkBrush = value;
            }
        }

        public Brush LightBrush
        {
            get
            {
                return this.m_LightBrush;
            }
            set
            {
                this.m_LightBrush = value;
            }
        }

        public ISizeCalculation SizeCalculator
        {
            get
            {
                return this.m_iSize;
            }
            set
            {
                this.m_iSize = value;
            }
        }
    }
}


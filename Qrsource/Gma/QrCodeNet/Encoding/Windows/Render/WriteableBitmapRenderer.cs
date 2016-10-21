namespace Gma.QrCodeNet.Encoding.Windows.Render
{
    using Gma.QrCodeNet.Encoding;
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public class WriteableBitmapRenderer
    {
        public WriteableBitmapRenderer(ISizeCalculation iSize) : this(iSize, Colors.Black, Colors.White)
        {
        }

        public WriteableBitmapRenderer(ISizeCalculation iSize, Color darkColor, Color lightColor)
        {
            this.LightColor = lightColor;
            this.DarkColor = darkColor;
            this.ISize = iSize;
        }

        public void Draw(WriteableBitmap wBitmap, BitMatrix matrix)
        {
            this.Draw(wBitmap, matrix, 0, 0);
        }

        public void Draw(WriteableBitmap wBitmap, BitMatrix matrix, int offsetX, int offsetY)
        {
            DrawingSize size = (matrix == null) ? this.ISize.GetSize(0x15) : this.ISize.GetSize(matrix.Width);
            if (wBitmap == null)
            {
                wBitmap = new WriteableBitmap(size.CodeWidth + offsetX, size.CodeWidth + offsetY, 96.0, 96.0, PixelFormats.Gray8, null);
            }
            else if ((wBitmap.PixelHeight == 0) || (wBitmap.PixelWidth == 0))
            {
                return;
            }
            this.DrawQuietZone(wBitmap, size.CodeWidth, offsetX, offsetY);
            if (matrix != null)
            {
                this.DrawDarkModule(wBitmap, matrix, offsetX, offsetY);
            }
        }

        public void DrawDarkModule(WriteableBitmap wBitmap, BitMatrix matrix, int offsetX, int offsetY)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException("Bitmatrix");
            }
            DrawingSize size = this.ISize.GetSize(matrix.Width);
            if (wBitmap == null)
            {
                throw new ArgumentNullException("wBitmap");
            }
            if ((wBitmap.PixelHeight == 0) || (wBitmap.PixelWidth == 0))
            {
                throw new ArgumentOutOfRangeException("wBitmap", "WriteableBitmap's pixelHeight or PixelWidth are equal to zero");
            }
            int num = (size.CodeWidth - (size.ModuleSize * matrix.Width)) / 2;
            int num2 = -1;
            int moduleSize = size.ModuleSize;
            if (moduleSize != 0)
            {
                for (int i = 0; i < matrix.Width; i++)
                {
                    for (int j = 0; j < matrix.Width; j++)
                    {
                        if (matrix[j, i])
                        {
                            if (num2 == -1)
                            {
                                num2 = j;
                            }
                            if (j == (matrix.Width - 1))
                            {
                                Int32Rect rectangle = new Int32Rect(((num2 * moduleSize) + num) + offsetX, ((i * moduleSize) + num) + offsetY, ((j - num2) + 1) * moduleSize, moduleSize);
                                wBitmap.FillRectangle(rectangle, this.DarkColor);
                                num2 = -1;
                            }
                        }
                        else if (num2 != -1)
                        {
                            Int32Rect rect2 = new Int32Rect(((num2 * moduleSize) + num) + offsetX, ((i * moduleSize) + num) + offsetY, (j - num2) * moduleSize, moduleSize);
                            wBitmap.FillRectangle(rect2, this.DarkColor);
                            num2 = -1;
                        }
                    }
                }
            }
        }

        private void DrawQuietZone(WriteableBitmap wBitmap, int pixelWidth, int offsetX, int offsetY)
        {
            wBitmap.FillRectangle(new Int32Rect(offsetX, offsetY, pixelWidth, pixelWidth), this.LightColor);
        }

        public void WriteToStream(BitMatrix qrMatrix, ImageFormatEnum imageFormat, Stream stream)
        {
            DrawingSize size = this.ISize.GetSize((qrMatrix == null) ? 0x15 : qrMatrix.Width);
            WriteableBitmap wBitmap = new WriteableBitmap(size.CodeWidth, size.CodeWidth, 96.0, 96.0, PixelFormats.Gray8, null);
            this.Draw(wBitmap, qrMatrix);
            BitmapEncoder encoder = imageFormat.ChooseEncoder();
            encoder.Frames.Add(BitmapFrame.Create(wBitmap));
            encoder.Save(stream);
        }

        public Color DarkColor { get; set; }

        public ISizeCalculation ISize { get; set; }

        public Color LightColor { get; set; }
    }
}


namespace Gma.QrCodeNet.Encoding.Windows.Render
{
    using Gma.QrCodeNet.Encoding;
    using System;
    using System.IO;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public class DrawingBrushRenderer
    {
        private Brush m_DarkBrush;
        private ISizeCalculation m_ISize;
        private Brush m_LightBrush;

        public DrawingBrushRenderer(ISizeCalculation iSize) : this(iSize, Brushes.Black, Brushes.White)
        {
        }

        public DrawingBrushRenderer(ISizeCalculation iSize, Brush darkBrush, Brush lightBrush)
        {
            this.m_ISize = iSize;
            this.m_DarkBrush = darkBrush;
            this.m_LightBrush = lightBrush;
        }

        private DrawingBrush ConstructDrawingBrush(Drawing drawing)
        {
            return new DrawingBrush { Stretch = Stretch.Uniform, Drawing = drawing };
        }

        private GeometryDrawing ConstructQrDrawing(BitMatrix QrMatrix, int offsetX, int offSetY)
        {
            StreamGeometry geometry = this.DrawGeometry(QrMatrix, offsetX, offSetY);
            return new GeometryDrawing { Brush = this.m_DarkBrush, Geometry = geometry };
        }

        private GeometryDrawing ConstructQZDrawing(int width)
        {
            StreamGeometry geometry = new StreamGeometry {
                FillRule = FillRule.EvenOdd
            };
            using (StreamGeometryContext context = geometry.Open())
            {
                context.DrawRectGeometry(new Int32Rect(0, 0, width, width));
            }
            geometry.Freeze();
            return new GeometryDrawing { Brush = this.m_LightBrush, Geometry = geometry };
        }

        public DrawingBrush DrawBrush(BitMatrix QrMatrix)
        {
            if (QrMatrix == null)
            {
                return this.ConstructDrawingBrush(null);
            }
            GeometryDrawing drawing = this.ConstructQrDrawing(QrMatrix, 0, 0);
            return this.ConstructDrawingBrush(drawing);
        }

        public StreamGeometry DrawGeometry(BitMatrix QrMatrix, int offsetX, int offSetY)
        {
            int num = (QrMatrix == null) ? 0x15 : QrMatrix.Width;
            StreamGeometry geometry = new StreamGeometry {
                FillRule = FillRule.EvenOdd
            };
            if (QrMatrix != null)
            {
                using (StreamGeometryContext context = geometry.Open())
                {
                    int num2 = -1;
                    for (int i = 0; i < num; i++)
                    {
                        for (int j = 0; j < num; j++)
                        {
                            if (QrMatrix[j, i])
                            {
                                if (num2 == -1)
                                {
                                    num2 = j;
                                }
                                if (j == (num - 1))
                                {
                                    context.DrawRectGeometry(new Int32Rect(num2 + offsetX, i + offSetY, (j - num2) + 1, 1));
                                    num2 = -1;
                                }
                            }
                            else if (!QrMatrix[j, i] && (num2 != -1))
                            {
                                context.DrawRectGeometry(new Int32Rect(num2 + offsetX, i + offSetY, j - num2, 1));
                                num2 = -1;
                            }
                        }
                    }
                }
                geometry.Freeze();
            }
            return geometry;
        }

        public BitmapSource WriteToBitmapSource(BitMatrix QrMatrix, Point DPI)
        {
            int matrixWidth = (QrMatrix == null) ? 0x15 : QrMatrix.Width;
            DrawingSize size = this.m_ISize.GetSize(matrixWidth);
            int quietZoneModules = (int) size.QuietZoneModules;
            GeometryDrawing drawing = this.ConstructQZDrawing((2 * quietZoneModules) + matrixWidth);
            GeometryDrawing drawing2 = this.ConstructQrDrawing(QrMatrix, quietZoneModules, quietZoneModules);
            DrawingGroup group = new DrawingGroup {
                Children = { drawing, drawing2 }
            };
            DrawingBrush brush = this.ConstructDrawingBrush(group);
            PixelFormat pixelFormat = PixelFormats.Pbgra32;
            RenderTargetBitmap bitmap = new RenderTargetBitmap(size.CodeWidth, size.CodeWidth, DPI.X, DPI.Y, pixelFormat);
            DrawingVisual visual = new DrawingVisual();
            using (DrawingContext context = visual.RenderOpen())
            {
                context.DrawRectangle(brush, null, new Rect(0.0, 0.0, (((double) size.CodeWidth) / DPI.X) * 96.0, (((double) size.CodeWidth) / DPI.Y) * 96.0));
            }
            bitmap.Render(visual);
            return bitmap;
        }

        public void WriteToStream(BitMatrix QrMatrix, ImageFormatEnum imageFormat, Stream stream)
        {
            this.WriteToStream(QrMatrix, imageFormat, stream, new Point(96.0, 96.0));
        }

        public void WriteToStream(BitMatrix QrMatrix, ImageFormatEnum imageFormat, Stream stream, Point DPI)
        {
            BitmapSource source = this.WriteToBitmapSource(QrMatrix, DPI);
            BitmapEncoder encoder = imageFormat.ChooseEncoder();
            encoder.Frames.Add(BitmapFrame.Create(source));
            encoder.Save(stream);
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

        public ISizeCalculation ISize
        {
            get
            {
                return this.m_ISize;
            }
            set
            {
                this.m_ISize = value;
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
    }
}


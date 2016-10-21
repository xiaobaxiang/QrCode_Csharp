namespace Gma.QrCodeNet.Encoding.Windows.Render
{
    using Gma.QrCodeNet.Encoding;
    using System;
    using System.Globalization;
    using System.IO;

    public class EncapsulatedPostScriptRenderer
    {
        private GColor m_DarkColor;
        private EpsModuleDrawingTechnique m_DrawingTechnique;
        private ISizeCalculation m_iSize;
        private GColor m_LightColor;

        public EncapsulatedPostScriptRenderer(ISizeCalculation iSize, GColor darkColor, GColor lightColor)
        {
            this.m_iSize = iSize;
            this.m_DarkColor = darkColor;
            this.m_LightColor = lightColor;
            this.m_DrawingTechnique = EpsModuleDrawingTechnique.Squares;
        }

        private void DrawImage(BitMatrix matrix, StreamWriter stream)
        {
            bool flag = (((this.LightColor.R == this.LightColor.G) && (this.LightColor.G == this.LightColor.B)) && (this.DarkColor.R == this.DarkColor.G)) && (this.DarkColor.G == this.DarkColor.B);
            string str = string.Empty;
            string str2 = string.Empty;
            if (flag)
            {
                str = this.LightColor.R.ToString("x2");
                str2 = this.DarkColor.R.ToString("x2");
            }
            else
            {
                str = string.Format("{0:x2}{1:x2}{2:x2}", this.LightColor.R, this.LightColor.G, this.LightColor.B);
                str2 = string.Format("{0:x2}{1:x2}{2:x2}", this.DarkColor.R, this.DarkColor.G, this.DarkColor.B);
            }
            stream.WriteLine("% Draw squares\r\nq q translate\r\nw h scale\r\nw h 8 [w 0 0 h 0 0]\r\n{<");
            for (int i = 0; i < matrix.Height; i++)
            {
                for (int j = 0; j < matrix.Width; j++)
                {
                    stream.Write(matrix[j, i] ? str2 : str);
                }
                stream.WriteLine();
            }
            stream.WriteLine(">}");
            stream.WriteLine(flag ? "image" : "false 3 colorimage");
        }

        private void DrawSquares(BitMatrix matrix, StreamWriter stream)
        {
            string format = "\r\n% Draw squares\r\n{0} 255 div {1} 255 div {2} 255 div setrgbcolor\r\nq q translate";
            stream.WriteLine(string.Format(format, this.DarkColor.R, this.DarkColor.G, this.DarkColor.B));
            for (int i = 0; i < matrix.Height; i++)
            {
                for (int j = 0; j < matrix.Width; j++)
                {
                    if (matrix[j, i])
                    {
                        bool flag = ((j + 1) < matrix.Width) && matrix[j + 1, i];
                        bool flag2 = ((i + 1) < matrix.Height) && matrix[j, i + 1];
                        stream.WriteLine(string.Format("{0} {1} b{2}{3}", new object[] { j, i, flag ? "r" : "", flag2 ? "b" : "" }));
                    }
                }
            }
        }

        private void OutputBackground(StreamWriter stream)
        {
            string format = "\r\n% Create the background\r\n{0} 255 div {1} 255 div {2} 255 div setrgbcolor\r\nnewpath 0 0 moveto W 0 rlineto 0 H rlineto W neg 0 rlineto closepath fill";
            if (this.LightColor.A != 0)
            {
                stream.WriteLine(string.Format(format, this.LightColor.R, this.LightColor.G, this.LightColor.B));
            }
        }

        private void OutputFooter(StreamWriter stream)
        {
            string str = "\r\n% Restore the initial state\r\nrestore showpage\r\n%\r\n% End of page\r\n%\r\n%%Trailer\r\n%%EOF";
            stream.Write(str);
        }

        private void OutputHeader(DrawingSize drawingSize, StreamWriter stream)
        {
            string format = "%!PS-Adobe-3.0 EPSF-3.0\r\n%%Creator: Gma.QrCodeNet\r\n%%Title: QR Code\r\n%%CreationDate: {0:yyyyMMdd}\r\n%%Pages: 1\r\n%%BoundingBox: 0 0 {1} {2}\r\n%%Document-Fonts: Times-Roman\r\n%%LanguageLevel: 1\r\n%%EndComments\r\n%%BeginProlog\r\n/w {{ {3} }} def\r\n/h {{ {4} }} def\r\n/q {{ {5} }} def\r\n/s {{ {6} }} def\r\n/W {{ w q q add add }} def\r\n/H {{ h q q add add }} def";
            string str2 = "% Define the box functions taking X and Y coordinates of the top left corner and filling a 1 point large square\r\n/b { newpath moveto 1 0 rlineto 0 1 rlineto -1 0 rlineto closepath fill } def\r\n/br { newpath moveto 1.01 0 rlineto 0 1 rlineto -1.01 0 rlineto closepath fill } def\r\n/bb { newpath moveto 1 0 rlineto 0 1.01 rlineto -1 0 rlineto closepath fill } def\r\n/brb { newpath moveto 1.01 0 rlineto 0 1 rlineto -0.01 0 rlineto 0 0.01 rlineto -1 0 rlineto closepath fill } def";
            string str3 = "%%EndProlog\r\n%%Page: 1 1\r\n\r\n% Save the current state\r\nsave\r\n\r\n% Invert the Y axis\r\n0 W s mul translate\r\ns s neg scale";
            stream.WriteLine(string.Format(format, new object[] { DateTime.UtcNow, drawingSize.CodeWidth.ToString(CultureInfo.InvariantCulture.NumberFormat), drawingSize.CodeWidth.ToString(CultureInfo.InvariantCulture.NumberFormat), (drawingSize.CodeWidth / drawingSize.ModuleSize) - (drawingSize.QuietZoneModules * QuietZoneModules.Two), (drawingSize.CodeWidth / drawingSize.ModuleSize) - (drawingSize.QuietZoneModules * QuietZoneModules.Two), (int) drawingSize.QuietZoneModules, drawingSize.ModuleSize.ToString(CultureInfo.InvariantCulture.NumberFormat) }));
            if (this.m_DrawingTechnique == EpsModuleDrawingTechnique.Squares)
            {
                stream.WriteLine(str2);
            }
            stream.WriteLine(str3);
        }

        public void WriteToStream(BitMatrix matrix, Stream stream)
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                int matrixWidth = (matrix == null) ? 0x15 : matrix.Width;
                DrawingSize drawingSize = this.m_iSize.GetSize(matrixWidth);
                this.OutputHeader(drawingSize, writer);
                this.OutputBackground(writer);
                if (matrix != null)
                {
                    switch (this.m_DrawingTechnique)
                    {
                        case EpsModuleDrawingTechnique.Squares:
                            this.DrawSquares(matrix, writer);
                            goto Label_006A;

                        case EpsModuleDrawingTechnique.Image:
                            this.DrawImage(matrix, writer);
                            goto Label_006A;
                    }
                    throw new ArgumentOutOfRangeException("DrawingTechnique");
                }
            Label_006A:
                this.OutputFooter(writer);
            }
        }

        public GColor DarkColor
        {
            get
            {
                return this.m_DarkColor;
            }
            set
            {
                this.m_DarkColor = value;
            }
        }

        public EpsModuleDrawingTechnique DrawingTechnique
        {
            get
            {
                return this.m_DrawingTechnique;
            }
            set
            {
                this.m_DrawingTechnique = value;
            }
        }

        public GColor LightColor
        {
            get
            {
                return this.m_LightColor;
            }
            set
            {
                this.m_LightColor = value;
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


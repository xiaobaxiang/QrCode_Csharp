namespace Gma.QrCodeNet.Encoding.Windows.Render
{
    using Gma.QrCodeNet.Encoding;
    using System;
    using System.IO;
    using System.Text;

    public class SVGRenderer
    {
        private GColor m_DarkColor;
        private ISizeCalculation m_iSize;
        private GColor m_LightColor;

        public SVGRenderer(ISizeCalculation isize, GColor darkcolor, GColor lightcolor)
        {
            this.m_iSize = isize;
            this.m_DarkColor = darkcolor;
            this.m_LightColor = lightcolor;
        }

        private static void AppendDarkCell(StringBuilder sb, BitMatrix matrix, int offsetX, int offSetY)
        {
            int num = (matrix == null) ? 0x15 : matrix.Width;
            if (matrix != null)
            {
                int num2 = -1;
                for (int i = 0; i < num; i++)
                {
                    for (int j = 0; j < num; j++)
                    {
                        if (matrix[j, i])
                        {
                            if (num2 == -1)
                            {
                                num2 = j;
                            }
                            if (j == (num - 1))
                            {
                                sb.AppendRec(new MatrixPoint(num2 + offsetX, i + offSetY), new MatrixPoint((j - num2) + 1, 1));
                                num2 = -1;
                            }
                        }
                        else if (!matrix[j, i] && (num2 != -1))
                        {
                            sb.AppendRec(new MatrixPoint(num2 + offsetX, i + offSetY), new MatrixPoint(j - num2, 1));
                            num2 = -1;
                        }
                    }
                }
            }
        }

        private void AppendSVGQrCode(StringBuilder sb, BitMatrix matrix, bool includeSize, bool isStream)
        {
            DrawingSize size = this.m_iSize.GetSize(matrix.Width);
            int codeWidth = this.m_iSize.GetSize(matrix.Width).CodeWidth;
            int quietZoneModules = (int) size.QuietZoneModules;
            int num3 = (matrix == null) ? 0x15 : matrix.Width;
            sb.AppendSVGTag(includeSize ? new MatrixPoint(codeWidth, codeWidth) : new MatrixPoint(0, 0), new MatrixPoint((2 * quietZoneModules) + num3, (2 * quietZoneModules) + num3), this.m_LightColor, this.m_DarkColor);
            if (!isStream)
            {
                sb.Append("<!-- Created with Qrcode.Net (http://qrcodenet.codeplex.com/) -->");
            }
            AppendDarkCell(sb, matrix, quietZoneModules, quietZoneModules);
            sb.AppendSVGTagEnd();
        }

        public void WriteToStream(BitMatrix matrix, Stream stream)
        {
            this.WriteToStream(matrix, stream, true);
        }

        public void WriteToStream(BitMatrix matrix, Stream stream, bool includeSize)
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendHeader();
                this.AppendSVGQrCode(sb, matrix, includeSize, true);
                writer.Write(sb.ToString());
            }
        }

        public string WriteToString(BitMatrix matrix)
        {
            return this.WriteToString(matrix, true);
        }

        public string WriteToString(BitMatrix matrix, bool includeSize)
        {
            StringBuilder sb = new StringBuilder();
            this.AppendSVGQrCode(sb, matrix, includeSize, false);
            return sb.ToString();
        }
    }
}


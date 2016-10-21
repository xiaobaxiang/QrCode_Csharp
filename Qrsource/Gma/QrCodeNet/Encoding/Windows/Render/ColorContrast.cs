namespace Gma.QrCodeNet.Encoding.Windows.Render
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public static class ColorContrast
    {
        private static GColor Closest(GColor backGround, GColor frontColor)
        {
            double num = Convert.ToDouble(backGround.A) / Convert.ToDouble((byte) 0xff);
            return new FormColor(Color.FromArgb(0xff, ConvertByte((Convert.ToDouble(frontColor.R) - (Convert.ToDouble(backGround.R) * num)) / (1.0 - num)), ConvertByte((Convert.ToDouble(frontColor.G) - (Convert.ToDouble(backGround.G) * num)) / (1.0 - num)), ConvertByte((Convert.ToDouble(frontColor.B) - (Convert.ToDouble(backGround.B) * num)) / (1.0 - num))));
        }

        private static double Colorspace(byte rgb)
        {
            double num = Convert.ToDouble(rgb) / Convert.ToDouble((byte) 0xff);
            return ((num < 0.03928) ? (num / 12.92) : Math.Pow((num + 0.055) / 1.055, 2.4));
        }

        private static byte ConvertByte(double doubleVal)
        {
            if (doubleVal >= 255.0)
            {
                return 0xff;
            }
            if (doubleVal <= 0.0)
            {
                return 0;
            }
            return Convert.ToByte(doubleVal);
        }

        public static Contrast GetContrast(GColor backGround, GColor frontColor)
        {
            GColor color = new FormColor(Color.FromArgb(backGround.A, backGround.R, backGround.G, backGround.B));
            GColor color2 = new FormColor(Color.FromArgb(frontColor.A, frontColor.R, frontColor.G, frontColor.B));
            if (color.A == 0xff)
            {
                if (color2.A < 0xff)
                {
                    color2 = color2.OverlayOn(color);
                }
                double num = Luminance(color) + 0.05;
                double num2 = Luminance(color2) + 0.05;
                double num3 = (num2 > num) ? (num2 / num) : (num / num2);
                num3 = Math.Round(num3, 1);
                return new Contrast { Ratio = num3, Error = 0.0, Min = num3, Max = num3, Closet = null, Farthest = null };
            }
            double ratio = GetContrast(color.OverlayOn(Black), color2).Ratio;
            double num5 = GetContrast(color.OverlayOn(White), color2).Ratio;
            double num6 = Math.Max(ratio, num5);
            GColor color3 = Closest(backGround, frontColor);
            double num7 = GetContrast(color.OverlayOn(color3), color2).Ratio;
            return new Contrast { Ratio = Math.Round((double) ((num6 + num7) / 2.0), 2), Error = Math.Round((double) ((num6 - num7) / 2.0), 2), Min = num7, Max = num6, Closet = color3, Farthest = (num5 == num6) ? White : Black };
        }

        private static GColor InverseColor(GColor color)
        {
            return new FormColor(Color.FromArgb(color.A, 0xff - color.R, 0xff - color.G, 0xff - color.B));
        }

        private static double Luminance(GColor color)
        {
            double num = Colorspace(color.R);
            double num2 = Colorspace(color.G);
            double num3 = Colorspace(color.B);
            return (((0.2126 * num) + (0.7152 * num2)) + (0.0722 * num3));
        }

        private static GColor OverlayOn(this GColor color, GColor backGround)
        {
            if (color.A == 0xff)
            {
                return new FormColor(Color.FromArgb(color.A, color.R, color.G, color.B));
            }
            double num5 = Convert.ToDouble(color.A) / Convert.ToDouble((byte) 0xff);
            double num6 = Convert.ToDouble(backGround.A) / Convert.ToDouble((byte) 0xff);
            byte red = ConvertByte((Convert.ToDouble(color.R) * num5) + ((Convert.ToDouble(backGround.R) * num6) * (1.0 - num5)));
            byte green = ConvertByte((Convert.ToDouble(color.G) * num5) + ((Convert.ToDouble(backGround.G) * num6) * (1.0 - num5)));
            byte blue = ConvertByte((Convert.ToDouble(color.B) * num5) + ((Convert.ToDouble(backGround.B) * num6) * (1.0 - num5)));
            byte alpha = (backGround.A == 0xff) ? ((byte) 0xff) : ConvertByte((num5 + (num6 * (1.0 - num5))) * Convert.ToDouble((byte) 0xff));
            return new FormColor(Color.FromArgb(alpha, red, green, blue));
        }

        private static GColor Black
        {
            get
            {
                return new FormColor(Color.Black);
            }
        }

        private static GColor White
        {
            get
            {
                return new FormColor(Color.White);
            }
        }
    }
}


namespace Gma.QrCodeNet.Encoding.Windows.Render
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    internal static class WriteableBitmapExtension
    {
        internal static unsafe void Clear(this WriteableBitmap wBitmap, Color color)
        {
            if ((wBitmap.Format == PixelFormats.Pbgra32) || (wBitmap.Format == PixelFormats.Gray8))
            {
                byte[] buffer = ConvertColor(wBitmap.Format, color);
                int num = (wBitmap.Format == PixelFormats.Pbgra32) ? 4 : 1;
                int num2 = (wBitmap.Format == PixelFormats.Gray8) ? wBitmap.BackBufferStride : wBitmap.PixelWidth;
                int pixelHeight = wBitmap.PixelHeight;
                int num4 = num2 * pixelHeight;
                wBitmap.Lock();
                byte* backBuffer = (byte*) wBitmap.BackBuffer;
                int length = buffer.Length;
                for (int i = 0; i < length; i++)
                {
                    backBuffer[i] = buffer[i];
                }
                int num7 = 1;
                for (int j = 1; num7 < num4; j = Math.Min((int) (2 * j), (int) (num4 - num7)))
                {
                    CopyUnmanagedMemory(backBuffer, 0, backBuffer, num7 * num, j * num);
                    num7 += j;
                }
                wBitmap.AddDirtyRect(new Int32Rect(0, 0, wBitmap.PixelWidth, wBitmap.PixelHeight));
                wBitmap.Unlock();
            }
        }

        private static byte[] ConvertColor(PixelFormat format, Color color)
        {
            if (format == PixelFormats.Gray8)
            {
                return new byte[] { ((byte) ((((color.R + color.B) + color.G) / 3) & 0xff)) };
            }
            if (format != PixelFormats.Pbgra32)
            {
                throw new ArgumentOutOfRangeException("Not supported PixelFormats");
            }
            byte[] buffer = new byte[4];
            int num = color.A + 1;
            buffer[0] = (byte) (0xff & ((color.B * num) >> 8));
            buffer[1] = (byte) (0xff & ((color.G * num) >> 8));
            buffer[2] = (byte) (0xff & ((color.R * num) >> 8));
            buffer[3] = (byte) (0xff & color.A);
            return buffer;
        }

        private static unsafe void CopyUnmanagedMemory(byte* src, int srcOffset, byte* dst, int dstOffset, int count)
        {
            src += srcOffset;
            dst += dstOffset;
            memcpy(dst, src, count);
        }

        internal static unsafe void FillRectangle(this WriteableBitmap wBitmap, Int32Rect rectangle, Color color)
        {
            if ((wBitmap.Format == PixelFormats.Pbgra32) || (wBitmap.Format == PixelFormats.Gray8))
            {
                byte[] buffer = ConvertColor(wBitmap.Format, color);
                int num = (wBitmap.Format == PixelFormats.Pbgra32) ? 4 : 1;
                int num2 = (wBitmap.Format == PixelFormats.Gray8) ? wBitmap.BackBufferStride : wBitmap.PixelWidth;
                int pixelHeight = wBitmap.PixelHeight;
                if (((rectangle.X < num2) && (rectangle.Y < pixelHeight)) && ((((rectangle.X + rectangle.Width) - 1) >= 0) && (((rectangle.Y + rectangle.Height) - 1) >= 0)))
                {
                    if (rectangle.X < 0)
                    {
                        rectangle.X = 0;
                    }
                    if (rectangle.Y < 0)
                    {
                        rectangle.Y = 0;
                    }
                    if ((rectangle.X + rectangle.Width) >= num2)
                    {
                        rectangle.Width = num2 - rectangle.X;
                    }
                    if ((rectangle.Y + rectangle.Height) >= pixelHeight)
                    {
                        rectangle.Height = pixelHeight - rectangle.Y;
                    }
                    wBitmap.Lock();
                    byte* backBuffer = (byte*) wBitmap.BackBuffer;
                    int num4 = (rectangle.Y * num2) + rectangle.X;
                    int num5 = num4 + rectangle.Width;
                    int srcOffset = num4 * num;
                    int length = buffer.Length;
                    for (int i = 0; i < length; i++)
                    {
                        (backBuffer + srcOffset)[i] = buffer[i];
                    }
                    int num9 = num4 + 1;
                    for (int j = 1; num9 < num5; j = Math.Min((int) (2 * j), (int) (num5 - num9)))
                    {
                        CopyUnmanagedMemory(backBuffer, srcOffset, backBuffer, num9 * num, j * num);
                        num9 += j;
                    }
                    int num11 = (((rectangle.Y + rectangle.Height) - 1) * num2) + rectangle.X;
                    for (num9 = num4 + num2; num9 <= num11; num9 += num2)
                    {
                        CopyUnmanagedMemory(backBuffer, srcOffset, backBuffer, num9 * num, rectangle.Width * num);
                    }
                    wBitmap.AddDirtyRect(rectangle);
                    wBitmap.Unlock();
                }
            }
        }

        [DllImport("msvcrt.dll", CallingConvention=CallingConvention.Cdecl)]
        private static extern unsafe byte* memcpy(byte* dst, byte* src, int count);
    }
}


namespace Gma.QrCodeNet.Encoding.Windows.Render
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Media.Imaging;

    public static class ImageFormatExtension
    {
        public static BitmapEncoder ChooseEncoder(this ImageFormatEnum imageFormat)
        {
            switch (imageFormat)
            {
                case ImageFormatEnum.JPEG:
                    return new JpegBitmapEncoder();

                case ImageFormatEnum.BMP:
                    return new BmpBitmapEncoder();

                case ImageFormatEnum.PNG:
                    return new PngBitmapEncoder();

                case ImageFormatEnum.WDP:
                    return new WmpBitmapEncoder();

                case ImageFormatEnum.GIF:
                    return new GifBitmapEncoder();

                case ImageFormatEnum.TIFF:
                    return new TiffBitmapEncoder();
            }
            throw new ArgumentOutOfRangeException("imageFormat", imageFormat, "No such encoder support for this imageFormat");
        }
    }
}


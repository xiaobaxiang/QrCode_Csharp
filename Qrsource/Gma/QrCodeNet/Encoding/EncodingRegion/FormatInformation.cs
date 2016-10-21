namespace Gma.QrCodeNet.Encoding.EncodingRegion
{
    using Gma.QrCodeNet.Encoding;
    using Gma.QrCodeNet.Encoding.Masking;
    using System;
    using System.Runtime.CompilerServices;

    internal static class FormatInformation
    {
        private const int s_FormatInfoMaskPattern = 0x5412;
        private const int s_FormatInfoPoly = 0x537;

        internal static void EmbedFormatInformation(this TriStateMatrix triMatrix, ErrorCorrectionLevel errorlevel, Pattern pattern)
        {
            BitList formatInfoBits = GetFormatInfoBits(errorlevel, pattern);
            int width = triMatrix.Width;
            for (int i = 0; i < 15; i++)
            {
                MatrixPoint point = PointForInfo1(i);
                bool flag = formatInfoBits[i];
                triMatrix[point.X, point.Y, MatrixStatus.NoMask] = flag;
                if (i < 7)
                {
                    triMatrix[8, (width - 1) - i, MatrixStatus.NoMask] = flag;
                }
                else
                {
                    triMatrix[(width - 8) + (i - 7), 8, MatrixStatus.NoMask] = flag;
                }
            }
        }

        internal static int GetErrorCorrectionIndicatorBits(ErrorCorrectionLevel errorLevel)
        {
            switch (errorLevel)
            {
                case ErrorCorrectionLevel.L:
                    return 1;

                case ErrorCorrectionLevel.M:
                    return 0;

                case ErrorCorrectionLevel.Q:
                    return 3;

                case ErrorCorrectionLevel.H:
                    return 2;
            }
            throw new ArgumentException(string.Format("Unsupported error correction level [{0}]", errorLevel), "errorLevel");
        }

        private static BitList GetFormatInfoBits(ErrorCorrectionLevel errorlevel, Pattern pattern)
        {
            int num = ((int) pattern.MaskPatternType) | (GetErrorCorrectionIndicatorBits(errorlevel) << 3);
            int num2 = BCHCalculator.CalculateBCH(num, 0x537);
            num = (num << 10) | num2;
            num ^= 0x5412;
            BitList list = new BitList();
            list.Add(num, 15);
            if (list.Count != 15)
            {
                throw new Exception("FormatInfoBits length is not 15");
            }
            return list;
        }

        private static MatrixPoint PointForInfo1(int bitsIndex)
        {
            if (bitsIndex <= 7)
            {
                if (bitsIndex < 6)
                {
                    return new MatrixPoint(bitsIndex, 8);
                }
                return new MatrixPoint(bitsIndex + 1, 8);
            }
            if (bitsIndex != 8)
            {
                return new MatrixPoint(8, (8 - (bitsIndex - 7)) - 1);
            }
            return new MatrixPoint(8, 8 - (bitsIndex - 7));
        }
    }
}


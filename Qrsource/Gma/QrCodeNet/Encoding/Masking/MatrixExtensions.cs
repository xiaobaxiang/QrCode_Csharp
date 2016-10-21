namespace Gma.QrCodeNet.Encoding.Masking
{
    using Gma.QrCodeNet.Encoding;
    using Gma.QrCodeNet.Encoding.EncodingRegion;
    using System;
    using System.Runtime.CompilerServices;

    public static class MatrixExtensions
    {
        public static TriStateMatrix Apply(this Pattern pattern, TriStateMatrix matrix, ErrorCorrectionLevel errorlevel)
        {
            return matrix.Xor(pattern, errorlevel);
        }

        public static TriStateMatrix Apply(this TriStateMatrix matrix, Pattern pattern, ErrorCorrectionLevel errorlevel)
        {
            return matrix.Xor(pattern, errorlevel);
        }

        public static TriStateMatrix Xor(this TriStateMatrix first, Pattern second, ErrorCorrectionLevel errorlevel)
        {
            TriStateMatrix triMatrix = XorMatrix(first, second);
            triMatrix.EmbedFormatInformation(errorlevel, second);
            return triMatrix;
        }

        private static TriStateMatrix XorMatrix(TriStateMatrix first, BitMatrix second)
        {
            int width = first.Width;
            TriStateMatrix matrix = new TriStateMatrix(width);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    switch (first.MStatus(i, j))
                    {
                        case MatrixStatus.NoMask:
                            matrix[i, j, MatrixStatus.NoMask] = first[i, j];
                            break;

                        case MatrixStatus.Data:
                            matrix[i, j, MatrixStatus.Data] = first[i, j] ^ second[i, j];
                            break;

                        default:
                            throw new ArgumentException("TristateMatrix has None value cell.", "first");
                    }
                }
            }
            return matrix;
        }
    }
}


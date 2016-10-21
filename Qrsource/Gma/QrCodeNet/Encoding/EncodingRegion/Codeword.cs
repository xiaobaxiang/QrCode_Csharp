namespace Gma.QrCodeNet.Encoding.EncodingRegion
{
    using Gma.QrCodeNet.Encoding;
    using System;
    using System.Runtime.CompilerServices;

    internal static class Codeword
    {
        internal static int ChangeDirection(int directionUp)
        {
            return -directionUp;
        }

        internal static int NextY(int y, int directionUp)
        {
            return (y + directionUp);
        }

        internal static void TryEmbedCodewords(this TriStateMatrix tsMatrix, BitList codewords)
        {
            int width = tsMatrix.Width;
            int count = codewords.Count;
            int num3 = 0;
            int directionUp = -1;
            int num5 = width - 1;
            int j = width - 1;
            while (num5 > 0)
            {
                if (num5 == 6)
                {
                    num5--;
                }
                while ((j >= 0) && (j < width))
                {
                    for (int i = 0; i < 2; i++)
                    {
                        int num8 = num5 - i;
                        if (tsMatrix.MStatus(num8, j) == MatrixStatus.None)
                        {
                            bool flag;
                            if (num3 < count)
                            {
                                flag = codewords[num3];
                                num3++;
                            }
                            else
                            {
                                flag = false;
                            }
                            tsMatrix[num8, j, MatrixStatus.Data] = flag;
                        }
                    }
                    j = NextY(j, directionUp);
                }
                directionUp = ChangeDirection(directionUp);
                j = NextY(j, directionUp);
                num5 -= 2;
            }
            if (num3 != count)
            {
                throw new Exception(string.Format("Not all bits from codewords consumed by matrix: {0} / {1}", num3, count));
            }
        }
    }
}


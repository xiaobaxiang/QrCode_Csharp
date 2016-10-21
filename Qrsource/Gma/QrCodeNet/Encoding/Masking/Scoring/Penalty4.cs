namespace Gma.QrCodeNet.Encoding.Masking.Scoring
{
    using Gma.QrCodeNet.Encoding;
    using System;

    internal class Penalty4 : Penalty
    {
        internal override int PenaltyCalculate(BitMatrix matrix)
        {
            int width = matrix.Width;
            int num2 = 0;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (matrix[j, i])
                    {
                        num2++;
                    }
                }
            }
            int num5 = width * width;
            double num6 = ((double) num2) / ((double) num5);
            return ((Math.Abs((int) ((num6 * 100.0) - 50.0)) / 5) * 10);
        }
    }
}


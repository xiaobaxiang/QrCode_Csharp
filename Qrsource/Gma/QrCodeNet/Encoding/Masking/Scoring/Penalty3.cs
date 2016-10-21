namespace Gma.QrCodeNet.Encoding.Masking.Scoring
{
    using Gma.QrCodeNet.Encoding;
    using System;

    internal class Penalty3 : Penalty
    {
        private int PatternCheck(BitMatrix matrix, int i, int j, bool isHorizontal)
        {
            for (int k = 3; k >= 1; k--)
            {
                if (!(isHorizontal ? matrix[j + k, i] : matrix[i, j + k]))
                {
                    return 0;
                }
            }
            if (((j - 1) < 0) || ((j + 1) >= matrix.Width))
            {
                return 0;
            }
            if (!(isHorizontal ? matrix[j + 5, i] : matrix[i, j + 5]))
            {
                return 0;
            }
            if (!(isHorizontal ? matrix[j - 1, i] : matrix[i, j - 1]))
            {
                return 0;
            }
            if ((j - 5) >= 0)
            {
                for (int n = -2; n >= -5; n--)
                {
                    if (isHorizontal ? matrix[j + n, i] : matrix[i, j + n])
                    {
                        break;
                    }
                    if (n == -5)
                    {
                        return 40;
                    }
                }
            }
            if ((j + 9) >= matrix.Width)
            {
                return 0;
            }
            for (int m = 6; m <= 9; m++)
            {
                if (isHorizontal ? matrix[j + m, i] : matrix[i, j + m])
                {
                    return 0;
                }
            }
            return 40;
        }

        internal override int PenaltyCalculate(BitMatrix matrix)
        {
            return (this.PenaltyCalculation(matrix, true) + this.PenaltyCalculation(matrix, false));
        }

        private int PenaltyCalculation(BitMatrix matrix, bool isHorizontal)
        {
            int i = 0;
            int j = 1;
            int num3 = 0;
            int width = matrix.Width;
            while (i < width)
            {
                while (j < (width - 5))
                {
                    if (!(isHorizontal ? matrix[j + 4, i] : matrix[i, j + 4]))
                    {
                        if (!(isHorizontal ? matrix[j, i] : matrix[i, j]))
                        {
                            num3 += this.PatternCheck(matrix, i, j, isHorizontal);
                            j += 4;
                        }
                        else
                        {
                            j += 4;
                        }
                    }
                    else
                    {
                        for (int k = 4; k > 0; k--)
                        {
                            if (!(isHorizontal ? matrix[j + k, i] : matrix[i, j + k]))
                            {
                                j += k;
                                break;
                            }
                            if (k == 1)
                            {
                                j += 5;
                            }
                        }
                    }
                }
                j = 0;
                i++;
            }
            return num3;
        }
    }
}


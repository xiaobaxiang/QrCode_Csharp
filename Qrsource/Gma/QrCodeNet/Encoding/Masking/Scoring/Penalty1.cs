namespace Gma.QrCodeNet.Encoding.Masking.Scoring
{
    using Gma.QrCodeNet.Encoding;
    using System;

    internal class Penalty1 : Penalty
    {
        internal override int PenaltyCalculate(BitMatrix matrix)
        {
            MatrixSize size = matrix.Size;
            return (this.PenaltyCalculation(matrix, true) + this.PenaltyCalculation(matrix, false));
        }

        private int PenaltyCalculation(BitMatrix matrix, bool isHorizontal)
        {
            int num = 0;
            int num2 = 0;
            int width = matrix.Width;
            int num4 = 0;
            int num5 = 0;
            while (num4 < width)
            {
                while (num5 < (width - 4))
                {
                    bool flag = isHorizontal ? matrix[num5 + 4, num4] : matrix[num4, num5 + 4];
                    num2 = 1;
                    for (int i = 1; i <= 4; i++)
                    {
                        bool flag2 = isHorizontal ? matrix[(num5 + 4) - i, num4] : matrix[num4, (num5 + 4) - i];
                        if (flag2 != flag)
                        {
                            break;
                        }
                        num2++;
                    }
                    if (num2 == 1)
                    {
                        num5 += 4;
                    }
                    else
                    {
                        int num7 = 5;
                        while ((num5 + num7) < width)
                        {
                            bool flag3 = isHorizontal ? matrix[num5 + num7, num4] : matrix[num4, num5 + num7];
                            if (flag3 != flag)
                            {
                                break;
                            }
                            num2++;
                            num7++;
                        }
                        if (num2 >= 5)
                        {
                            num += 3 + (num2 - 5);
                        }
                        num5 += num7;
                    }
                }
                num5 = 0;
                num4++;
            }
            return num;
        }
    }
}


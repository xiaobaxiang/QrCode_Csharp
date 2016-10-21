namespace Gma.QrCodeNet.Encoding.Masking.Scoring
{
    using Gma.QrCodeNet.Encoding;
    using System;

    internal class Penalty2 : Penalty
    {
        internal override int PenaltyCalculate(BitMatrix matrix)
        {
            int width = matrix.Width;
            bool flag = false;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            while (num3 < (width - 1))
            {
                while (num2 < (width - 1))
                {
                    flag = matrix[num2 + 1, num3];
                    if (flag == matrix[num2 + 1, num3 + 1])
                    {
                        if (flag == matrix[num2, num3 + 1])
                        {
                            if (flag == matrix[num2, num3])
                            {
                                num4 += 3;
                                num2++;
                            }
                            else
                            {
                                num2++;
                            }
                        }
                        else
                        {
                            num2++;
                        }
                    }
                    else
                    {
                        num2 += 2;
                    }
                }
                num2 = 0;
                num3++;
            }
            return num4;
        }
    }
}


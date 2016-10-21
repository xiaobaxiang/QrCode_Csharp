namespace Gma.QrCodeNet.Encoding.EncodingRegion
{
    using System;

    internal static class BCHCalculator
    {
        private static int BinarySearchPos(int num, int lowBoundary, int highBoundary)
        {
            int num2 = (lowBoundary + highBoundary) / 2;
            int num3 = num >> num2;
            if (num3 == 1)
            {
                return num2;
            }
            if (num3 < 1)
            {
                return BinarySearchPos(num, lowBoundary, num2);
            }
            return BinarySearchPos(num, num2, highBoundary);
        }

        internal static int CalculateBCH(int num, int poly)
        {
            int num2 = PosMSB(poly);
            num = num << (num2 - 1);
            for (int i = PosMSB(num); PosMSB(num) >= num2; i = PosMSB(num))
            {
                num ^= poly << (i - num2);
            }
            return num;
        }

        internal static int PosMSB(int num)
        {
            if (num != 0)
            {
                return (BinarySearchPos(num, 0, 0x20) + 1);
            }
            return 0;
        }
    }
}


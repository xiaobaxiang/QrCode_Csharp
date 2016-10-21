namespace Gma.QrCodeNet.Encoding.Terminate
{
    using Gma.QrCodeNet.Encoding;
    using System;
    using System.Runtime.CompilerServices;

    internal static class Terminator
    {
        private const int NumBitsForByte = 8;

        private static void PadeCodewords(this BitList mainList, int numOfPadeCodewords)
        {
            if (numOfPadeCodewords < 0)
            {
                throw new ArgumentException("Num of pade codewords less than Zero");
            }
            for (int i = 1; i <= numOfPadeCodewords; i++)
            {
                if ((i % 2) == 1)
                {
                    mainList.Add(0xec, 8);
                }
                else
                {
                    mainList.Add(0x11, 8);
                }
            }
        }

        internal static void TerminateBites(this BitList baseList, int dataCount, int numTotalDataCodewords)
        {
            int num = numTotalDataCodewords << 3;
            int num2 = dataCount;
            int num3 = num - num2;
            int numBits = num3 & 7;
            int numOfPadeCodewords = num3 >> 3;
            if (numBits >= 4)
            {
                baseList.TerminatorPadding(numBits);
                baseList.PadeCodewords(numOfPadeCodewords);
            }
            else if (numOfPadeCodewords == 0)
            {
                baseList.TerminatorPadding(numBits);
            }
            else if (numOfPadeCodewords > 0)
            {
                baseList.TerminatorPadding(numBits + 8);
                baseList.PadeCodewords(numOfPadeCodewords - 1);
            }
            if (baseList.Count != num)
            {
                throw new ArgumentException(string.Format("Generate terminator and Padding fail. Num of bits need: {0}, Actually length: {1}", numOfPadeCodewords, baseList.Count - num2));
            }
        }

        private static void TerminatorPadding(this BitList mainList, int numBits)
        {
            mainList.Add(0, numBits);
        }
    }
}


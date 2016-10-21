namespace Gma.QrCodeNet.Encoding.common
{
    using Gma.QrCodeNet.Encoding;
    using System;
    using System.Runtime.CompilerServices;

    internal static class BitListExtensions
    {
        private static int InverseShiftValue(int numBitsInLastByte)
        {
            return (7 - numBitsInLastByte);
        }

        private static int ToBit(bool bit)
        {
            switch (bit)
            {
                case false:
                    return 0;

                case true:
                    return 1;
            }
            throw new ArgumentException("Invalide bit value");
        }

        internal static BitList ToBitList(byte[] bArray)
        {
            int length = bArray.Length;
            BitList list = new BitList();
            for (int i = 0; i < length; i++)
            {
                list.Add(bArray[i], 8);
            }
            return list;
        }

        internal static byte[] ToByteArray(this BitList bitList)
        {
            int count = bitList.Count;
            if ((count & 7) != 0)
            {
                throw new ArgumentException("bitList count % 8 is not equal to zero");
            }
            int num2 = count >> 3;
            byte[] buffer = new byte[num2];
            for (int i = 0; i < count; i++)
            {
                int numBitsInLastByte = i & 7;
                if (numBitsInLastByte == 0)
                {
                    buffer[i >> 3] = 0;
                }
                buffer[i >> 3] = (byte) (buffer[i >> 3] | ((byte) (ToBit(bitList[i]) << InverseShiftValue(numBitsInLastByte))));
            }
            return buffer;
        }
    }
}


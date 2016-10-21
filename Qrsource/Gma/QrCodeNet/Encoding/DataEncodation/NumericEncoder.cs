namespace Gma.QrCodeNet.Encoding.DataEncodation
{
    using Gma.QrCodeNet.Encoding;
    using System;
    using System.Runtime.InteropServices;

    internal class NumericEncoder : EncoderBase
    {
        internal NumericEncoder()
        {
        }

        protected int GetBitCountByGroupLength(int groupLength)
        {
            switch (groupLength)
            {
                case 0:
                    return 0;

                case 1:
                    return 4;

                case 2:
                    return 7;

                case 3:
                    return 10;
            }
            throw new InvalidOperationException("Unexpected group length:" + groupLength.ToString());
        }

        protected override int GetBitCountInCharCountIndicator(int version)
        {
            return CharCountIndicatorTable.GetBitCountInCharCountIndicator(Gma.QrCodeNet.Encoding.DataEncodation.Mode.Numeric, version);
        }

        internal override BitList GetDataBits(string content)
        {
            BitList list = new BitList();
            int length = content.Length;
            for (int i = 0; i < length; i += 3)
            {
                int num3 = Math.Min(3, length - i);
                int num4 = this.GetDigitGroupValue(content, i, num3);
                int bitCountByGroupLength = this.GetBitCountByGroupLength(num3);
                list.Add(num4, bitCountByGroupLength);
            }
            return list;
        }

        private int GetDigitGroupValue(string content, int startIndex, int length)
        {
            int num = 0;
            int num2 = 1;
            for (int i = 0; i < length; i++)
            {
                int num4 = ((startIndex + length) - i) - 1;
                int num5 = content[num4] - '0';
                num += num5 * num2;
                num2 *= 10;
            }
            return num;
        }

        private bool TryGetDigitGroupValue(string content, int startIndex, int length, out int value)
        {
            value = 0;
            int num = 1;
            for (int i = 0; i < length; i++)
            {
                int num3 = ((startIndex + length) - i) - 1;
                int num4 = content[num3] - '0';
                if ((num4 < 0) || (num4 > 9))
                {
                    return false;
                }
                value += num4 * num;
                num *= 10;
            }
            return true;
        }

        internal override Gma.QrCodeNet.Encoding.DataEncodation.Mode Mode
        {
            get
            {
                return Gma.QrCodeNet.Encoding.DataEncodation.Mode.Numeric;
            }
        }
    }
}


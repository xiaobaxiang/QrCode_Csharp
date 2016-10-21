namespace Gma.QrCodeNet.Encoding.DataEncodation
{
    using Gma.QrCodeNet.Encoding;
    using System;

    internal class AlphanumericEncoder : EncoderBase
    {
        private const int s_MultiplyFirstChar = 0x2d;

        internal AlphanumericEncoder()
        {
        }

        private static int GetAlphaNumValue(string content, int startIndex, int length)
        {
            int num = 0;
            int num2 = 1;
            for (int i = 0; i < length; i++)
            {
                int num4 = ((startIndex + length) - i) - 1;
                int num5 = AlphanumericTable.ConvertAlphaNumChar(content[num4]);
                num += num5 * num2;
                num2 *= 0x2d;
            }
            return num;
        }

        protected int GetBitCountByGroupLength(int groupLength)
        {
            switch (groupLength)
            {
                case 0:
                    return 0;

                case 1:
                    return 6;

                case 2:
                    return 11;
            }
            throw new InvalidOperationException(string.Format("Unexpected group length {0}", groupLength));
        }

        protected override int GetBitCountInCharCountIndicator(int version)
        {
            return CharCountIndicatorTable.GetBitCountInCharCountIndicator(Gma.QrCodeNet.Encoding.DataEncodation.Mode.Alphanumeric, version);
        }

        internal override BitList GetDataBits(string content)
        {
            BitList list = new BitList();
            int length = content.Length;
            for (int i = 0; i < length; i += 2)
            {
                int num3 = Math.Min(2, length - i);
                int num4 = GetAlphaNumValue(content, i, num3);
                int bitCountByGroupLength = this.GetBitCountByGroupLength(num3);
                list.Add(num4, bitCountByGroupLength);
            }
            return list;
        }

        internal override Gma.QrCodeNet.Encoding.DataEncodation.Mode Mode
        {
            get
            {
                return Gma.QrCodeNet.Encoding.DataEncodation.Mode.Alphanumeric;
            }
        }
    }
}


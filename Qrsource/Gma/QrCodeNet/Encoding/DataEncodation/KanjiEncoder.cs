namespace Gma.QrCodeNet.Encoding.DataEncodation
{
    using Gma.QrCodeNet.Encoding;
    using System;
    using System.Text;

    internal class KanjiEncoder : EncoderBase
    {
        private const int FST_GROUP_LOWER_BOUNDARY = 0x8140;
        private const int FST_GROUP_SUBTRACT_VALUE = 0x8140;
        private const int FST_GROUP_UPPER_BOUNDARY = 0x9ffc;
        private const int KANJI_BITCOUNT = 13;
        private const int MULTIPLY_FOR_msb = 0xc0;
        private const int SEC_GROUP_LOWER_BOUNDARY = 0xe040;
        private const int SEC_GROUP_SUBTRACT_VALUE = 0xc140;
        private const int SEC_GROUP_UPPER_BOUNDARY = 0xebbf;

        internal KanjiEncoder()
        {
        }

        private int ConvertShiftJIS(byte FirstByte, byte SecondByte)
        {
            int num = (FirstByte << 8) + (SecondByte & 0xff);
            int num2 = -1;
            if ((num >= 0x8140) && (num <= 0x9ffc))
            {
                num2 = num - 0x8140;
            }
            else
            {
                if ((num < 0xe040) || (num > 0xebbf))
                {
                    throw new ArgumentOutOfRangeException("Char is not inside acceptable range.");
                }
                num2 = num - 0xc140;
            }
            return (((num2 >> 8) * 0xc0) + (num2 & 0xff));
        }

        protected byte[] EncodeContent(string content)
        {
            byte[] bytes;
            try
            {
                bytes = Encoding.GetEncoding("shift_jis").GetBytes(content);
            }
            catch (ArgumentException exception)
            {
                throw exception;
            }
            return bytes;
        }

        protected override int GetBitCountInCharCountIndicator(int version)
        {
            return CharCountIndicatorTable.GetBitCountInCharCountIndicator(Gma.QrCodeNet.Encoding.DataEncodation.Mode.Kanji, version);
        }

        internal override BitList GetDataBits(string content)
        {
            byte[] encodeContent = this.EncodeContent(content);
            int dataLength = base.GetDataLength(content);
            return this.GetDataBitsByByteArray(encodeContent, dataLength);
        }

        internal BitList GetDataBitsByByteArray(byte[] encodeContent, int contentLength)
        {
            BitList list = new BitList();
            int length = encodeContent.Length;
            if (length != (contentLength * 2))
            {
                throw new ArgumentOutOfRangeException("Each char must be two byte length");
            }
            for (int i = 0; i < length; i += 2)
            {
                int num3 = this.ConvertShiftJIS(encodeContent[i], encodeContent[i + 1]);
                list.Add(num3, 13);
            }
            return list;
        }

        internal override Gma.QrCodeNet.Encoding.DataEncodation.Mode Mode
        {
            get
            {
                return Gma.QrCodeNet.Encoding.DataEncodation.Mode.Kanji;
            }
        }
    }
}


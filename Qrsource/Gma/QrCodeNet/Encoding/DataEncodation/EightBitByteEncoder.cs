namespace Gma.QrCodeNet.Encoding.DataEncodation
{
    using Gma.QrCodeNet.Encoding;
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;

    internal class EightBitByteEncoder : EncoderBase
    {
        private const string _defaultEncoding = "iso-8859-1";
        private const int EIGHT_BIT_BYTE_BITCOUNT = 8;

        internal EightBitByteEncoder()
        {
            this.Encoding = "iso-8859-1";
        }

        internal EightBitByteEncoder(string encoding)
        {
            this.Encoding = encoding ?? "iso-8859-1";
        }

        protected byte[] EncodeContent(string content, string encoding)
        {
            byte[] bytes;
            try
            {
                bytes = System.Text.Encoding.GetEncoding(encoding).GetBytes(content);
            }
            catch (ArgumentException exception)
            {
                throw exception;
            }
            return bytes;
        }

        protected override int GetBitCountInCharCountIndicator(int version)
        {
            return CharCountIndicatorTable.GetBitCountInCharCountIndicator(Gma.QrCodeNet.Encoding.DataEncodation.Mode.EightBitByte, version);
        }

        internal override BitList GetDataBits(string content)
        {
            ECISet set = new ECISet(ECISet.AppendOption.NameToValue);
            if (!set.ContainsECIName(this.Encoding))
            {
                throw new ArgumentOutOfRangeException("Encoding", "Current ECI table does not support this encoding. Please check ECISet class for more info");
            }
            byte[] encodeContent = this.EncodeContent(content, this.Encoding);
            return this.GetDataBitsByByteArray(encodeContent, this.Encoding);
        }

        internal BitList GetDataBitsByByteArray(byte[] encodeContent, string encodingName)
        {
            BitList list = new BitList();
            if (encodingName == "utf-8")
            {
                byte[] buffer = QRCodeConstantVariable.UTF8ByteOrderMark;
                int num = buffer.Length;
                for (int j = 0; j < num; j++)
                {
                    list.Add(buffer[j], 8);
                }
            }
            int length = encodeContent.Length;
            for (int i = 0; i < length; i++)
            {
                list.Add(encodeContent[i], 8);
            }
            return list;
        }

        internal string Encoding { get; private set; }

        internal override Gma.QrCodeNet.Encoding.DataEncodation.Mode Mode
        {
            get
            {
                return Gma.QrCodeNet.Encoding.DataEncodation.Mode.EightBitByte;
            }
        }
    }
}


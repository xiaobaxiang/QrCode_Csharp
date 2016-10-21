namespace Gma.QrCodeNet.Encoding.DataEncodation
{
    using Gma.QrCodeNet.Encoding;
    using System;

    public abstract class EncoderBase
    {
        internal EncoderBase()
        {
        }

        protected abstract int GetBitCountInCharCountIndicator(int version);
        internal BitList GetCharCountIndicator(int characterCount, int version)
        {
            BitList list = new BitList();
            int bitCountInCharCountIndicator = this.GetBitCountInCharCountIndicator(version);
            list.Add(characterCount, bitCountInCharCountIndicator);
            return list;
        }

        internal abstract BitList GetDataBits(string content);
        protected virtual int GetDataLength(string content)
        {
            return content.Length;
        }

        internal BitList GetModeIndicator()
        {
            BitList list = new BitList();
            list.Add((int) this.Mode, 4);
            return list;
        }

        internal abstract Gma.QrCodeNet.Encoding.DataEncodation.Mode Mode { get; }
    }
}


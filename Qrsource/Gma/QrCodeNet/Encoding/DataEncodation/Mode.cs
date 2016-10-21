namespace Gma.QrCodeNet.Encoding.DataEncodation
{
    using System;

    /*
     * 数据的编码模式 数字，字母，字节，日文汉字
     */
    public enum Mode
    {
        Alphanumeric = 2,
        EightBitByte = 4,
        Kanji = 8,
        Numeric = 1
    }
}


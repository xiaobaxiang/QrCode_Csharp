namespace Gma.QrCodeNet.Encoding
{
    using System;

    public static class QRCodeConstantVariable
    {
        public const string DefaultEncoding = "iso-8859-1";
        public const int MaxVersion = 40;
        public const int MinVersion = 1;
        internal const int PadeCodewordsEven = 0x11;
        internal const int PadeCodewordsOdd = 0xec;
        internal static bool[] PadeEven;
        internal static bool[] PadeOdd = new bool[] { true, true, true, false, true, true, false, false };
        internal const int PositionStencilWidth = 7;
        public const int QRCodePrimitive = 0x11d;
        internal const int TerminatorLength = 4;
        internal const int TerminatorNPaddingBit = 0;
        public const string UTF8Encoding = "utf-8";

        static QRCodeConstantVariable()
        {
            bool[] flagArray = new bool[8];
            flagArray[3] = true;
            flagArray[7] = true;
            PadeEven = flagArray;
        }

        public static byte[] UTF8ByteOrderMark
        {
            get
            {
                return new byte[] { 0xef, 0xbb, 0xbf };
            }
        }
    }
}


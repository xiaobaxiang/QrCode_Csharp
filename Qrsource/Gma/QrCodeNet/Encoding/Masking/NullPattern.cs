namespace Gma.QrCodeNet.Encoding.Masking
{
    using System;
    using System.Reflection;

    internal class NullPattern : Pattern
    {
        public override bool this[int i, int j]
        {
            get
            {
                return false;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public override Gma.QrCodeNet.Encoding.Masking.MaskPatternType MaskPatternType
        {
            get
            {
                throw new NotSupportedException("Null pattern is not supported in QR code standard.");
            }
        }
    }
}


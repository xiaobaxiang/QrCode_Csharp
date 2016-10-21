namespace Gma.QrCodeNet.Encoding.Masking
{
    using System;
    using System.Reflection;

    internal class Pattern7 : Pattern
    {
        public override bool this[int i, int j]
        {
            get
            {
                return (((((i * j) % 3) + ((i + j) % 2)) % 2) == 0);
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
                return Gma.QrCodeNet.Encoding.Masking.MaskPatternType.Type7;
            }
        }
    }
}


namespace Gma.QrCodeNet.Encoding.Masking
{
    using System;
    using System.Reflection;

    internal class Pattern5 : Pattern
    {
        public override bool this[int i, int j]
        {
            get
            {
                return ((((i * j) % 2) + ((i * j) % 3)) == 0);
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
                return Gma.QrCodeNet.Encoding.Masking.MaskPatternType.Type5;
            }
        }
    }
}


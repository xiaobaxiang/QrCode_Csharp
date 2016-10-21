namespace Gma.QrCodeNet.Encoding.Masking
{
    using System;
    using System.Reflection;

    internal class Pattern4 : Pattern
    {
        public override bool this[int i, int j]
        {
            get
            {
                return ((((j / 2) + (i / 3)) % 2) == 0);
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
                return Gma.QrCodeNet.Encoding.Masking.MaskPatternType.Type4;
            }
        }
    }
}


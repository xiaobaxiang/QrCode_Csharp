namespace Gma.QrCodeNet.Encoding.Masking
{
    using System;
    using System.Reflection;

    internal class Pattern3 : Pattern
    {
        public override bool this[int i, int j]
        {
            get
            {
                return (((j + i) % 3) == 0);
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
                return Gma.QrCodeNet.Encoding.Masking.MaskPatternType.Type3;
            }
        }
    }
}


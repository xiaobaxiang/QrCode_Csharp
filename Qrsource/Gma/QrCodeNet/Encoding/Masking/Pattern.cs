namespace Gma.QrCodeNet.Encoding.Masking
{
    using Gma.QrCodeNet.Encoding;
    using System;

    public abstract class Pattern : BitMatrix
    {
        protected Pattern()
        {
        }

        public override int Height
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public override bool[,] InternalArray
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public abstract Gma.QrCodeNet.Encoding.Masking.MaskPatternType MaskPatternType { get; }

        public override int Width
        {
            get
            {
                throw new NotSupportedException();
            }
        }
    }
}


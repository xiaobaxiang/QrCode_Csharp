namespace Gma.QrCodeNet.Encoding.Windows.Render
{
    using System;

    public abstract class GColor
    {
        protected GColor()
        {
        }

        public abstract byte A { get; }

        public abstract byte B { get; }

        public abstract byte G { get; }

        public abstract byte R { get; }
    }
}


namespace Gma.QrCodeNet.Encoding.Windows.Render
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    public class MediaColor : GColor
    {
        public MediaColor(System.Windows.Media.Color color)
        {
            this.Color = color;
        }

        public override byte A
        {
            get
            {
                return this.Color.A;
            }
        }

        public override byte B
        {
            get
            {
                return this.Color.B;
            }
        }

        public System.Windows.Media.Color Color { get; set; }

        public override byte G
        {
            get
            {
                return this.Color.G;
            }
        }

        public override byte R
        {
            get
            {
                return this.Color.R;
            }
        }
    }
}


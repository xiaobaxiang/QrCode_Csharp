namespace Gma.QrCodeNet.Encoding.Windows.Render
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class FormColor : GColor
    {
        public FormColor(System.Drawing.Color color)
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

        public System.Drawing.Color Color { get; set; }

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


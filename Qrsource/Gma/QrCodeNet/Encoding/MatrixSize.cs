namespace Gma.QrCodeNet.Encoding
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct MatrixSize
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        internal MatrixSize(int width, int height)
        {
            this = new MatrixSize();
            this.Width = width;
            this.Height = height;
        }

        public override string ToString()
        {
            return string.Format("Size({0};{1})", this.Width, this.Height);
        }
    }
}


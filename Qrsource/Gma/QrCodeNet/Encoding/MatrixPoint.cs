namespace Gma.QrCodeNet.Encoding
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct MatrixPoint
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        internal MatrixPoint(int x, int y)
        {
            this = new MatrixPoint();
            this.X = x;
            this.Y = y;
        }

        public MatrixPoint Offset(MatrixPoint offset)
        {
            return new MatrixPoint(offset.X + this.X, offset.Y + this.Y);
        }

        internal MatrixPoint Offset(int offsetX, int offsetY)
        {
            return this.Offset(new MatrixPoint(offsetX, offsetY));
        }

        public override string ToString()
        {
            return string.Format("Point({0};{1})", this.X, this.Y);
        }
    }
}


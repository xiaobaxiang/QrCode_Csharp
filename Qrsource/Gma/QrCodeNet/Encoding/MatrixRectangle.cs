namespace Gma.QrCodeNet.Encoding
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct MatrixRectangle : IEnumerable<MatrixPoint>, IEnumerable
    {
        public MatrixPoint Location { get; private set; }
        public MatrixSize Size { get; private set; }
        internal MatrixRectangle(MatrixPoint location, MatrixSize size)
        {
            this = new MatrixRectangle();
            this.Location = location;
            this.Size = size;
        }

        public IEnumerator<MatrixPoint> GetEnumerator()
        {
            for (int i = this.Location.Y; i < (this.Location.Y + this.Size.Height); i++)
            {
                for (int j = this.Location.X; j < (this.Location.X + this.Size.Width); j++)
                {
                    yield return new MatrixPoint(j, i);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public override string ToString()
        {
            return string.Format("Rectangle({0};{1}):({2} x {3})", new object[] { this.Location.X, this.Location.Y, this.Size.Width, this.Size.Height });
        }
    }
}


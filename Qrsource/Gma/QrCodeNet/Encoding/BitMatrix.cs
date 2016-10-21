namespace Gma.QrCodeNet.Encoding
{
    using System;
    using System.Reflection;

    public abstract class BitMatrix
    {
        protected BitMatrix()
        {
        }

        internal void CopyTo(TriStateMatrix target, MatrixPoint targetPoint, MatrixStatus mstatus)
        {
            this.CopyTo(target, new MatrixRectangle(new MatrixPoint(0, 0), new MatrixSize(this.Width, this.Height)), targetPoint, mstatus);
        }

        internal void CopyTo(TriStateMatrix target, MatrixRectangle sourceArea, MatrixPoint targetPoint, MatrixStatus mstatus)
        {
            for (int i = 0; i < sourceArea.Size.Height; i++)
            {
                for (int j = 0; j < sourceArea.Size.Width; j++)
                {
                    bool flag = this[sourceArea.Location.X + j, sourceArea.Location.Y + i];
                    target[targetPoint.X + j, targetPoint.Y + i, mstatus] = flag;
                }
            }
        }

        public abstract int Height { get; }

        public abstract bool[,] InternalArray { get; }

        public abstract bool this[int i, int j] { get; set; }

        internal bool this[MatrixPoint point]
        {
            get
            {
                return this[point.X, point.Y];
            }
            set
            {
                this[point.X, point.Y] = value;
            }
        }

        internal MatrixSize Size
        {
            get
            {
                return new MatrixSize(this.Width, this.Height);
            }
        }

        public abstract int Width { get; }
    }
}


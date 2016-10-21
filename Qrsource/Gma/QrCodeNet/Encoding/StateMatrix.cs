namespace Gma.QrCodeNet.Encoding
{
    using System;
    using System.Reflection;

    public sealed class StateMatrix
    {
        private MatrixStatus[,] m_matrixStatus;
        private readonly int m_Width;

        public StateMatrix(int width)
        {
            this.m_Width = width;
            this.m_matrixStatus = new MatrixStatus[width, width];
        }

        public int Height
        {
            get
            {
                return this.Width;
            }
        }

        public MatrixStatus this[int x, int y]
        {
            get
            {
                return this.m_matrixStatus[x, y];
            }
            set
            {
                this.m_matrixStatus[x, y] = value;
            }
        }

        internal MatrixStatus this[MatrixPoint point]
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

        public int Width
        {
            get
            {
                return this.m_Width;
            }
        }
    }
}


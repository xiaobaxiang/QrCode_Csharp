namespace Gma.QrCodeNet.Encoding.Common
{
    using System;
    using System.Reflection;

    public sealed class ByteMatrix
    {
        private readonly sbyte[,] m_Bytes;

        internal ByteMatrix(int width, int height)
        {
            this.m_Bytes = new sbyte[height, width];
        }

        internal void Clear(sbyte value)
        {
            this.ForAll((x, y, matrix) => matrix[x, y] = value);
        }

        internal void ForAll(Action<int, int, ByteMatrix> action)
        {
            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    action(j, i, this);
                }
            }
        }

        internal int Height
        {
            get
            {
                return this.m_Bytes.GetLength(0);
            }
        }

        internal sbyte this[int x, int y]
        {
            get
            {
                return this.m_Bytes[y, x];
            }
            set
            {
                this.m_Bytes[y, x] = value;
            }
        }

        internal int Width
        {
            get
            {
                return this.m_Bytes.GetLength(1);
            }
        }
    }
}


namespace Gma.QrCodeNet.Encoding
{
    using System;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public class SquareBitMatrix : BitMatrix
    {
        private readonly bool[,] m_InternalArray;
        private readonly int m_Width;

        public SquareBitMatrix(int width)
        {
            this.m_InternalArray = new bool[width, width];
            this.m_Width = width;
        }

        internal SquareBitMatrix(bool[,] internalArray)
        {
            this.m_InternalArray = internalArray;
            int length = internalArray.GetLength(0);
            this.m_Width = length;
        }

        public static bool CreateSquareBitMatrix(bool[,] internalArray, out SquareBitMatrix triStateMatrix)
        {
            triStateMatrix = null;
            if ((internalArray != null) && (internalArray.GetLength(0) == internalArray.GetLength(1)))
            {
                triStateMatrix = new SquareBitMatrix(internalArray);
                return true;
            }
            return false;
        }

        public override int Height
        {
            get
            {
                return this.Width;
            }
        }

        public override bool[,] InternalArray
        {
            get
            {
                bool[,] flagArray = new bool[this.m_Width, this.m_Width];
                for (int i = 0; i < this.m_Width; i++)
                {
                    for (int j = 0; j < this.m_Width; j++)
                    {
                        flagArray[i, j] = this.m_InternalArray[i, j];
                    }
                }
                return flagArray;
            }
        }

        public override bool this[int i, int j]
        {
            get
            {
                return this.m_InternalArray[i, j];
            }
            set
            {
                this.m_InternalArray[i, j] = value;
            }
        }

        public override int Width
        {
            get
            {
                return this.m_Width;
            }
        }
    }
}


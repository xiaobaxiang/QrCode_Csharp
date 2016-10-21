namespace Gma.QrCodeNet.Encoding
{
    using System;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public class TriStateMatrix : BitMatrix
    {
        private readonly bool[,] m_InternalArray;
        private readonly StateMatrix m_stateMatrix;
        private readonly int m_Width;

        public TriStateMatrix(int width)
        {
            this.m_stateMatrix = new StateMatrix(width);
            this.m_InternalArray = new bool[width, width];
            this.m_Width = width;
        }

        internal TriStateMatrix(bool[,] internalArray)
        {
            this.m_InternalArray = internalArray;
            int length = internalArray.GetLength(0);
            this.m_stateMatrix = new StateMatrix(length);
            this.m_Width = length;
        }

        public static bool CreateTriStateMatrix(bool[,] internalArray, out TriStateMatrix triStateMatrix)
        {
            triStateMatrix = null;
            if ((internalArray != null) && (internalArray.GetLength(0) == internalArray.GetLength(1)))
            {
                triStateMatrix = new TriStateMatrix(internalArray);
                return true;
            }
            return false;
        }

        internal MatrixStatus MStatus(MatrixPoint point)
        {
            return this.MStatus(point.X, point.Y);
        }

        internal MatrixStatus MStatus(int i, int j)
        {
            return this.m_stateMatrix[i, j];
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
                if ((this.MStatus(i, j) == MatrixStatus.None) || (this.MStatus(i, j) == MatrixStatus.NoMask))
                {
                    throw new InvalidOperationException(string.Format("The value of cell [{0},{1}] is not set or is Stencil.", i, j));
                }
                this.m_InternalArray[i, j] = value;
            }
        }

        public bool this[int i, int j, MatrixStatus mstatus]
        {
            set
            {
                this.m_stateMatrix[i, j] = mstatus;
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


namespace Gma.QrCodeNet.Encoding.Positioning.Stencils
{
    using Gma.QrCodeNet.Encoding;
    using System;

    internal class PositionDetectionPattern : PatternStencilBase
    {
        private static readonly bool[,] s_PositionDetection = new bool[,] { { false, false, false, false, false, false, false, false, false }, { false, true, true, true, true, true, true, true, false }, { false, true, false, false, false, false, false, true, false }, { false, true, false, true, true, true, false, true, false }, { false, true, false, true, true, true, false, true, false }, { false, true, false, true, true, true, false, true, false }, { false, true, false, false, false, false, false, true, false }, { false, true, true, true, true, true, true, true, false }, { false, false, false, false, false, false, false, false, false } };

        public PositionDetectionPattern(int version) : base(version)
        {
        }

        public override void ApplyTo(TriStateMatrix matrix)
        {
            MatrixSize sizeOfSquareWithSeparators = this.GetSizeOfSquareWithSeparators();
            MatrixPoint targetPoint = new MatrixPoint(0, 0);
            base.CopyTo(matrix, new MatrixRectangle(new MatrixPoint(1, 1), sizeOfSquareWithSeparators), targetPoint, MatrixStatus.NoMask);
            MatrixPoint point2 = new MatrixPoint((matrix.Width - this.Width) + 1, 0);
            base.CopyTo(matrix, new MatrixRectangle(new MatrixPoint(0, 1), sizeOfSquareWithSeparators), point2, MatrixStatus.NoMask);
            MatrixPoint point3 = new MatrixPoint(0, (matrix.Width - this.Width) + 1);
            base.CopyTo(matrix, new MatrixRectangle(new MatrixPoint(1, 0), sizeOfSquareWithSeparators), point3, MatrixStatus.NoMask);
        }

        private MatrixSize GetSizeOfSquareWithSeparators()
        {
            return new MatrixSize(this.Width - 1, this.Height - 1);
        }

        public override bool[,] Stencil
        {
            get
            {
                return s_PositionDetection;
            }
        }
    }
}


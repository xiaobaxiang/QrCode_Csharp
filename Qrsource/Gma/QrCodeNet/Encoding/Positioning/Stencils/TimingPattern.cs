namespace Gma.QrCodeNet.Encoding.Positioning.Stencils
{
    using Gma.QrCodeNet.Encoding;
    using System;

    internal class TimingPattern : PatternStencilBase
    {
        public TimingPattern(int version) : base(version)
        {
        }

        public override void ApplyTo(TriStateMatrix matrix)
        {
            for (int i = 8; i < (matrix.Width - 8); i++)
            {
                bool flag = ((sbyte) ((i + 1) % 2)) == 1;
                if (matrix.MStatus(6, i) == MatrixStatus.None)
                {
                    matrix[6, i, MatrixStatus.NoMask] = flag;
                }
                if (matrix.MStatus(i, 6) == MatrixStatus.None)
                {
                    matrix[i, 6, MatrixStatus.NoMask] = flag;
                }
            }
        }

        public override bool[,] Stencil
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}


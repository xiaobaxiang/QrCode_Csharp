namespace Gma.QrCodeNet.Encoding.Positioning.Stencils
{
    using Gma.QrCodeNet.Encoding;
    using System;

    internal class DarkDotAtLeftBottom : PatternStencilBase
    {
        public DarkDotAtLeftBottom(int version) : base(version)
        {
        }

        public override void ApplyTo(TriStateMatrix matrix)
        {
            matrix[8, matrix.Width - 8, MatrixStatus.NoMask] = true;
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


namespace Gma.QrCodeNet.Encoding.Positioning
{
    using Gma.QrCodeNet.Encoding;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal static class TriStateMatrixExtensions
    {
        internal static TriStateMatrix Embed(this TriStateMatrix matrix, BitMatrix stencil, MatrixPoint location)
        {
            stencil.CopyTo(matrix, location, MatrixStatus.NoMask);
            return matrix;
        }

        internal static TriStateMatrix Embed(this TriStateMatrix matrix, BitMatrix stencil, IEnumerable<MatrixPoint> locations)
        {
            foreach (MatrixPoint point in locations)
            {
                matrix.Embed(stencil, point);
            }
            return matrix;
        }
    }
}


namespace Gma.QrCodeNet.Encoding.Positioning.Stencils
{
    using Gma.QrCodeNet.Encoding;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Linq;

    internal class AlignmentPattern : PatternStencilBase
    {
        private static readonly bool[,] s_AlignmentPattern = new bool[,] { { true, true, true, true, true }, { true, false, false, false, true }, { true, false, true, false, true }, { true, false, false, false, true }, { true, true, true, true, true } };
        private static readonly byte[][] s_AlignmentPatternCoordinatesByVersion;

        static AlignmentPattern()
        {
            byte[][] bufferArray = new byte[0x29][];
            bufferArray[1] = new byte[0];
            bufferArray[2] = new byte[] { 6, 0x12 };
            bufferArray[3] = new byte[] { 6, 0x16 };
            bufferArray[4] = new byte[] { 6, 0x1a };
            bufferArray[5] = new byte[] { 6, 30 };
            bufferArray[6] = new byte[] { 6, 0x22 };
            bufferArray[7] = new byte[] { 6, 0x16, 0x26 };
            bufferArray[8] = new byte[] { 6, 0x18, 0x2a };
            bufferArray[9] = new byte[] { 6, 0x1a, 0x2e };
            bufferArray[10] = new byte[] { 6, 0x1c, 50 };
            bufferArray[11] = new byte[] { 6, 30, 0x36 };
            bufferArray[12] = new byte[] { 6, 0x20, 0x3a };
            bufferArray[13] = new byte[] { 6, 0x22, 0x3e };
            bufferArray[14] = new byte[] { 6, 0x1a, 0x2e, 0x42 };
            bufferArray[15] = new byte[] { 6, 0x1a, 0x30, 70 };
            bufferArray[0x10] = new byte[] { 6, 0x1a, 50, 0x4a };
            bufferArray[0x11] = new byte[] { 6, 30, 0x36, 0x4e };
            bufferArray[0x12] = new byte[] { 6, 30, 0x38, 0x52 };
            bufferArray[0x13] = new byte[] { 6, 30, 0x3a, 0x56 };
            bufferArray[20] = new byte[] { 6, 0x22, 0x3e, 90 };
            bufferArray[0x15] = new byte[] { 6, 0x1c, 50, 0x48, 0x5e };
            bufferArray[0x16] = new byte[] { 6, 0x1a, 50, 0x4a, 0x62 };
            bufferArray[0x17] = new byte[] { 6, 30, 0x36, 0x4e, 0x66 };
            bufferArray[0x18] = new byte[] { 6, 0x1c, 0x36, 80, 0x6a };
            bufferArray[0x19] = new byte[] { 6, 0x20, 0x3a, 0x54, 110 };
            bufferArray[0x1a] = new byte[] { 6, 30, 0x3a, 0x56, 0x72 };
            bufferArray[0x1b] = new byte[] { 6, 0x22, 0x3e, 90, 0x76 };
            bufferArray[0x1c] = new byte[] { 6, 0x1a, 50, 0x4a, 0x62, 0x7a };
            bufferArray[0x1d] = new byte[] { 6, 30, 0x36, 0x4e, 0x66, 0x7e };
            bufferArray[30] = new byte[] { 6, 0x1a, 0x34, 0x4e, 0x68, 130 };
            bufferArray[0x1f] = new byte[] { 6, 30, 0x38, 0x52, 0x6c, 0x86 };
            bufferArray[0x20] = new byte[] { 6, 0x22, 60, 0x56, 0x70, 0x8a };
            bufferArray[0x21] = new byte[] { 6, 30, 0x3a, 0x56, 0x72, 0x8e };
            bufferArray[0x22] = new byte[] { 6, 0x22, 0x3e, 90, 0x76, 0x92 };
            bufferArray[0x23] = new byte[] { 6, 30, 0x36, 0x4e, 0x66, 0x7e, 150 };
            bufferArray[0x24] = new byte[] { 6, 0x18, 50, 0x4c, 0x66, 0x80, 0x9a };
            bufferArray[0x25] = new byte[] { 6, 0x1c, 0x36, 80, 0x6a, 0x84, 0x9e };
            bufferArray[0x26] = new byte[] { 6, 0x20, 0x3a, 0x54, 110, 0x88, 0xa2 };
            bufferArray[0x27] = new byte[] { 6, 0x1a, 0x36, 0x52, 110, 0x8a, 0xa6 };
            bufferArray[40] = new byte[] { 6, 30, 0x3a, 0x56, 0x72, 0x8e, 170 };
            s_AlignmentPatternCoordinatesByVersion = bufferArray;
        }

        public AlignmentPattern(int version) : base(version)
        {
        }

        public override void ApplyTo(TriStateMatrix matrix)
        {
            foreach (MatrixPoint point in this.GetNonColidingCoordinatePairs(matrix))
            {
                base.CopyTo(matrix, point, MatrixStatus.NoMask);
            }
        }

        private IEnumerable<MatrixPoint> GetAllCoordinatePairs()
        {
            IEnumerable<byte> patternCoordinatesByVersion = GetPatternCoordinatesByVersion(this.Version);
            foreach (byte iteratorVariable1 in patternCoordinatesByVersion)
            {
                foreach (byte iteratorVariable2 in patternCoordinatesByVersion)
                {
                    MatrixPoint iteratorVariable3 = new MatrixPoint(iteratorVariable1 - 2, iteratorVariable2 - 2);
                    yield return iteratorVariable3;
                }
            }
        }

        public IEnumerable<MatrixPoint> GetNonColidingCoordinatePairs(TriStateMatrix matrix)
        {
            return (from point in this.GetAllCoordinatePairs()
                where matrix.MStatus(point.Offset(2, 2)) == MatrixStatus.None
                select point);
        }

        private static IEnumerable<byte> GetPatternCoordinatesByVersion(int version)
        {
            return s_AlignmentPatternCoordinatesByVersion[version];
        }

        public override bool[,] Stencil
        {
            get
            {
                return s_AlignmentPattern;
            }
        }

    }
}


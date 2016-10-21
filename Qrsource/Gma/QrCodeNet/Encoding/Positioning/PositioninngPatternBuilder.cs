namespace Gma.QrCodeNet.Encoding.Positioning
{
    using Gma.QrCodeNet.Encoding;
    using Gma.QrCodeNet.Encoding.Positioning.Stencils;
    using System;

    internal static class PositioninngPatternBuilder
    {
        internal static void EmbedBasicPatterns(int version, TriStateMatrix matrix)
        {
            new PositionDetectionPattern(version).ApplyTo(matrix);
            new DarkDotAtLeftBottom(version).ApplyTo(matrix);
            new AlignmentPattern(version).ApplyTo(matrix);
            new TimingPattern(version).ApplyTo(matrix);
        }
    }
}


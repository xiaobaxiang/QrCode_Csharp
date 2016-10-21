namespace Gma.QrCodeNet.Encoding.Versions
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct ErrorCorrectionBlock
    {
        internal int NumErrorCorrectionBlock { get; private set; }
        internal int NumDataCodewords { get; private set; }
        internal ErrorCorrectionBlock(int numErrorCorrectionBlock, int numDataCodewards)
        {
            this = new ErrorCorrectionBlock();
            this.NumErrorCorrectionBlock = numErrorCorrectionBlock;
            this.NumDataCodewords = numDataCodewards;
        }
    }
}


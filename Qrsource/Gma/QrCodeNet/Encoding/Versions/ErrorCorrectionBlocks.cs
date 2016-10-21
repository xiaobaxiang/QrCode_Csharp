namespace Gma.QrCodeNet.Encoding.Versions
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct ErrorCorrectionBlocks
    {
        private ErrorCorrectionBlock[] m_ECBlock;
        internal int NumErrorCorrectionCodewards { get; private set; }
        internal int NumBlocks { get; private set; }
        internal int ErrorCorrectionCodewordsPerBlock { get; private set; }
        internal ErrorCorrectionBlocks(int numErrorCorrectionCodewords, ErrorCorrectionBlock ecBlock)
        {
            this = new ErrorCorrectionBlocks();
            this.NumErrorCorrectionCodewards = numErrorCorrectionCodewords;
            this.m_ECBlock = new ErrorCorrectionBlock[] { ecBlock };
            this.initialize();
        }

        internal ErrorCorrectionBlocks(int numErrorCorrectionCodewords, ErrorCorrectionBlock ecBlock1, ErrorCorrectionBlock ecBlock2)
        {
            this = new ErrorCorrectionBlocks();
            this.NumErrorCorrectionCodewards = numErrorCorrectionCodewords;
            this.m_ECBlock = new ErrorCorrectionBlock[] { ecBlock1, ecBlock2 };
            this.initialize();
        }

        internal ErrorCorrectionBlock[] GetECBlocks()
        {
            return this.m_ECBlock;
        }

        private void initialize()
        {
            if (this.m_ECBlock == null)
            {
                throw new ArgumentNullException("ErrorCorrectionBlocks array doesn't contain any value");
            }
            this.NumBlocks = 0;
            int length = this.m_ECBlock.Length;
            for (int i = 0; i < length; i++)
            {
                this.NumBlocks += this.m_ECBlock[i].NumErrorCorrectionBlock;
            }
            this.ErrorCorrectionCodewordsPerBlock = this.NumErrorCorrectionCodewards / this.NumBlocks;
        }
    }
}


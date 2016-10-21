namespace Gma.QrCodeNet.Encoding.Versions
{
    using Gma.QrCodeNet.Encoding;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct QRCodeVersion
    {
        private ErrorCorrectionBlocks[] m_ECBlocks;
        internal int VersionNum { get; private set; }
        internal int TotalCodewords { get; private set; }
        internal int DimensionForVersion { get; private set; }
        internal QRCodeVersion(int versionNum, int totalCodewords, ErrorCorrectionBlocks ecblocksL, ErrorCorrectionBlocks ecblocksM, ErrorCorrectionBlocks ecblocksQ, ErrorCorrectionBlocks ecblocksH)
        {
            this = new QRCodeVersion();
            this.VersionNum = versionNum;
            this.TotalCodewords = totalCodewords;
            this.m_ECBlocks = new ErrorCorrectionBlocks[] { ecblocksL, ecblocksM, ecblocksQ, ecblocksH };
            this.DimensionForVersion = 0x11 + (versionNum * 4);
        }

        internal ErrorCorrectionBlocks GetECBlocksByLevel(ErrorCorrectionLevel ECLevel)
        {
            switch (ECLevel)
            {
                case ErrorCorrectionLevel.L:
                    return this.m_ECBlocks[0];

                case ErrorCorrectionLevel.M:
                    return this.m_ECBlocks[1];

                case ErrorCorrectionLevel.Q:
                    return this.m_ECBlocks[2];

                case ErrorCorrectionLevel.H:
                    return this.m_ECBlocks[3];
            }
            throw new ArgumentOutOfRangeException("Invalide ErrorCorrectionLevel");
        }
    }
}


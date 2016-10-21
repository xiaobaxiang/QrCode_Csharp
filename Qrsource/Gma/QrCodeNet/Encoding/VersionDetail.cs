namespace Gma.QrCodeNet.Encoding
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    /*
     * 版本信息：即二维码的规格，
     * QR码符号共有40种规格的矩阵（一般为黑白色），从21x21（版本1），到177x177（版本40），
     * 每一版本符号比前一版本 每边增加4个模块。
     */
    [StructLayout(LayoutKind.Sequential)]
    public struct VersionDetail
    {
        internal int Version { get; private set; }
        internal int NumTotalBytes { get; private set; }
        internal int NumDataBytes { get; private set; }
        internal int NumECBlocks { get; private set; }
        internal VersionDetail(int version, int numTotalBytes, int numDataBytes, int numECBlocks)
        {
            this = new VersionDetail();
            this.Version = version;
            this.NumTotalBytes = numTotalBytes;
            this.NumDataBytes = numDataBytes;
            this.NumECBlocks = numECBlocks;
        }

        internal int MatrixWidth
        {
            get
            {
                return Width(this.Version);
            }
        }
        internal static int Width(int version)
        {
            return (0x11 + (4 * version));
        }

        internal int ECBlockGroup1
        {
            get
            {
                return (this.NumECBlocks - this.ECBlockGroup2);
            }
        }
        internal int ECBlockGroup2
        {
            get
            {
                return (this.NumTotalBytes % this.NumECBlocks);
            }
        }
        internal int NumDataBytesGroup1
        {
            get
            {
                return (this.NumDataBytes / this.NumECBlocks);
            }
        }
        internal int NumDataBytesGroup2
        {
            get
            {
                return (this.NumDataBytesGroup1 + 1);
            }
        }
        internal int NumECBytesPerBlock
        {
            get
            {
                return ((this.NumTotalBytes - this.NumDataBytes) / this.NumECBlocks);
            }
        }
        public override string ToString()
        {
            return string.Concat(new object[] { this.Version, ";", this.NumTotalBytes, ";", this.NumDataBytes, ";", this.NumECBlocks });
        }
    }
}


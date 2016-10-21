namespace Gma.QrCodeNet.Encoding.DataEncodation
{
    using Gma.QrCodeNet.Encoding;
    using Gma.QrCodeNet.Encoding.Versions;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]//设置栈中成员的顺序布局，还有一种Explicit,精确布局
    internal struct EncodationStruct
    {
        internal Gma.QrCodeNet.Encoding.VersionDetail VersionDetail { get; set; }
        internal Gma.QrCodeNet.Encoding.DataEncodation.Mode Mode { get; set; }
        internal BitList DataCodewords { get; set; }
        internal EncodationStruct(VersionControlStruct vcStruct)
        {
            this = new EncodationStruct();
            this.VersionDetail = vcStruct.VersionDetail;
        }
    }
}


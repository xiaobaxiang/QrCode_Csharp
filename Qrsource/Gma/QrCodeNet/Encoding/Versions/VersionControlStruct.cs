namespace Gma.QrCodeNet.Encoding.Versions
{
    using Gma.QrCodeNet.Encoding;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct VersionControlStruct
    {
        internal Gma.QrCodeNet.Encoding.VersionDetail VersionDetail { get; set; }
        internal bool isContainECI { get; set; }
        internal BitList ECIHeader { get; set; }
    }
}


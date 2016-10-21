namespace Gma.QrCodeNet.Encoding.ReedSolomon
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct PolyDivideStruct
    {
        internal Polynomial Quotient { get; private set; }
        internal Polynomial Remainder { get; private set; }
        internal PolyDivideStruct(Polynomial quotient, Polynomial remainder)
        {
            this = new PolyDivideStruct();
            this.Quotient = quotient;
            this.Remainder = remainder;
        }
    }
}


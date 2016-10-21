namespace Gma.QrCodeNet.Encoding
{
    using System;

    public enum ErrorCorrectionLevel
    {
        L,// 7%的字码可被修复
        M,//15%的字码可被修复
        Q,//25%的字码可被修复
        H //30%的字码可被修复
    }
}


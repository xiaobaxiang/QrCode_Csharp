namespace Gma.QrCodeNet.Encoding.DataEncodation.InputRecognition
{
    using Gma.QrCodeNet.Encoding.DataEncodation;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct RecognitionStruct
    {
        public Gma.QrCodeNet.Encoding.DataEncodation.Mode Mode { get; private set; }
        public string EncodingName { get; private set; }
        public RecognitionStruct(Gma.QrCodeNet.Encoding.DataEncodation.Mode mode, string encodingName)
        {
            this = new RecognitionStruct();
            this.Mode = mode;
            this.EncodingName = encodingName;
        }
    }
}


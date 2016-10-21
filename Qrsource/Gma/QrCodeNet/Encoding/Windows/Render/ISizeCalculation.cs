namespace Gma.QrCodeNet.Encoding.Windows.Render
{
    using System;

    public interface ISizeCalculation
    {
        DrawingSize GetSize(int matrixWidth);
    }
}


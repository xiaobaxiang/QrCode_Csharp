namespace Gma.QrCodeNet.Encoding
{
    using System;

    public class InputOutOfBoundaryException : Exception
    {
        public InputOutOfBoundaryException()
        {
        }

        public InputOutOfBoundaryException(string message) : base(message)
        {
        }
    }
}


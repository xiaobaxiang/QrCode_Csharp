namespace Gma.QrCodeNet.Encoding.ReedSolomon
{
    using System;

    internal sealed class ReedSolomonEncoder
    {
        private static int[] ConvertToIntArray(byte[] dataBytes, int dataLength, int numECBytes)
        {
            int[] numArray = new int[dataLength + numECBytes];
            for (int i = 0; i < dataLength; i++)
            {
                numArray[i] = dataBytes[i] & 0xff;
            }
            return numArray;
        }

        private static byte[] ConvertTosByteArray(int[] remainder, int numECBytes)
        {
            int length = remainder.Length;
            if (length > numECBytes)
            {
                throw new ArgumentException("Num of remainder bytes can not larger than numECBytes");
            }
            int num2 = numECBytes - length;
            byte[] buffer = new byte[numECBytes];
            for (int i = 0; i < num2; i++)
            {
                buffer[i] = 0;
            }
            for (int j = 0; j < length; j++)
            {
                buffer[num2 + j] = (byte) remainder[j];
            }
            return buffer;
        }

        internal static byte[] Encode(byte[] dataBytes, int numECBytes, GeneratorPolynomial generatorPoly)
        {
            int length = dataBytes.Length;
            if (generatorPoly == null)
            {
                throw new ArgumentNullException("generator", "GeneratorPolynomial var is null");
            }
            if (length == 0)
            {
                throw new ArgumentException("There is no data bytes to encode");
            }
            if (numECBytes <= 0)
            {
                throw new ArgumentException("No Error Correction bytes");
            }
            int[] coefficients = ConvertToIntArray(dataBytes, length, numECBytes);
            Polynomial generator = generatorPoly.GetGenerator(numECBytes);
            Polynomial polynomial2 = new Polynomial(generator.GField, coefficients);
            return ConvertTosByteArray(polynomial2.Divide(generator).Remainder.Coefficients, numECBytes);
        }
    }
}


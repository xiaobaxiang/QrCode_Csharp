namespace Gma.QrCodeNet.Encoding.ErrorCorrection
{
    using Gma.QrCodeNet.Encoding;
    using Gma.QrCodeNet.Encoding.ReedSolomon;
    using System;
    using System.Collections.Generic;

    internal static class ECGenerator
    {
        internal static BitList FillECCodewords(BitList dataCodewords, VersionDetail vd)
        {
            List<byte> list = dataCodewords.List;
            int num1 = vd.ECBlockGroup2;
            int num = vd.ECBlockGroup1;
            int num2 = vd.NumDataBytesGroup1;
            int num3 = vd.NumDataBytesGroup2;
            int numECBytesPerBlock = vd.NumECBytesPerBlock;
            int num5 = 0;
            byte[][] bufferArray = new byte[vd.NumECBlocks][];
            byte[][] bufferArray2 = new byte[vd.NumECBlocks][];
            GeneratorPolynomial generatorPoly = new GeneratorPolynomial(GaloisField256.QRCodeGaloisField);
            for (int i = 0; i < vd.NumECBlocks; i++)
            {
                if (i < num)
                {
                    bufferArray[i] = new byte[num2];
                    for (int m = 0; m < num2; m++)
                    {
                        bufferArray[i][m] = list[num5 + m];
                    }
                    num5 += num2;
                }
                else
                {
                    bufferArray[i] = new byte[num3];
                    for (int n = 0; n < num3; n++)
                    {
                        bufferArray[i][n] = list[num5 + n];
                    }
                    num5 += num3;
                }
                bufferArray2[i] = ReedSolomonEncoder.Encode(bufferArray[i], numECBytesPerBlock, generatorPoly);
            }
            if (vd.NumDataBytes != num5)
            {
                throw new ArgumentException("Data bytes does not match offset");
            }
            BitList list2 = new BitList();
            int num9 = (num == vd.NumECBlocks) ? num2 : num3;
            for (int j = 0; j < num9; j++)
            {
                for (int num11 = 0; num11 < vd.NumECBlocks; num11++)
                {
                    if ((j != num2) || (num11 >= num))
                    {
                        list2.Add(bufferArray[num11][j], 8);
                    }
                }
            }
            for (int k = 0; k < numECBytesPerBlock; k++)
            {
                for (int num13 = 0; num13 < vd.NumECBlocks; num13++)
                {
                    list2.Add(bufferArray2[num13][k], 8);
                }
            }
            if (vd.NumTotalBytes != (list2.Count >> 3))
            {
                throw new ArgumentException(string.Format("total bytes: {0}, actual bits: {1}", vd.NumTotalBytes, list2.Count));
            }
            return list2;
        }
    }
}


namespace Gma.QrCodeNet.Encoding.EncodingRegion
{
    using Gma.QrCodeNet.Encoding;
    using System;
    using System.Runtime.CompilerServices;

    internal static class VersionInformation
    {
        private const int s_LengthDataBits = 6;
        private const int s_LengthECBits = 12;
        private const int s_VersionBCHPoly = 0x1f25;
        private const int s_VIRectangleHeight = 3;
        private const int s_VIRectangleWidth = 6;

        internal static void EmbedVersionInformation(this TriStateMatrix tsMatrix, int version)
        {
            if (version >= 7)
            {
                BitList list = VersionInfoBitList(version);
                int width = tsMatrix.Width;
                int num2 = 11;
                int num3 = 0x11;
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        bool flag = list[num3];
                        num3--;
                        tsMatrix[i, (width - num2) + j, MatrixStatus.NoMask] = flag;
                        tsMatrix[(width - num2) + j, i, MatrixStatus.NoMask] = flag;
                    }
                }
            }
        }

        private static BitList VersionInfoBitList(int version)
        {
            BitList list = new BitList();
            list.Add(version, 6);
            list.Add(BCHCalculator.CalculateBCH(version, 0x1f25), 12);
            if (list.Count != 0x12)
            {
                throw new Exception("Version Info creation error. Result is not 18 bits");
            }
            return list;
        }
    }
}


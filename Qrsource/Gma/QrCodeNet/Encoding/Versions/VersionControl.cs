namespace Gma.QrCodeNet.Encoding.Versions
{
    using Gma.QrCodeNet.Encoding;
    using Gma.QrCodeNet.Encoding.DataEncodation;
    using System;

    internal static class VersionControl
    {
        private const string DEFAULT_ENCODING = "iso-8859-1";
        private const int NUM_BITS_MODE_INDICATOR = 4;
        private static readonly int[] VERSION_GROUP = new int[] { 9, 0x1a, 40 };

        private static int BinarySearch(int numDataBits, ErrorCorrectionLevel level, int lowerVersionNum, int higherVersionNum)
        {
            while (lowerVersionNum <= higherVersionNum)
            {
                int versionNum = (lowerVersionNum + higherVersionNum) / 2;
                QRCodeVersion versionByNum = VersionTable.GetVersionByNum(versionNum);
                int numErrorCorrectionCodewards = versionByNum.GetECBlocksByLevel(level).NumErrorCorrectionCodewards;
                int num3 = versionByNum.TotalCodewords - numErrorCorrectionCodewards;
                if ((num3 << 3) == numDataBits)
                {
                    return versionNum;
                }
                if ((num3 << 3) > numDataBits)
                {
                    higherVersionNum = versionNum - 1;
                }
                else
                {
                    lowerVersionNum = versionNum + 1;
                }
            }
            return lowerVersionNum;
        }

        private static int DynamicSearchIndicator(int numBits, ErrorCorrectionLevel level, Mode mode)
        {
            int[] charCountIndicatorSet = CharCountIndicatorTable.GetCharCountIndicatorSet(mode);
            int num = 0;
            int length = VERSION_GROUP.Length;
            for (int i = 0; i < length; i++)
            {
                num = (numBits + 4) + charCountIndicatorSet[i];
                QRCodeVersion versionByNum = VersionTable.GetVersionByNum(VERSION_GROUP[i]);
                int numErrorCorrectionCodewards = versionByNum.GetECBlocksByLevel(level).NumErrorCorrectionCodewards;
                int num5 = versionByNum.TotalCodewords - numErrorCorrectionCodewards;
                if (num <= (num5 * 8))
                {
                    return i;
                }
            }
            throw new InputOutOfBoundaryException(string.Format("QRCode do not have enough space for {0} bits", (numBits + 4) + charCountIndicatorSet[2]));
        }

        private static VersionControlStruct FillVCStruct(int versionNum, ErrorCorrectionLevel level, string encodingName)
        {
            if ((versionNum < 1) || (versionNum > 40))
            {
                throw new InvalidOperationException(string.Format("Unexpected version number: {0}", versionNum));
            }
            VersionControlStruct struct2 = new VersionControlStruct();
            int num = versionNum;
            QRCodeVersion versionByNum = VersionTable.GetVersionByNum(versionNum);
            int totalCodewords = versionByNum.TotalCodewords;
            ErrorCorrectionBlocks eCBlocksByLevel = versionByNum.GetECBlocksByLevel(level);
            int numDataBytes = totalCodewords - eCBlocksByLevel.NumErrorCorrectionCodewards;
            int numBlocks = eCBlocksByLevel.NumBlocks;
            VersionDetail detail = new VersionDetail(num, totalCodewords, numDataBytes, numBlocks);
            struct2.VersionDetail = detail;
            return struct2;
        }

        internal static VersionControlStruct InitialSetup(int dataBitsLength, Mode mode, ErrorCorrectionLevel level, string encodingName)
        {
            int numBits = dataBitsLength;
            bool flag = false;
            BitList eCIHeader = new BitList();
            if (((mode == Mode.EightBitByte) && (encodingName != "iso-8859-1")) && (encodingName != "utf-8"))
            {
                ECISet set = new ECISet(ECISet.AppendOption.NameToValue);
                int eCIValueByName = set.GetECIValueByName(encodingName);
                numBits += ECISet.NumOfECIHeaderBits(eCIValueByName);
                eCIHeader = set.GetECIHeader(encodingName);
                flag = true;
            }
            int index = DynamicSearchIndicator(numBits, level, mode);
            int[] charCountIndicatorSet = CharCountIndicatorTable.GetCharCountIndicatorSet(mode);
            numBits += 4 + charCountIndicatorSet[index];
            int lowerVersionNum = (index == 0) ? 1 : (VERSION_GROUP[index - 1] + 1);
            int higherVersionNum = VERSION_GROUP[index];
            VersionControlStruct struct2 = FillVCStruct(BinarySearch(numBits, level, lowerVersionNum, higherVersionNum), level, encodingName);
            struct2.isContainECI = flag;
            struct2.ECIHeader = eCIHeader;
            return struct2;
        }
    }
}


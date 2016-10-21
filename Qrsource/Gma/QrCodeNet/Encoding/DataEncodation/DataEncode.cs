namespace Gma.QrCodeNet.Encoding.DataEncodation
{
    using Gma.QrCodeNet.Encoding;
    using Gma.QrCodeNet.Encoding.DataEncodation.InputRecognition;
    using Gma.QrCodeNet.Encoding.Terminate;
    using Gma.QrCodeNet.Encoding.Versions;
    using System;
    using System.Collections.Generic;

    /*
     * 根据编码类型，纠错级别，返回编码后的结构体
     */
    internal static class DataEncode
    {
        private static EncoderBase CreateEncoder(Mode mode, string encodingName)
        {
            switch (mode)
            {
                case Mode.Numeric://数字模式
                    return new NumericEncoder();

                case Mode.Alphanumeric://字母数字模式
                    return new AlphanumericEncoder();

                case Mode.EightBitByte://8位字节模式，可以针对汉字编码
                    return new EightBitByteEncoder(encodingName);

                case Mode.Kanji://日文汉字模式
                    return new KanjiEncoder();
            }
            throw new ArgumentOutOfRangeException("mode", string.Format("Doesn't contain encoder for {0}", mode));
        }

        internal static EncodationStruct Encode(IEnumerable<byte> content, ErrorCorrectionLevel eclevel)
        {
            EncoderBase base2 = CreateEncoder(Mode.EightBitByte, "iso-8859-1");
            BitList items = new BitList(content);
            int count = items.Count;
            VersionControlStruct vcStruct = VersionControl.InitialSetup(count, Mode.EightBitByte, eclevel, "iso-8859-1");
            BitList baseList = new BitList();
            if (vcStruct.isContainECI && (vcStruct.ECIHeader != null))
            {
                baseList.Add(vcStruct.ECIHeader);
            }
            baseList.Add(base2.GetModeIndicator());
            int characterCount = count >> 3;
            baseList.Add(base2.GetCharCountIndicator(characterCount, vcStruct.VersionDetail.Version));
            baseList.Add(items);
            baseList.TerminateBites(baseList.Count, vcStruct.VersionDetail.NumDataBytes);
            int num3 = baseList.Count;
            if ((num3 & 7) != 0)
            {
                throw new ArgumentException("data codewords is not byte sized.");
            }
            if ((num3 >> 3) != vcStruct.VersionDetail.NumDataBytes)
            {
                throw new ArgumentException("datacodewords num of bytes not equal to NumDataBytes for current version");
            }
            return new EncodationStruct(vcStruct) { Mode = Mode.EightBitByte, DataCodewords = baseList };
        }

        //返回编码模式的结构体，包含版本信息和模式
        internal static EncodationStruct Encode(string content, ErrorCorrectionLevel ecLevel)
        {
            RecognitionStruct struct2 = InputRecognise.Recognise(content);//找到content的编码方式和模式
            EncoderBase base2 = CreateEncoder(struct2.Mode, struct2.EncodingName);
            BitList dataBits = base2.GetDataBits(content);
            int count = dataBits.Count;
            VersionControlStruct vcStruct = VersionControl.InitialSetup(count, struct2.Mode, ecLevel, struct2.EncodingName);
            BitList baseList = new BitList();
            if (vcStruct.isContainECI && (vcStruct.ECIHeader != null))
            {
                baseList.Add(vcStruct.ECIHeader);
            }
            baseList.Add(base2.GetModeIndicator());
            int characterCount = (struct2.Mode == Mode.EightBitByte) ? (count >> 3) : content.Length;
            baseList.Add(base2.GetCharCountIndicator(characterCount, vcStruct.VersionDetail.Version));
            baseList.Add(dataBits);
            baseList.TerminateBites(baseList.Count, vcStruct.VersionDetail.NumDataBytes);
            int num3 = baseList.Count;
            if ((num3 & 7) != 0)
            {
                throw new ArgumentException("data codewords is not byte sized.");
            }
            if ((num3 >> 3) != vcStruct.VersionDetail.NumDataBytes)
            {
                throw new ArgumentException("datacodewords num of bytes not equal to NumDataBytes for current version");
            }
            return new EncodationStruct(vcStruct) { Mode = struct2.Mode, DataCodewords = baseList };
        }
    }
}


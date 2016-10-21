namespace Gma.QrCodeNet.Encoding
{
    using Gma.QrCodeNet.Encoding.DataEncodation;
    using Gma.QrCodeNet.Encoding.EncodingRegion;
    using Gma.QrCodeNet.Encoding.ErrorCorrection;
    using Gma.QrCodeNet.Encoding.Masking;
    using Gma.QrCodeNet.Encoding.Masking.Scoring;
    using Gma.QrCodeNet.Encoding.Positioning;
    using System;
    using System.Collections.Generic;

    /*
     * 返回的是BitMatrix 位矩阵  供QRCode实体使用
     */
    internal static class QRCodeEncode
    {
        internal static BitMatrix Encode(IEnumerable<byte> content, ErrorCorrectionLevel errorLevel)
        {
            return ProcessEncodationResult(DataEncode.Encode(content, errorLevel), errorLevel);
        }

        internal static BitMatrix Encode(string content, ErrorCorrectionLevel errorLevel)
        {
            return ProcessEncodationResult(DataEncode.Encode(content, errorLevel), errorLevel);
        }

        /*
         * 生成的位矩阵入口
         */
        private static BitMatrix ProcessEncodationResult(EncodationStruct encodeStruct, ErrorCorrectionLevel errorLevel)
        {
            BitList codewords = ECGenerator.FillECCodewords(encodeStruct.DataCodewords, encodeStruct.VersionDetail);
            TriStateMatrix matrix = new TriStateMatrix(encodeStruct.VersionDetail.MatrixWidth);
            PositioninngPatternBuilder.EmbedBasicPatterns(encodeStruct.VersionDetail.Version, matrix);
            matrix.EmbedVersionInformation(encodeStruct.VersionDetail.Version);
            matrix.EmbedFormatInformation(errorLevel, new Pattern0());
            matrix.TryEmbedCodewords(codewords);
            return matrix.GetLowestPenaltyMatrix(errorLevel);
        }
    }
}


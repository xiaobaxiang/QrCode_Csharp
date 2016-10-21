namespace Gma.QrCodeNet.Encoding.DataEncodation.InputRecognition
{
    using Gma.QrCodeNet.Encoding.DataEncodation;
    using System;
    using System.Collections.Generic;
    /*
     * 对content分辨字符编码类型
     */
    public static class InputRecognise
    {
        private static string EightBitByteRecognision(string content, int startPos, int contentLength)
        {
            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentNullException("content", "Input content is null or empty");
            }
            Dictionary<string, int> eCITable = new ECISet(ECISet.AppendOption.NameToValue).GetECITable();
            eCITable.Remove("utf-8");
            eCITable.Remove("iso-8859-1");
            int num = startPos;
            num = ModeEncodeCheck.TryEncodeEightBitByte(content, "iso-8859-1", num, contentLength);
            if (num == -1)
            {
                return "iso-8859-1";
            }
            foreach (KeyValuePair<string, int> pair in eCITable)
            {
                num = ModeEncodeCheck.TryEncodeEightBitByte(content, pair.Key, num, contentLength);
                if (num == -1)
                {
                    return pair.Key;
                }
            }
            if (num == -1)
            {
                throw new ArgumentException("foreach Loop check give wrong result.");
            }
            return "utf-8";
        }

        public static RecognitionStruct Recognise(string content)//识别编码模式
        {
            //参考http://blog.csdn.net/longteng1116/article/details/12753541
            int length = content.Length;
            switch (ModeEncodeCheck.TryEncodeKanji(content, length))
            {
                case -2:
                    /*
                     * 字节编码，可以是0-255的ISO-8859-1字符。
                     * 有些二维码的扫描器可以自动检测是否是UTF-8的编码。
                     */
                    return new RecognitionStruct(Mode.EightBitByte, "utf-8");

                case -1:
                    /*
                     * 日文编码，也是双字节编码。
                     * 同样，也可以用于中文编码。日文和汉字的编码会减去一个值。
                     * 如：在0X8140 to 0X9FFC中的字符会减去8140，
                     * 在0XE040到0XEBBF中的字符要减去0XC140，
                     * 然后把前两位拿出来乘以0XC0，然后再加上后两位，最后转成13bit的编码
                     */
                    return new RecognitionStruct(Mode.Kanji, "iso-8859-1");
            }
            int startPos = ModeEncodeCheck.TryEncodeAlphaNum(content, 0, length);
            switch (startPos)
            {
                case -2:
                    /*
                     * 数字编码，从0到9。
                     * 如果需要编码的数字的个数不是3的倍数，
                     * 那么，最后剩下的1或2位数会被转成4或7bits，
                     * 则其它的每3位数字会被编成 10，12，14bits，
                     * 编成多长还要看二维码的尺寸
                     */
                    return new RecognitionStruct(Mode.Numeric, "iso-8859-1");

                case -1:
                    /*
                     * 字符编码。包括 0-9，大写的A到Z（没有小写），以及符号$ % * + – . / : 包括空格。
                     * 这些字符会映射成一个字符索引表。
                     * 如下所示：（其中的SP是空格，Char是字符，Value是其索引值）
                     * 编码的过程是把字符两两分组，然后转成上面字符的45进制，然后转成11bits的二进制，
                     * 如果最后有一个落单的，那就转成6bits的二进制。
                     * 而编码模式和 字符的个数需要根据不同的Version尺寸编成9, 11或13个二进制
                     */
                    return new RecognitionStruct(Mode.Alphanumeric, "iso-8859-1");
            }
            return new RecognitionStruct(Mode.EightBitByte, EightBitByteRecognision(content, startPos, length));
        }
    }
}


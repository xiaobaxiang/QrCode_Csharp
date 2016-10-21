namespace Gma.QrCodeNet.Encoding.DataEncodation.InputRecognition
{
    using Gma.QrCodeNet.Encoding.DataEncodation;
    using System;
    using System.Text;

    public static class ModeEncodeCheck
    {
        private const int QUESTION_MARK_CHAR = 0x3f;

        private static bool AlphaNumCheck(string content)
        {
            if (TryEncodeAlphaNum(content, 0, content.Length) != -1)
            {
                return false;
            }
            return true;
        }

        private static bool EightBitByteCheck(string encodingName, string content)
        {
            if (TryEncodeEightBitByte(content, encodingName, 0, content.Length) != -1)
            {
                return false;
            }
            return true;
        }

        public static bool isModeEncodeValid(Mode mode, string encoding, string content)
        {
            switch (mode)
            {
                case Mode.Numeric:
                    return NumericCheck(content);

                case Mode.Alphanumeric:
                    return AlphaNumCheck(content);

                case Mode.EightBitByte:
                    return EightBitByteCheck(encoding, content);

                case Mode.Kanji:
                    return KanjiCheck(content);
            }
            throw new InvalidOperationException(string.Format("System does not contain mode: {0}", mode.ToString()));
        }

        private static bool KanjiCheck(string content)
        {
            if (TryEncodeKanji(content, content.Length) != -1)
            {
                return false;
            }
            return true;
        }

        private static bool NumericCheck(string content)
        {
            if (TryEncodeAlphaNum(content, 0, content.Length) != -2)
            {
                return false;
            }
            return true;
        }

        internal static int TryEncodeAlphaNum(string content, int startPos, int contentLength)
        {
            if (string.IsNullOrEmpty(content))
            {
                throw new IndexOutOfRangeException("Input content should not be Null or empty");
            }
            bool flag = true;
            int num = 0;
            for (int i = startPos; i < contentLength; i++)
            {
                if (flag)
                {
                    num = content[i] - '0';
                    if ((num < 0) || (num > 9))
                    {
                        flag = false;
                    }
                }
                if (!flag && !AlphanumericTable.Contains(content[i]))
                {
                    return i;
                }
            }
            if (!flag)
            {
                return -1;
            }
            return -2;
        }

        internal static int TryEncodeEightBitByte(string content, string encodingName, int startPos, int contentLength)
        {
            Encoding encoding;
            byte[] bytes;
            if (string.IsNullOrEmpty(content))
            {
                throw new IndexOutOfRangeException("Input content should not be Null or empty");
            }
            try
            {
                encoding = Encoding.GetEncoding(encodingName);
            }
            catch (ArgumentException)
            {
                return startPos;
            }
            char[] chars = new char[1];
            for (int i = startPos; i < contentLength; i++)
            {
                chars[0] = content[i];
                bytes = encoding.GetBytes(chars);
                int length = bytes.Length;
                if (((chars[0] != '?') && (length == 1)) && (bytes[0] == 0x3f))
                {
                    return i;
                }
                if (length > 1)
                {
                    return i;
                }
            }
            for (int j = 0; j < startPos; j++)
            {
                chars[0] = content[j];
                bytes = encoding.GetBytes(chars);
                int num4 = bytes.Length;
                if (((chars[0] != '?') && (num4 == 1)) && (bytes[0] == 0x3f))
                {
                    return j;
                }
                if (num4 > 1)
                {
                    return j;
                }
            }
            return -1;
        }

        internal static int TryEncodeKanji(string content, int contentLength)
        {
            Encoding encoding;
            if (string.IsNullOrEmpty(content))
            {
                throw new IndexOutOfRangeException("Input content should not be Null or empty");
            }
            try
            {
                encoding = Encoding.GetEncoding("Shift_JIS");//尝试日本汉字
            }
            catch (ArgumentException)
            {
                return 0;
            }
            char[] chars = new char[1];
            for (int i = 0; i < contentLength; i++)
            {
                chars[0] = content[i];
                byte[] bytes = encoding.GetBytes(chars);
                int length = bytes.Length;
                byte num3 = bytes[0];
                if (length != 2)
                {
                    if (i != 0)
                    {
                        return -2;
                    }
                    return 0;
                }
                if (((num3 < 0x81) || (num3 > 0x9f)) && ((num3 < 0xe0) || (num3 > 0xeb)))
                {
                    if (i != 0)
                    {
                        return -2;
                    }
                    return 0;
                }
            }
            return -1;
        }
    }
}


namespace Gma.QrCodeNet.Encoding.DataEncodation
{
    using System;

    public static class CharCountIndicatorTable
    {
        public static int GetBitCountInCharCountIndicator(Mode mode, int version)
        {
            int[] charCountIndicatorSet = GetCharCountIndicatorSet(mode);
            int versionGroup = GetVersionGroup(version);
            return charCountIndicatorSet[versionGroup];
        }

        public static int[] GetCharCountIndicatorSet(Mode mode)
        {
            switch (mode)
            {
                case Mode.Numeric:
                    return new int[] { 10, 12, 14 };

                case Mode.Alphanumeric:
                    return new int[] { 9, 11, 13 };

                case Mode.EightBitByte:
                    return new int[] { 8, 0x10, 0x10 };

                case Mode.Kanji:
                    return new int[] { 8, 10, 12 };
            }
            throw new InvalidOperationException(string.Format("Unexpected Mode: {0}", mode.ToString()));
        }

        private static int GetVersionGroup(int version)
        {
            if (version > 40)
            {
                throw new InvalidOperationException(string.Format("Unexpected version: {0}", version));
            }
            if (version >= 0x1b)
            {
                return 2;
            }
            if (version >= 10)
            {
                return 1;
            }
            if (version <= 0)
            {
                throw new InvalidOperationException(string.Format("Unexpected version: {0}", version));
            }
            return 0;
        }
    }
}


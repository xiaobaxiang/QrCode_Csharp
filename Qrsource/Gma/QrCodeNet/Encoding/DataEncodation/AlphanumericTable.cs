namespace Gma.QrCodeNet.Encoding.DataEncodation
{
    using System;
    using System.Collections.Generic;

    internal class AlphanumericTable
    {
        private static readonly Dictionary<char, int> s_AlphanumericTable;

        static AlphanumericTable()
        {
            Dictionary<char, int> dictionary = new Dictionary<char, int>();
            dictionary.Add('0', 0);
            dictionary.Add('1', 1);
            dictionary.Add('2', 2);
            dictionary.Add('3', 3);
            dictionary.Add('4', 4);
            dictionary.Add('5', 5);
            dictionary.Add('6', 6);
            dictionary.Add('7', 7);
            dictionary.Add('8', 8);
            dictionary.Add('9', 9);
            dictionary.Add('A', 10);
            dictionary.Add('B', 11);
            dictionary.Add('C', 12);
            dictionary.Add('D', 13);
            dictionary.Add('E', 14);
            dictionary.Add('F', 15);
            dictionary.Add('G', 0x10);
            dictionary.Add('H', 0x11);
            dictionary.Add('I', 0x12);
            dictionary.Add('J', 0x13);
            dictionary.Add('K', 20);
            dictionary.Add('L', 0x15);
            dictionary.Add('M', 0x16);
            dictionary.Add('N', 0x17);
            dictionary.Add('O', 0x18);
            dictionary.Add('P', 0x19);
            dictionary.Add('Q', 0x1a);
            dictionary.Add('R', 0x1b);
            dictionary.Add('S', 0x1c);
            dictionary.Add('T', 0x1d);
            dictionary.Add('U', 30);
            dictionary.Add('V', 0x1f);
            dictionary.Add('W', 0x20);
            dictionary.Add('X', 0x21);
            dictionary.Add('Y', 0x22);
            dictionary.Add('Z', 0x23);
            dictionary.Add(' ', 0x24);
            dictionary.Add('$', 0x25);
            dictionary.Add('%', 0x26);
            dictionary.Add('*', 0x27);
            dictionary.Add('+', 40);
            dictionary.Add('-', 0x29);
            dictionary.Add('.', 0x2a);
            dictionary.Add('/', 0x2b);
            dictionary.Add(':', 0x2c);
            s_AlphanumericTable = dictionary;
        }

        internal static bool Contains(char inputChar)
        {
            return s_AlphanumericTable.ContainsKey(inputChar);
        }

        internal static int ConvertAlphaNumChar(char inputChar)
        {
            int num;
            if (!s_AlphanumericTable.TryGetValue(inputChar, out num))
            {
                throw new ArgumentOutOfRangeException("inputChar", "Not an alphanumeric character found. Only characters from table from chapter 8.4.3 P21 are supported in alphanumeric mode.");
            }
            return num;
        }
    }
}


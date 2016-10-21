namespace Gma.QrCodeNet.Encoding.DataEncodation
{
    using Gma.QrCodeNet.Encoding;
    using System;
    using System.Collections.Generic;

    public sealed class ECISet
    {
        private const int ECIIndicatorNumBits = 4;
        private const int ECIMode = 7;
        private Dictionary<string, int> s_NameToValue;
        private Dictionary<int, string> s_ValueToName;

        internal ECISet(AppendOption option)
        {
            this.Initialize(option);
        }

        private void AppendECI(string name, int value, AppendOption option)
        {
            switch (option)
            {
                case AppendOption.NameToValue:
                    this.s_NameToValue.Add(name, value);
                    return;

                case AppendOption.ValueToName:
                    this.s_ValueToName.Add(value, name);
                    return;

                case AppendOption.Both:
                    this.s_NameToValue.Add(name, value);
                    this.s_ValueToName.Add(value, name);
                    return;
            }
            throw new InvalidOperationException("There is no such AppendOption");
        }

        public bool ContainsECIName(string encodingName)
        {
            if (this.s_NameToValue == null)
            {
                this.Initialize(AppendOption.NameToValue);
            }
            return this.s_NameToValue.ContainsKey(encodingName);
        }

        public bool ContainsECIValue(int eciValue)
        {
            if (this.s_ValueToName == null)
            {
                this.Initialize(AppendOption.ValueToName);
            }
            return this.s_ValueToName.ContainsKey(eciValue);
        }

        internal BitList GetECIHeader(string encodingName)
        {
            int eCIValueByName = this.GetECIValueByName(encodingName);
            BitList list = new BitList();
            list.Add(7, 4);
            int num2 = NumOfCodewords(eCIValueByName);
            int bitCount = 0;
            switch (num2)
            {
                case 1:
                    list.Add(0, 1);
                    bitCount = (num2 * 8) - 1;
                    break;

                case 2:
                    list.Add(2, 2);
                    bitCount = (num2 * 8) - 2;
                    break;

                case 3:
                    list.Add(6, 3);
                    bitCount = (num2 * 8) - 3;
                    break;

                default:
                    throw new InvalidOperationException("Assignment Codewords should be either 1, 2 or 3");
            }
            list.Add(eCIValueByName, bitCount);
            return list;
        }

        internal string GetECINameByValue(int ECIValue)
        {
            string str;
            if (this.s_ValueToName == null)
            {
                this.Initialize(AppendOption.ValueToName);
            }
            if (!this.s_ValueToName.TryGetValue(ECIValue, out str))
            {
                throw new ArgumentOutOfRangeException(string.Format("ECI doesn't contain value: {0}", ECIValue));
            }
            return str;
        }

        public Dictionary<string, int> GetECITable()
        {
            if (this.s_NameToValue == null)
            {
                this.Initialize(AppendOption.NameToValue);
            }
            return this.s_NameToValue;
        }

        internal int GetECIValueByName(string encodingName)
        {
            int num;
            if (this.s_NameToValue == null)
            {
                this.Initialize(AppendOption.NameToValue);
            }
            if (!this.s_NameToValue.TryGetValue(encodingName, out num))
            {
                throw new ArgumentOutOfRangeException(string.Format("ECI doesn't contain encoding: {0}", encodingName));
            }
            return num;
        }

        private void Initialize(AppendOption option)
        {
            switch (option)
            {
                case AppendOption.NameToValue:
                    this.s_NameToValue = new Dictionary<string, int>();
                    break;

                case AppendOption.ValueToName:
                    this.s_ValueToName = new Dictionary<int, string>();
                    break;

                case AppendOption.Both:
                    this.s_NameToValue = new Dictionary<string, int>();
                    this.s_ValueToName = new Dictionary<int, string>();
                    break;

                default:
                    throw new InvalidOperationException("There is no such AppendOption");
            }
            this.AppendECI("iso-8859-1", 1, option);
            this.AppendECI("IBM437", 2, option);
            this.AppendECI("iso-8859-2", 4, option);
            this.AppendECI("iso-8859-3", 5, option);
            this.AppendECI("iso-8859-4", 6, option);
            this.AppendECI("iso-8859-5", 7, option);
            this.AppendECI("iso-8859-6", 8, option);
            this.AppendECI("iso-8859-7", 9, option);
            this.AppendECI("iso-8859-8", 10, option);
            this.AppendECI("iso-8859-9", 11, option);
            this.AppendECI("windows-874", 13, option);
            this.AppendECI("iso-8859-13", 15, option);
            this.AppendECI("iso-8859-15", 0x11, option);
            this.AppendECI("shift_jis", 20, option);
            this.AppendECI("utf-8", 0x1a, option);
        }

        private static int NumOfAssignmentBits(int ECIValue)
        {
            return (NumOfCodewords(ECIValue) * 8);
        }

        private static int NumOfCodewords(int ECIValue)
        {
            if ((ECIValue >= 0) && (ECIValue <= 0x7f))
            {
                return 1;
            }
            if ((ECIValue > 0x7f) && (ECIValue <= 0x3fff))
            {
                return 2;
            }
            if ((ECIValue <= 0x3fff) || (ECIValue > 0xf423f))
            {
                throw new ArgumentOutOfRangeException("ECIValue should be in range: 0 to 999999");
            }
            return 3;
        }

        internal static int NumOfECIHeaderBits(int ECIValue)
        {
            return (NumOfAssignmentBits(ECIValue) + 4);
        }

        public enum AppendOption
        {
            NameToValue,
            ValueToName,
            Both
        }

        private enum ECICodewordsLength
        {
            one = 0,
            three = 6,
            two = 2
        }
    }
}


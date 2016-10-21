namespace Gma.QrCodeNet.Encoding
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal sealed class BitList : IEnumerable<bool>, IEnumerable
    {
        private readonly List<byte> m_Array;
        private int m_BitsSize;

        internal BitList()
        {
            this.m_BitsSize = 0;
            this.m_Array = new List<byte>(0x20);
        }

        internal BitList(IEnumerable<byte> byteArray)
        {
            this.m_BitsSize = byteArray.Count<byte>();
            this.m_Array = byteArray.ToList<byte>();
        }

        internal void Add(bool item)
        {
            List<byte> list;
            int num2;
            int num = this.m_BitsSize & 7;
            if (num == 0)
            {
                this.m_Array.Add(0);
            }
            (list = this.m_Array)[num2 = this.m_BitsSize >> 3] = Byte.Parse((list[num2] | this.ToBit(item) << (7 - num)).ToString());
            this.m_BitsSize++;
        }

        internal void Add(IEnumerable<bool> items)
        {
            foreach (bool flag in items)
            {
                this.Add(flag);
            }
        }

        internal void Add(int value, int bitCount)
        {
            if ((bitCount < 0) || (bitCount > 0x20))
            {
                throw new ArgumentOutOfRangeException("bitCount", "bitCount must greater or equal to 0");
            }
            int num = bitCount;
            while (num > 0)
            {
                if (((this.m_BitsSize & 7) == 0) && (num >= 8))
                {
                    byte item = (byte) ((value >> (num - 8)) & 0xff);
                    this.appendByte(item);
                    num -= 8;
                }
                else
                {
                    bool flag = ((value >> (num - 1)) & 1) == 1;
                    this.Add(flag);
                    num--;
                }
            }
        }

        private void appendByte(byte item)
        {
            this.m_Array.Add(item);
            this.m_BitsSize += 8;
        }

        public IEnumerator<bool> GetEnumerator()
        {
            byte iteratorVariable2;
            int iteratorVariable0 = this.m_BitsSize >> 3;
            int iteratorVariable1 = this.m_BitsSize & 7;
            for (int i = 0; i < iteratorVariable0; i++)
            {
                iteratorVariable2 = this.m_Array[i];
                for (int j = 7; j >= 0; j--)
                {
                    yield return (((iteratorVariable2 >> j) & 1) == 1);
                }
            }
            if (iteratorVariable1 > 0)
            {
                iteratorVariable2 = this.m_Array[iteratorVariable0];
                for (int k = 0; k < iteratorVariable1; k++)
                {
                    yield return (((iteratorVariable2 >> (7 - k)) & 1) == 1);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private int ToBit(bool item)
        {
            if (!item)
            {
                return 0;
            }
            return 1;
        }

        internal int Count
        {
            get
            {
                return this.m_BitsSize;
            }
        }

        internal bool this[int index]
        {
            get
            {
                if ((index < 0) || (index >= this.m_BitsSize))
                {
                    throw new ArgumentOutOfRangeException("index", "Index out of range");
                }
                int num = this.m_Array[index >> 3] & 0xff;
                return (((num >> (7 - (index & 7))) & 1) == 1);
            }
        }

        internal List<byte> List
        {
            get
            {
                return this.m_Array;
            }
        }

        internal int SizeInByte
        {
            get
            {
                return ((this.m_BitsSize + 7) >> 3);
            }
        }

    }
}


namespace Gma.QrCodeNet.Encoding.ReedSolomon
{
    using System;

    internal sealed class GaloisField256
    {
        private int[] antiLogTable = new int[0x100];
        private int[] logTable = new int[0x100];
        private readonly int m_primitive;

        internal GaloisField256(int primitive)
        {
            this.m_primitive = primitive;
            int index = 1;
            for (int i = 0; i < 0x100; i++)
            {
                this.antiLogTable[i] = index;
                if (i != 0xff)
                {
                    this.logTable[index] = i;
                }
                index = index << 1;
                if (index > 0xff)
                {
                    index ^= primitive;
                }
            }
        }

        internal int Addition(int gfValueA, int gfValueB)
        {
            return (gfValueA ^ gfValueB);
        }

        internal int Exponent(int PowersOfa)
        {
            return this.antiLogTable[PowersOfa];
        }

        internal int inverse(int gfValue)
        {
            if (gfValue == 0)
            {
                throw new ArgumentException("GaloisField value will not equal to 0, Inverse method");
            }
            return this.Exponent(0xff - this.Log(gfValue));
        }

        internal int Log(int gfValue)
        {
            if (gfValue == 0)
            {
                throw new ArgumentException("GaloisField value will not equal to 0, Log method");
            }
            return this.logTable[gfValue];
        }

        internal int Product(int gfValueA, int gfValueB)
        {
            if ((gfValueA == 0) || (gfValueB == 0))
            {
                return 0;
            }
            if (gfValueA == 1)
            {
                return gfValueB;
            }
            if (gfValueB == 1)
            {
                return gfValueA;
            }
            return this.Exponent((this.Log(gfValueA) + this.Log(gfValueB)) % 0xff);
        }

        internal int Quotient(int gfValueA, int gfValueB)
        {
            if (gfValueA == 0)
            {
                return 0;
            }
            if (gfValueB == 0)
            {
                throw new ArgumentException("gfValueB can not be zero");
            }
            if (gfValueB == 1)
            {
                return gfValueA;
            }
            return this.Exponent(Math.Abs((int) (this.Log(gfValueA) - this.Log(gfValueB))) % 0xff);
        }

        internal int Subtraction(int gfValueA, int gfValueB)
        {
            return this.Addition(gfValueA, gfValueB);
        }

        internal int Primitive
        {
            get
            {
                return this.m_primitive;
            }
        }

        internal static GaloisField256 QRCodeGaloisField
        {
            get
            {
                return new GaloisField256(0x11d);
            }
        }
    }
}


namespace Gma.QrCodeNet.Encoding.ReedSolomon
{
    using System;

    internal sealed class Polynomial
    {
        private readonly int[] m_Coefficients;
        private readonly GaloisField256 m_GField;
        private readonly int m_primitive;

        internal Polynomial(GaloisField256 gfield, int[] coefficients)
        {
            int length = coefficients.Length;
            if ((length == 0) || (coefficients == null))
            {
                throw new ArithmeticException("Can not create empty Polynomial");
            }
            this.m_GField = gfield;
            this.m_primitive = gfield.Primitive;
            if ((length > 1) && (coefficients[0] == 0))
            {
                int index = 1;
                while ((index < length) && (coefficients[index] == 0))
                {
                    index++;
                }
                if (index == length)
                {
                    this.m_Coefficients = new int[1];
                }
                else
                {
                    int num3 = length - index;
                    this.m_Coefficients = new int[num3];
                    Array.Copy(coefficients, index, this.m_Coefficients, 0, num3);
                }
            }
            else
            {
                this.m_Coefficients = new int[length];
                Array.Copy(coefficients, this.m_Coefficients, length);
            }
        }

        internal Polynomial AddOrSubtract(Polynomial other)
        {
            if (this.Primitive != other.Primitive)
            {
                throw new ArgumentException("Polynomial can not perform AddOrSubtract as they don't have same Primitive for GaloisField256");
            }
            if (this.isMonomialZero)
            {
                return other;
            }
            if (other.isMonomialZero)
            {
                return this;
            }
            int length = other.Coefficients.Length;
            int num2 = this.Coefficients.Length;
            if (length > num2)
            {
                return this.CoefficientXor(this.Coefficients, other.Coefficients);
            }
            return this.CoefficientXor(other.Coefficients, this.Coefficients);
        }

        internal Polynomial CoefficientXor(int[] smallerCoefficients, int[] largerCoefficients)
        {
            if (smallerCoefficients.Length > largerCoefficients.Length)
            {
                throw new ArgumentException("Can not perform CoefficientXor method as smaller Coefficients length is larger than larger one.");
            }
            int length = largerCoefficients.Length;
            int[] destinationArray = new int[length];
            int num2 = largerCoefficients.Length - smallerCoefficients.Length;
            Array.Copy(largerCoefficients, 0, destinationArray, 0, num2);
            for (int i = num2; i < length; i++)
            {
                destinationArray[i] = this.GField.Addition(largerCoefficients[i], smallerCoefficients[i - num2]);
            }
            return new Polynomial(this.GField, destinationArray);
        }

        internal PolyDivideStruct Divide(Polynomial other)
        {
            if (this.Primitive != other.Primitive)
            {
                throw new ArgumentException("Polynomial can not perform Devide as they don't have same Primitive for GaloisField256");
            }
            if (other.isMonomialZero)
            {
                throw new ArgumentException("Can not devide by Polynomial Zero");
            }
            int length = this.Coefficients.Length;
            int[] destinationArray = new int[length];
            Array.Copy(this.Coefficients, 0, destinationArray, 0, length);
            int num2 = other.Coefficients.Length;
            if (length < num2)
            {
                int[] numArray4 = new int[1];
                return new PolyDivideStruct(new Polynomial(this.GField, numArray4), this);
            }
            int[] coefficients = new int[(length - num2) + 1];
            int coefficient = other.GetCoefficient(other.Degree);
            int gfValueA = this.GField.inverse(coefficient);
            for (int i = 0; i <= (length - num2); i++)
            {
                if (destinationArray[i] != 0)
                {
                    int scalar = this.GField.Product(gfValueA, destinationArray[i]);
                    Polynomial polynomial = other.MultiplyScalar(scalar);
                    coefficients[i] = scalar;
                    int[] numArray3 = polynomial.Coefficients;
                    if (numArray3[0] != 0)
                    {
                        for (int j = 0; j < num2; j++)
                        {
                            destinationArray[i + j] = this.GField.Subtraction(destinationArray[i + j], numArray3[j]);
                        }
                    }
                }
            }
            return new PolyDivideStruct(new Polynomial(this.GField, coefficients), new Polynomial(this.GField, destinationArray));
        }

        internal int GetCoefficient(int degree)
        {
            return this.m_Coefficients[this.m_Coefficients.Length - (degree + 1)];
        }

        internal Polynomial Multiply(Polynomial other)
        {
            if (this.Primitive != other.Primitive)
            {
                throw new ArgumentException("Polynomial can not perform Multiply as they don't have same Primitive for GaloisField256");
            }
            if (this.isMonomialZero || other.isMonomialZero)
            {
                return new Polynomial(this.GField, new int[1]);
            }
            int[] coefficients = this.Coefficients;
            int length = coefficients.Length;
            int[] numArray2 = other.Coefficients;
            int num2 = numArray2.Length;
            int[] numArray3 = new int[(length + num2) - 1];
            for (int i = 0; i < length; i++)
            {
                int gfValueA = coefficients[i];
                for (int j = 0; j < num2; j++)
                {
                    numArray3[i + j] = this.GField.Addition(numArray3[i + j], this.GField.Product(gfValueA, numArray2[j]));
                }
            }
            return new Polynomial(this.GField, numArray3);
        }

        internal Polynomial MultiplyScalar(int scalar)
        {
            if (scalar == 0)
            {
                return new Polynomial(this.GField, new int[1]);
            }
            if (scalar == 1)
            {
                return this;
            }
            int length = this.Coefficients.Length;
            int[] coefficients = new int[length];
            for (int i = 0; i < length; i++)
            {
                coefficients[i] = this.GField.Product(this.Coefficients[i], scalar);
            }
            return new Polynomial(this.GField, coefficients);
        }

        internal int[] Coefficients
        {
            get
            {
                return this.m_Coefficients;
            }
        }

        internal int Degree
        {
            get
            {
                return (this.Coefficients.Length - 1);
            }
        }

        internal GaloisField256 GField
        {
            get
            {
                return this.m_GField;
            }
        }

        internal bool isMonomialZero
        {
            get
            {
                return (this.m_Coefficients[0] == 0);
            }
        }

        internal int Primitive
        {
            get
            {
                return this.m_primitive;
            }
        }
    }
}


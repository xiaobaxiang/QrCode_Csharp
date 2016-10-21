namespace Gma.QrCodeNet.Encoding.ReedSolomon
{
    using System;
    using System.Collections.Generic;

    internal sealed class GeneratorPolynomial
    {
        private List<Polynomial> m_cacheGenerator;
        private readonly GaloisField256 m_gfield;

        internal GeneratorPolynomial(GaloisField256 gfield)
        {
            this.m_gfield = gfield;
            this.m_cacheGenerator = new List<Polynomial>(10);
            this.m_cacheGenerator.Add(new Polynomial(this.m_gfield, new int[] { 1 }));
        }

        private void BuildGenerator(int degree)
        {
            lock (this.m_cacheGenerator)
            {
                int count = this.m_cacheGenerator.Count;
                if (degree >= count)
                {
                    Polynomial polynomial = this.m_cacheGenerator[count - 1];
                    for (int i = count; i <= degree; i++)
                    {
                        Polynomial item = polynomial.Multiply(new Polynomial(this.m_gfield, new int[] { 1, this.m_gfield.Exponent(i - 1) }));
                        this.m_cacheGenerator.Add(item);
                        polynomial = item;
                    }
                }
            }
        }

        internal Polynomial GetGenerator(int degree)
        {
            if (degree >= this.m_cacheGenerator.Count)
            {
                this.BuildGenerator(degree);
            }
            return this.m_cacheGenerator[degree];
        }
    }
}


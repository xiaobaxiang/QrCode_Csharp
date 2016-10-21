namespace Gma.QrCodeNet.Encoding.Masking.Scoring
{
    using Gma.QrCodeNet.Encoding;
    using System;

    public abstract class Penalty
    {
        protected Penalty()
        {
        }

        internal abstract int PenaltyCalculate(BitMatrix matrix);
    }
}


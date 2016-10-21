namespace Gma.QrCodeNet.Encoding.Masking.Scoring
{
    using Gma.QrCodeNet.Encoding;
    using Gma.QrCodeNet.Encoding.Masking;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal static class MatrixScoreCalculator
    {
        internal static BitMatrix GetLowestPenaltyMatrix(this TriStateMatrix matrix, ErrorCorrectionLevel errorlevel)
        {
            PatternFactory factory = new PatternFactory();
            int num = 0x7fffffff;
            TriStateMatrix matrix2 = new TriStateMatrix(matrix.Width);
            foreach (Pattern pattern in factory.AllPatterns())
            {
                TriStateMatrix matrix3 = matrix.Apply(pattern, errorlevel);
                int num2 = matrix3.PenaltyScore();
                if (num2 < num)
                {
                    num = num2;
                    matrix2 = matrix3;
                }
            }
            return matrix2;
        }

        internal static int PenaltyScore(this BitMatrix matrix)
        {
            PenaltyFactory factory = new PenaltyFactory();
            return factory.AllRules().Sum<Penalty>(((Func<Penalty, int>) (penalty => penalty.PenaltyCalculate(matrix))));
        }
    }
}


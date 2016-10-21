namespace Gma.QrCodeNet.Encoding.Masking.Scoring
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;

    internal class PenaltyFactory
    {
        internal IEnumerable<Penalty> AllRules()
        {
            IEnumerator enumerator = Enum.GetValues(typeof(PenaltyRules)).GetEnumerator();
            while (enumerator.MoveNext())
            {
                PenaltyRules current = (PenaltyRules) enumerator.Current;
                yield return this.CreateByRule(current);
            }
        }

        internal Penalty CreateByRule(PenaltyRules penaltyRule)
        {
            switch (penaltyRule)
            {
                case PenaltyRules.Rule01:
                    return new Penalty1();

                case PenaltyRules.Rule02:
                    return new Penalty2();

                case PenaltyRules.Rule03:
                    return new Penalty3();

                case PenaltyRules.Rule04:
                    return new Penalty4();
            }
            throw new ArgumentException(string.Format("Unsupport penalty rule : {0}", penaltyRule), "penaltyRule");
        }

    }
}


namespace Matheparser.Parsing.PostFixExpressions.Binary.Compare
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using Matheparser.Values;

    [DebuggerDisplay("Operator ==")]
    public sealed class EqualExpression : CompareOperatorBase
    {
        internal override bool CompareNumber(double double1, double double2)
        {
            return double1 == double2;
        }

        internal override bool CompareString(string string1, string string2)
        {
            return string1.Equals(string2);
        }

        internal override IValue EvalSet(HashSet<IValue> setA, HashSet<IValue> setB)
        {
            foreach (var item in setA)
            {
                if (!setB.Contains(item))
                {
                    return new DoubleValue(0);
                }
            }

            return new DoubleValue(setA.Count == setB.Count ? 1 : 0);
        }

        public override string ToString()
        {
            return "Op ==";
        }
    }
}

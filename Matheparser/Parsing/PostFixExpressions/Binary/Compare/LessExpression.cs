namespace Matheparser.Parsing.PostFixExpressions.Binary.Compare
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using Matheparser.Values;

    [DebuggerDisplay("Operator <")]
    public sealed class LessExpression : CompareOperatorBase
    {
        internal override bool CompareNumber(double double1, double double2)
        {
            return double1 < double2;
        }

        internal override bool CompareString(string string1, string string2)
        {
            return string1.CompareTo(string2) < 0;
        }

        public override string ToString()
        {
            return "Op <";
        }

        internal override bool CompareSet(HashSet<IValue> setA, HashSet<IValue> setB)
        {
            return this.CompareNumber(setA.Count, setB.Count);
        }
    }
}

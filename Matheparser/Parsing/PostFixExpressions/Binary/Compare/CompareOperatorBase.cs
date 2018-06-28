namespace Matheparser.Parsing.PostFixExpressions.Binary.Compare
{
    using System.Collections.Generic;
    using Matheparser.Values;

    public abstract class CompareOperatorBase : BinaryOperatorExpressionBase
    {
        internal override IValue EvalNumber(double double1, double double2)
        {
            return new DoubleValue(this.CompareNumber(double1, double2) ? 1 : 0);
        }

        internal override IValue EvalString(string string1, string string2)
        {
            return new DoubleValue(this.CompareString(string1, string2) ? 1 : 0);
        }

        internal override IValue EvalSet(HashSet<IValue> setA, HashSet<IValue> setB)
        {
            return new DoubleValue(this.CompareSet(setA, setB) ? 1 : 0);
        }

        internal abstract bool CompareNumber(double double1, double double2);

        internal abstract bool CompareString(string string1, string string2);

        internal abstract bool CompareSet(HashSet<IValue> setA, HashSet<IValue> setB);
    }
}

using System.Collections.Generic;
using System.Diagnostics;
using Matheparser.Values;

namespace Matheparser.Parsing.PostFixExpressions.Binary.Arithmetic
{
    [DebuggerDisplay("Operator +")]
    public sealed class AddExpression : BinaryOperatorExpressionBase
    {
        internal override IValue EvalNumber(double double1, double double2)
        {
            return new DoubleValue(double1 + double2);
        }

        internal override IValue EvalString(string string1, string string2)
        {
            return new StringValue(string.Concat(string1, string2));
        }

        internal override IValue EvalSet(HashSet<IValue> setA, HashSet<IValue> setB)
        {
            var newSet = new HashSet<IValue>(setA);

            foreach (var item in setB)
            {
                if (!newSet.Contains(item))
                {
                    newSet.Add(item);
                }
            }

            return new SetValue(newSet);
        }

        public override string ToString()
        {
            return "+";
        }
    }
}

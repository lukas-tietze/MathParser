namespace Matheparser.Parsing.PostFixExpressions.Binary.Arithmetic
{
    using System;
    using System.Diagnostics;
    using Matheparser.Util;
    using Matheparser.Values;

    [DebuggerDisplay("Operator -")]
    public sealed class SubExpression : BinaryOperatorExpressionBase
    {
        internal override IValue EvalNumber(double double1, double double2)
        {
            return new DoubleValue(double1 - double2);
        }

        internal override IValue EvalString(string string1, string string2)
        {
            throw new InvalidOperationException();
        }

        internal override IValue EvalSet(IArray setA, IArray setB)
        {
            return new ArrayValue(setA.Except(setB));
        }

        public override string ToString()
        {
            return "op -";
        }
    }
}

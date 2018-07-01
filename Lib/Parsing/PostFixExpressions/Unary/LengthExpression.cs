namespace Matheparser.Parsing.PostFixExpressions.Unary
{
    using System;
    using Matheparser.Util;
    using Matheparser.Values;

    public class LengthExpression : UnaryOperatorExpressionBase
    {
        internal override IValue EvalNumber(double operand)
        {
            return new DoubleValue(Math.Abs(operand));
        }

        internal override IValue EvalString(string operand)
        {
            return new DoubleValue(operand.Length);
        }

        internal override IValue EvalSet(IArray operand)
        {
            return new DoubleValue(operand.Count);
        }
    }
}

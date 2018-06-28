using System;
using System.Collections.Generic;
using Matheparser.Values;

namespace Matheparser.Parsing.PostFixExpressions.Unary
{
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

        internal override IValue EvalSet(HashSet<IValue> operand)
        {
            return new DoubleValue(operand.Count);
        }
    }
}

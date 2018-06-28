using System;
using System.Collections.Generic;
using System.Diagnostics;
using Matheparser.Exceptions;
using Matheparser.Values;

namespace Matheparser.Parsing.PostFixExpressions.Unary
{
    [DebuggerDisplay("Operator !")]
    public class NotExpression : UnaryOperatorExpressionBase
    {
        internal override IValue EvalNumber(double operand)
        {
            return new DoubleValue(operand == 0 ? 1 : 0);
        }

        internal override IValue EvalString(string operand)
        {
            throw new InvalidOperationException();
        }

        internal override IValue EvalSet(HashSet<IValue> operand)
        {
            throw new NotSupportedException();
        }

        public override string ToString()
        {
            return "Op !";
        }
    }
}

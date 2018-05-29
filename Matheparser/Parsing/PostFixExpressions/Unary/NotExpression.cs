using System.Diagnostics;
using Matheparser.Parsing.PostFixExpressions.Exceptions;
using Matheparser.Values;

namespace Matheparser.Parsing.PostFixExpressions.Unary
{
    [DebuggerDisplay("Operator !")]
    public class NotExpression : UnaryOperatorExpressionBase
    {
        public override IValue EvalNumber(double operand)
        {
            return new DoubleValue(operand == 0 ? 1 : 0);
        }

        public override IValue EvalStirng(string operand)
        {
            throw new IllegalOperationException();
        }
    }
}

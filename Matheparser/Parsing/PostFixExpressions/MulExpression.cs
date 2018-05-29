using System.Diagnostics;
using Matheparser.Values;

namespace Matheparser.Parsing.PostFixExpressions
{
    [DebuggerDisplay("Operator: *")]
    public sealed class MulExpression : IPostFixExpression
    {
        public PostFixExpressionType Type
        {
            get
            {
                return PostFixExpressionType.Operator;
            }
        }

        public IValue Eval(IValue[] operands)
        {
            if (operands.Length != 2)
            {
                throw new MissingOperandException();
            }

            if (operands[0].Type == ValueType.Number && operands[1].Type == ValueType.Number)
            {
                return new DoubleValue(operands[0].AsDouble * operands[1].AsDouble);
            }

            throw new WrongOperandTypeException();
        }
    }
}

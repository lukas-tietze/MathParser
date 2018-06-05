using System;
using Matheparser.Exceptions;
using Matheparser.Values;

namespace Matheparser.Parsing.PostFixExpressions.Unary
{
    public abstract class UnaryOperatorExpressionBase : IPostFixExpression
    {
        public PostFixExpressionType Type
        {
            get
            {
                return PostFixExpressionType.Function;
            }
        }

        public int ArgCount
        {
            get
            {
                return 1;
            }
        }

        public IValue Eval(IValue[] operands)
        {
            this.Validate(operands);

            if (operands[0].Type == Values.ValueType.Number)
            {
                return this.EvalNumber(operands[0].AsDouble);
            }

            return this.EvalStirng(operands[0].AsString);
        }

        public abstract IValue EvalNumber(double operand);
        public abstract IValue EvalStirng(string operand);

        private void Validate(IValue[] operands)
        {
            if (operands.Length != 1)
            {
                throw new OperandNumberException();
            }
        }
    }
}

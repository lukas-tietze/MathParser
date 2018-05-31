using Matheparser.Parsing.PostFixExpressions.Exceptions;

namespace Matheparser.Parsing.PostFixExpressions.Unary
{
    public abstract class UnaryOperatorExpressionBase : IPostFixExpression
    {
        public PostFixExpressionType Type
        {
            get
            {
                return PostFixExpressionType.UnaryOperator;
            }
        }

        public IValue Eval(IValue[] operands)
        {
            this.Validate(operands);

            if (operands[0].Type == ValueType.Number)
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
                throw new MissingOperandException();
            }
        }
    }
}

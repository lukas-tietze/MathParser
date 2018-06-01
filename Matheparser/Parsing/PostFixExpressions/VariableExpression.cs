namespace Matheparser.Parsing.PostFixExpressions
{
    using System;
    using Matheparser.Parsing.PostFixExpressions.Exceptions;

    public sealed class VariableExpression : IPostFixExpression
    {
        private readonly string name;

        public VariableExpression(string name)
        {
            this.name = name;
        }

        public PostFixExpressionType Type
        {
            get
            {
                return PostFixExpressionType.Value;
            }
        }

        public int ArgCount
        {
            get
            {
                return 0;
            }
        }

        public IValue Eval(IValue[] operands)
        {
            this.Validate(operands);

            return VariableManager.Instance.GetValue(this.name);
        }

        private void Validate(IValue[] operands)
        {
            if (operands != null && operands.Length != 0)
            {
                throw new OperandEvaluationException();
            }
        }
    }
}

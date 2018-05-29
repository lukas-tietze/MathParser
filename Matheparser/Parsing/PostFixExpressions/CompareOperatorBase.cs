namespace Matheparser.Parsing.PostFixExpressions
{
    using Matheparser.Values;

    public abstract class CompareOperatorBase : IPostFixExpression
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
            this.Validate(operands);

            if (operands[0].Type == ValueType.Number)
            {
                return new DoubleValue(this.CompareNumber(operands[0].AsDouble, operands[1].AsDouble) ? 1 : 0);
            }

            return new DoubleValue(this.CompareString(operands[0].AsString, operands[1].AsString) ? 1 : 0);
        }

        internal abstract bool CompareNumber(double double1, double double2);

        internal abstract bool CompareString(string string1, string string2);

        protected void Validate(IValue[] operands)
        {
            if (operands.Length != 2)
            {
                throw new MissingOperandException();
            }

            if (operands[0].Type != operands[1].Type)
            {
                throw new WrongOperandTypeException();
            }
        }
    }
}

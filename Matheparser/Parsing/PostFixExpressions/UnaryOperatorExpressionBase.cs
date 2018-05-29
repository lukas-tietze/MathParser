namespace Matheparser.Parsing.PostFixExpressions
{
    public abstract class UnaryOperatorExpressionBase : IPostFixExpression
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
            throw new System.NotImplementedException();
        }
    }
}

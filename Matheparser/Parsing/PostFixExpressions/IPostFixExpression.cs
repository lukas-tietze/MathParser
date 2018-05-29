namespace Matheparser.Parsing.PostFixExpressions
{
    public interface IPostFixExpression
    {
        PostFixExpressionType Type
        {
            get;
        }

        IValue Eval(IValue[] operands);
    }
}

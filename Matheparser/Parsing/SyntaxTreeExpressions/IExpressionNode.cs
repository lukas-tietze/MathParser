namespace Matheparser.Parsing.SyntaxTreeExpressions
{
    public interface IExpressionNode
    {
        bool Validate();

        IValue Eval();

        ValueType Type
        {
            get;
        }
    }
}

namespace Matheparser.Parsing.Expressions
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

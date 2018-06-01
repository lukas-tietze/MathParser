namespace Matheparser
{
    public interface IVariable
    {
        string Name
        {
            get;
        }

        ValueType Type
        {
            get;
        }

        IValue Value
        {
            get;
        }
    }
}

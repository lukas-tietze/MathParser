namespace Matheparser.Variables
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

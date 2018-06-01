namespace Matheparser
{
    public interface IValue
    {
        ValueType Type
        {
            get;
        }

        double AsDouble
        {
            get;
        }

        string AsString
        {
            get;
        }
    }
}

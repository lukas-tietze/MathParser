using System.Collections.Generic;

namespace Matheparser
{
    public interface IFunction
    {
        string Name
        {
            get;
        }

        ValueType ReturnType
        {
            get;
        }

        IValue Eval(IReadOnlyList<IValue> parameters);

        bool Validate();
    }
}

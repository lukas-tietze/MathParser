using System.Collections.Generic;

namespace Matheparser
{
    public interface IFunction
    {
        string Name
        {
            get;
        }

        IValue Eval(IValue[] parameters);

        bool Validate();
    }
}

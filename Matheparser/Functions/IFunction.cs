using System.Collections.Generic;

namespace Matheparser.Functions
{
    public interface IFunction
    {
        string Name
        {
            get;
        }

        IValue Eval(IValue[] parameters);
    }
}

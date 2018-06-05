using Matheparser.Values;

namespace Matheparser.Functions
{
    public interface IFunction
    {
        string Name
        {
            get;
        }

        IValue Eval(EvaluationContext context, IValue[] parameters);
    }
}

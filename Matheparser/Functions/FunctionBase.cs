using Matheparser.Values;

namespace Matheparser.Functions
{
    public abstract class FunctionBase : IFunction
    {
        public abstract string Name
        {
            get;
        }

        protected EvaluationContext Context
        {
            get;
            private set;
        }

        public abstract IValue Eval(IValue[] parameters);

        public IValue Eval(EvaluationContext context, IValue[] parameters)
        {
            this.Context = context;
            return this.Eval(parameters);
        }
    }
}

using Matheparser.Variables;

namespace Matheparser.Functions
{
    public sealed class EvaluationContext
    {
        public EvaluationContext(VariableManager variableManager, FunctionManager functionManager, IConfig config)
        {
            this.VariableManager = variableManager;
            this.FunctionManager = functionManager;
            this.Config = config;
        }

        public VariableManager VariableManager { get; }

        public FunctionManager FunctionManager { get; }

        public IConfig Config { get; }
    }
}

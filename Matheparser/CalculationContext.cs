using Matheparser.Variables;

namespace Matheparser.Functions
{
    public sealed class CalculationContext
    {
        public CalculationContext(VariableManager variableManager, FunctionManager functionManager, IConfig config)
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

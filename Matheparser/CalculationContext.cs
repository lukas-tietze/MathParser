namespace Matheparser.Functions
{
    using System.Globalization;
    using Matheparser.Variables;

    public sealed class CalculationContext
    {
        public CalculationContext(VariableManager variableManager, FunctionManager functionManager, IConfig config)
        {
            this.VariableManager = variableManager;
            this.FunctionManager = functionManager;
            this.Config = config;
            this.Culture = config.Culture;
        }

        public VariableManager VariableManager { get; }

        public FunctionManager FunctionManager { get; }

        public IConfig Config { get; }

        public CultureInfo Culture { get; }
    }
}

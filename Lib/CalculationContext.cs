namespace Matheparser.Functions
{
    using System.Globalization;
    using Matheparser.Io;
    using Matheparser.Variables;

    public sealed class CalculationContext
    {
        public CalculationContext(VariableManager variableManager, FunctionManager functionManager, IConfig config)
        {
            this.VariableManager = variableManager;
            this.FunctionManager = functionManager;
            this.Config = config;
            this.Culture = config.Culture;
            this.Writer = new ConsoleWriter();
            this.Reader = new ConsoleReader();
        }

        public VariableManager VariableManager { get; set; }

        public FunctionManager FunctionManager { get; set; }

        public IConfig Config { get; set; }

        public CultureInfo Culture { get; set; }

        public IWriter Writer { get; set; }

        public IReader Reader { get; set; }
    }
}

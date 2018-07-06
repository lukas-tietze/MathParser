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
            this.Out = new ConsoleWriter();
            this.Err = new ConsoleWriter();
            this.In = new ConsoleReader();
        }

        public VariableManager VariableManager { get; set; }

        public FunctionManager FunctionManager { get; set; }

        public IConfig Config { get; set; }

        public CultureInfo Culture { get; set; }

        public IWriter Out { get; set; }

        public IWriter Err { get; set; }

        public IReader In { get; set; }
    }
}

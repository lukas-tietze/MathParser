namespace Matheparser.Functions
{
    using System.Globalization;
    using Matheparser.Io;
    using Matheparser.Variables;

    public sealed class CalculationContext
    {
        private IWriter out_writer;
        private IWriter err_writer;
        private IReader in_writer;

        public CalculationContext(VariableManager variableManager, FunctionManager functionManager, IConfig config)
        {
            this.VariableManager = variableManager;
            this.FunctionManager = functionManager;
            this.Config = config;
            this.Culture = config.Culture;
            this.Out = new ConsoleWriter();
            this.Err = new ConsoleWriter();
            this.In = new ExtendedConsoleReader();
        }

        public VariableManager VariableManager { get; set; }

        public FunctionManager FunctionManager { get; set; }

        public IConfig Config { get; set; }

        public CultureInfo Culture { get; set; }

        public IWriter Out 
        {
            get
            {
                return this.out_writer;
            }

            set
            {
                if(this.out_writer != null)
                {
                    this.out_writer.Dispose();
                }

                this.out_writer = value;
            }    
        }

        public IWriter Err 
        {
            get
            {
                return this.err_writer;
            }

            set
            {
                if(this.err_writer != null)
                {
                    this.err_writer.Dispose();
                }

                this.err_writer = value;
            }    
        }

        public IReader In
        {
            get
            {
                return this.in_writer;
            }

            set
            {
                if(this.in_writer != null)
                {
                    this.in_writer.Dispose();
                }

                this.in_writer = value;
            }    
        }
    }
}

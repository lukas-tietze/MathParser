namespace Matheparser.Solving
{
    using Matheparser.Exceptions;
    using Matheparser.Functions;
    using Matheparser.Parsing;
    using Matheparser.Parsing.Evaluation;
    using Matheparser.Tokenizing;
    using Matheparser.Values;
    using Matheparser.Variables;

    public class Calculator
    {
        private readonly CalculationContext context;

        public Calculator()
        {
            this.context = new CalculationContext(new VariableManager(true), new FunctionManager(true), ConfigBase.DefaultConfig);
        }

        public IConfig Config
        {
            get
            {
                return this.context.Config;
            }
        }

        public VariableManager VariableManager
        {
            get
            {
                return this.context.VariableManager;
            }
        }

        public FunctionManager FunctionManager
        {
            get
            {
                return this.context.FunctionManager;
            }
        }

        public IValue Calculate(string expression)
        {
            var config = this.context.Config.Clone();
            var value = default(IValue);

            try
            {
                var tokenizer = new Tokenizer(expression, config);
                tokenizer.Run();
                var parser = new Parser(tokenizer.Tokens, config);
                var evaluater = new PostFixEvaluator(parser.CreatePostFixExpression(), config);
                value = evaluater.Run();
                return value;
            }
            catch (TokenizerException t)
            {
                return new ErrorValue(t);
            }
            catch (ParserException p)
            {
                return new ErrorValue(p);
            }
        }
    }
}

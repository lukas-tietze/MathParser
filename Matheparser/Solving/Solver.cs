using System;
using Matheparser.Parsing;
using Matheparser.Parsing.Evaluation;
using Matheparser.Tokenizing;
namespace Matheparser.Solving
{
    public class Solver
    {
        private IConfig config;

        public Solver()
        {
            this.config = ConfigBase.DefaultConfig;
        }

        public IConfig Config
        {
            get
            {
                return this.config;
            }

            set
            {
                this.config = value;
            }
        }

        public IValue Solve(string expression)
        {
            var config = this.config.Clone();
            var value = default(IValue);

            try
            {
                var tokenizer = new Tokenizer(expression, config);
                tokenizer.Run();
                var parser = new Parser(tokenizer.Tokens, config);
                var evaluater = new PostFixEvaluator(parser.CreatePostFixExpression(), config);
                value = evaluater.Run();
            }
            catch (TokenizerException t)
            {
                return new ErrorValue(t);
            }
            catch(ParserException p)
            {
                return new ErrorValue(p);
            }

            return value;
        }
    }
}

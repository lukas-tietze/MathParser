namespace Matheparser.Values
{
    using System.Collections.Generic;
    using Matheparser.Functions;
    using Matheparser.Parsing.Evaluation;
    using Matheparser.Parsing.PostFixExpressions;

    public sealed class ExpressionValue : IValue
    {
        private readonly CalculationContext context;
        private readonly IReadOnlyList<IPostFixExpression> expressions;
        private IValue lastResult;

        public ExpressionValue(CalculationContext context, string expression)
        {
            this.context = context;

            var tokenizer = new Tokenizing.Tokenizer(expression, this.context.Config);
            var parser = new Parsing.Parser(tokenizer.Tokens, this.context.Config);
            this.expressions = parser.CreatePostFixExpression();
        }

        public ValueType Type
        {
            get
            {
                if (this.lastResult == null)
                {
                    this.Eval();
                }

                return this.lastResult.Type;
            }
        }

        public double AsDouble
        {
            get
            {
                if (this.lastResult == null)
                {
                    this.Eval();
                }

                return this.lastResult.AsDouble;
            }
        }

        public string AsString
        {
            get
            {
                if (this.lastResult == null)
                {
                    this.Eval();
                }

                return this.lastResult.AsString;
            }
        }

        public IEnumerable<IValue> AsSet
        {
            get
            {
                if (this.lastResult == null)
                {
                    this.Eval();
                }

                return this.lastResult.AsSet;
            }
        }

        private void Eval()
        {
            this.lastResult = new PostFixEvaluator(this.expressions, this.context.Config).Run();
        }
    }
}

namespace Matheparser.Values
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using Matheparser.Functions;
    using Matheparser.Parsing.Evaluation;
    using Matheparser.Parsing.PostFixExpressions;
    using Matheparser.Util;

    [DebuggerDisplay("Expression: {rawExpression}")]
    public sealed class ExpressionValue : IValue
    {
        private readonly string rawExpression;
        private readonly CalculationContext context;
        private readonly IReadOnlyList<IPostFixExpression> expressions;
        private IValue lastResult;

        public ExpressionValue(CalculationContext context, string expression)
        {
            this.context = context;
            this.rawExpression = expression;
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

        public IArray AsSet
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

        public override string ToString()
        {
            return this.rawExpression;
        }
    }
}

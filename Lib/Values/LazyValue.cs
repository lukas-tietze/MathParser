namespace Matheparser.Values
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using Matheparser.Functions;
    using Matheparser.Parsing.Evaluation;
    using Matheparser.Parsing.Expressions;
    using Matheparser.Util;

    [DebuggerDisplay("Expression: {rawExpression}")]
    public sealed class LazyValue : IValue
    {
        private readonly string rawExpression;
        private PostFixEvaluator evaluator;

        public LazyValue(IReadOnlyList<IPostFixExpression> expressions, CalculationContext context)
        {
            this.evaluator = new PostFixEvaluator(expressions, context);
            var sb = new StringBuilder();

            foreach (var expression in expressions)
            {
                sb.Append(expression.ToString()).Append(' ');
            }

            this.rawExpression = sb.ToString();
        }

        public LazyValue(CalculationContext context, string expression)
        {
            this.rawExpression = expression;
            var tokenizer = new Tokenizing.Tokenizer(expression, context.Config);
            tokenizer.Run();
            var parser = new Parsing.Parser(tokenizer.Tokens, context.Config);
            this.evaluator = new PostFixEvaluator(parser.CreatePostFixExpression(), context);
        }

        public ValueType Type
        {
            get
            {
                return this.evaluator.Run().Type;
            }
        }

        public double AsDouble
        {
            get
            {
                return this.evaluator.Run().AsDouble;
            }
        }

        public string AsString
        {
            get
            {
                return this.evaluator.Run().AsString;
            }
        }

        public IArray AsSet
        {
            get
            {
                return this.evaluator.Run().AsSet;
            }
        }

        public string Description
        {
            get
            {
                return this.rawExpression;
            }
        }

        public override string ToString()
        {
            return this.evaluator.Run().ToString();
        }
    }
}

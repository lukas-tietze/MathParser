using System.Collections.Generic;
using System.Diagnostics;
using Matheparser.Functions;
using Matheparser.Parsing.Evaluation;
using Matheparser.Values;

namespace Matheparser.Parsing.Expressions
{
    [DebuggerDisplay("Lazy expression:")]
    public class LazyExpression : IPostFixExpression
    {
        private IReadOnlyList<IPostFixExpression> expressions;

        public LazyExpression(IReadOnlyList<IPostFixExpression> expressions)
        {
            this.expressions = new List<IPostFixExpression>(expressions);
        }

        public int ArgCount
        {
            get
            {
                return 0;
            }
        }

        public ExpressionType Type
        {
            get
            {
                return ExpressionType.Value;
            }
        }

        public IValue Eval(CalculationContext context, IValue[] operands)
        {
            return new LazyValue(this.expressions, context);
        }
    }
}
using System;
using System.Collections.Generic;
using Matheparser.Parsing.PostFixExpressions;

namespace Matheparser.Parsing.Evaluation
{
    public class PostFixEvaluator
    {
        private IConfig config;
        private IReadOnlyList<IPostFixExpression> expressions;

        public PostFixEvaluator(IReadOnlyList<IPostFixExpression> expressions, IConfig config)
        {
            this.expressions = expressions;
            this.config = config;
        }

        public IValue Run()
        {
            var stack = new Stack<IValue>();
            var operands = new IValue[2];

            foreach (var expression in this.expressions)
            {
                switch (expression.Type)
                {
                    case PostFixExpressionType.Operator:
                        operands[0] = stack.Pop();
                        operands[1] = stack.Pop();
                        stack.Push(expression.Eval(operands));
                        break;
                    case PostFixExpressionType.Value:
                        stack.Push(expression.Eval(null));
                        break;
                }
            }

            return stack.Pop();
        }
    }
}

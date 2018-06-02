using System;
using System.Collections.Generic;
using Matheparser.Parsing.PostFixExpressions;

namespace Matheparser.Parsing.Evaluation
{
    public class PostFixEvaluator
    {
        private readonly IConfig config;
        private readonly IReadOnlyList<IPostFixExpression> expressions;

        public PostFixEvaluator(IReadOnlyList<IPostFixExpression> expressions, IConfig config)
        {
            this.expressions = expressions;
            this.config = config;
        }

        public IValue Run()
        {
            var stack = new Stack<IValue>();

            foreach (var expression in this.expressions)
            {
                switch (expression.Type)
                {
                    case PostFixExpressionType.Value:
                        stack.Push(expression.Eval(null));
                        break;
                    case PostFixExpressionType.Function:
                        var args = new IValue[expression.ArgCount];

                        for(var i = 0; i < expression.ArgCount; i++)
                        {
                            args[expression.ArgCount - i - 1] = stack.Pop();
                        }

                        stack.Push(expression.Eval(args));

                        break;
                    default:
                        throw new NotSupportedException();
                }
            }

            return stack.Pop();
        }
    }
}

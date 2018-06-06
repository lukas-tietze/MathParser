using System;
using System.Collections.Generic;
using Matheparser.Functions;
using Matheparser.Parsing.PostFixExpressions;
using Matheparser.Values;
using Matheparser.Variables;

namespace Matheparser.Parsing.Evaluation
{
    public class PostFixEvaluator
    {
        private readonly IReadOnlyList<IPostFixExpression> expressions;
        private readonly EvaluationContext context;

        public PostFixEvaluator(IReadOnlyList<IPostFixExpression> expressions, IConfig config)
        {
            this.expressions = expressions;
            this.context = new EvaluationContext(new VariableManager(true), new FunctionManager(true), config);
        }

        public IValue Run()
        {
            var stack = new Stack<IValue>();

            foreach (var expression in this.expressions)
            {
                switch (expression.Type)
                {
                    case PostFixExpressionType.Value:
                        stack.Push(expression.Eval(this.context, null));
                        break;
                    case PostFixExpressionType.Function:
                        var args = new IValue[expression.ArgCount];

                        for(var i = 0; i < expression.ArgCount; i++)
                        {
                            args[expression.ArgCount - i - 1] = stack.Pop();
                        }

                        stack.Push(expression.Eval(this.context, args));

                        break;
                    default:
                        throw new NotSupportedException();
                }
            }

            return stack.Pop();
        }
    }
}

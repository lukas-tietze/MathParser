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
            var binaryoperands = new IValue[2];
            var unaryOperand = new IValue[1];

            foreach (var expression in this.expressions)
            {
                switch (expression.Type)
                {
                    case PostFixExpressionType.UnaryOperator:
                        stack.Push(expression.Eval(new IValue[] { stack.Pop() }));
                        break;
                    case PostFixExpressionType.BinaryOperator:
                        stack.Push(expression.Eval(new IValue[] { stack.Pop(), stack.Pop() }));
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

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
            var operandsBinary = new IValue[2];
            var operandsUnary = new IValue[1];

            foreach (var expression in this.expressions)
            {
                switch (expression.Type)
                {
                    case PostFixExpressionType.UnaryOperator:
                        operandsUnary[0] = stack.Pop();
                        stack.Push(expression.Eval(operandsUnary));
                        break;
                    case PostFixExpressionType.BinaryOperator:
                        operandsBinary[0] = stack.Pop();
                        operandsBinary[1] = stack.Pop();
                        stack.Push(expression.Eval(operandsBinary));
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

using System;
using System.Collections.Generic;
using Matheparser.Parsing.PostFixExpressions;
using Matheparser.Tokenizing;

namespace Matheparser.Parsing
{
    public class Parser
    {
        private IReadOnlyList<Token> tokens;
        private IConfig config;

        public Parser(IReadOnlyList<Token> tokens, IConfig config)
        {
            this.tokens = tokens;
            this.config = config;
        }

        public IReadOnlyList<IPostFixExpression> CreatePostFixExpression()
        {
            var operatorStack = new Stack<char>();
            var expressions = new List<IPostFixExpression>();

            foreach (var token in this.tokens)
            {
                switch (token.Type)
                {
                    case TokenType.String:
                    case TokenType.Number:
                        expressions.Add(new ValueExpression(token.Value));
                        break;
                    case TokenType.Identifier:
                        break;
                    case TokenType.Operator:
                        var op = token.Value[0];

                        if (operatorStack.Count == 0)
                        {
                            operatorStack.Push(op);
                        }
                        else if (IsHigherPriority(op, operatorStack.Peek()))
                        {
                            operatorStack.Push(op);
                        }
                        else
                        {
                            expressions.Add(this.CreateOperatorExpression(operatorStack.Pop()));

                            while (operatorStack.Count > 0 && !IsHigherPriority(op, operatorStack.Peek()))
                            {
                                expressions.Add(this.CreateOperatorExpression(operatorStack.Pop()));
                            }
                        }

                        break;
                    case TokenType.Seperator:
                        break;
                    case TokenType.OpeningBracket:
                        break;
                    case TokenType.ClosingBracket:
                        break;
                }
            }

            while (operatorStack.Count > 0)
            {
                expressions.Add(this.CreateOperatorExpression(operatorStack.Pop()));
            }

            return expressions.AsReadOnly();
        }

        private bool IsHigherPriority(char opA, char opB)
        {
            if (opA == this.config.OperatorMul || opA == this.config.OperatorDiv)
            {
                return opB != this.config.OperatorMul && opB != this.config.OperatorDiv;
            }

            if (opA == this.config.OperatorAdd || opA == this.config.OperatorSub)
            {
                return opB == this.config.OperatorMod;
            }

            return false;
        }

        private IPostFixExpression CreateOperatorExpression(char op)
        {
            if (op == this.config.OperatorAdd)
            {
                return new AddExpression();
            }

            if (op == this.config.OperatorSub)
            {
                return new SubExpression();
            }

            if (op == this.config.OperatorMul)
            {
                return new MulExpression();
            }

            if (op == this.config.OperatorDiv)
            {
                return new DivExpression();
            }

            if (op == this.config.OperatorMod)
            {
                return new ModExpression();
            }

            throw new NotSupportedException();
        }

        public void CreateAbstractSyntaxTree()
        {
            throw new NotImplementedException();
        }
    }
}

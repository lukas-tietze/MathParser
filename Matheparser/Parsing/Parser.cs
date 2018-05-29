using System;
using System.Collections.Generic;
using Matheparser.Parsing.PostFixExpressions;
using Matheparser.Parsing.PostFixExpressions.Binary.Arithmetic;
using Matheparser.Parsing.PostFixExpressions.Binary.Compare;
using Matheparser.Parsing.PostFixExpressions.Unary;
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
            var operatorStack = new Stack<TokenType>();
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
                    case TokenType.Seperator:
                        break;
                    case TokenType.OpeningBracket:
                        break;
                    case TokenType.ClosingBracket:
                        break;
                    default:
                        if ((token.Type & TokenType.Operator) != 0)
                        {

                            if (operatorStack.Count == 0)
                            {
                                operatorStack.Push(token.Type);
                            }
                            else if (IsHigherPriority(token.Type, operatorStack.Peek()))
                            {
                                operatorStack.Push(token.Type);
                            }
                            else
                            {
                                expressions.Add(this.CreateOperatorExpression(operatorStack.Pop()));

                                while (operatorStack.Count > 0 && !IsHigherPriority(token.Type, operatorStack.Peek()))
                                {
                                    expressions.Add(this.CreateOperatorExpression(operatorStack.Pop()));
                                }
                            }
                        }
                        else
                        {
                            throw new NotSupportedException();
                        }

                        break;
                }
            }

            while (operatorStack.Count > 0)
            {
                expressions.Add(this.CreateOperatorExpression(operatorStack.Pop()));
            }

            return expressions.AsReadOnly();
        }

        private bool IsHigherPriority(TokenType opA, TokenType opB)
        {
            return opA > opB;
        }

        private IPostFixExpression CreateOperatorExpression(TokenType op)
        {
            switch (op)
            {
                case TokenType.String:
                case TokenType.Number:
                case TokenType.Identifier:
                case TokenType.Seperator:
                case TokenType.OpeningBracket:
                case TokenType.ClosingBracket:
                case TokenType.Operator:
                    throw new ArgumentException();
                case TokenType.OperatorAdd:
                    return new AddExpression();
                case TokenType.OperatorSub:
                    return new SubExpression();
                case TokenType.OperatorMul:
                    return new MulExpression();
                case TokenType.OperatorDiv:
                    return new DivExpression();
                case TokenType.OperatorExp:
                    return new ExpExpression();
                case TokenType.OperatorNot:
                    return new NotExpression();
                case TokenType.OperatorEqual:
                    return new EqualExpression();
                case TokenType.OperatorNotEqual:
                    return new NotEqualExpression();
                case TokenType.OperatorGreater:
                    return new GreaterExpression();
                case TokenType.OperatorGreaterEqual:
                    return new GreaterEqualExpression();
                case TokenType.OperatorLess:
                    return new LessExpression();
                case TokenType.OperatorLessEqual:
                    return new LessEqualExpression();
                default:
                    throw new NotSupportedException();
            }
        }

        public void CreateAbstractSyntaxTree()
        {
            throw new NotImplementedException();
        }
    }
}
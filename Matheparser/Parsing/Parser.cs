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
        private readonly IReadOnlyList<Token> tokens;
        private readonly IConfig config;

        public Parser(IReadOnlyList<Token> tokens, IConfig config)
        {
            this.tokens = tokens;
            this.config = config;
        }

        public IReadOnlyList<IPostFixExpression> CreatePostFixExpression()
        {
            var operatorStack = new Stack<TokenType>();
            var functionStack = new Stack<string>();
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
                        expressions.Add(new VariableExpression(token.Value));
                        break;
                    case TokenType.OpeningBracket:
                    case TokenType.ClosingBracket:
                        operatorStack.Push(token.Type);
                        break;
                    case TokenType.FunctionStart:
                        operatorStack.Push(token.Type);
                        functionStack.Push(token.Value);
                        break;
                    case TokenType.FunctionEnd:
                    case TokenType.Seperator:
                        var top = operatorStack.Peek();

                        while (operatorStack.Count > 0)
                        {
                            if (top == TokenType.FunctionStart || top == TokenType.Seperator)
                            {
                                this.AddOperatorExpression(operatorStack.Pop(), expressions);
                            }

                            top = operatorStack.Peek();
                        }

                        if(token.Type == TokenType.FunctionEnd)
                        {
                            expressions.Add(new Function);
                        }

                        break;
                    default:
                        if ((token.Type & TokenType.Operator) != 0)
                        {
                            if (operatorStack.Count == 0 || IsHigherPriority(token.Type, operatorStack.Peek()))
                            {
                                operatorStack.Push(token.Type);
                            }
                            else
                            {
                                this.AddOperatorExpression(operatorStack.Pop(), expressions);

                                while (operatorStack.Count > 0 && !IsHigherPriority(token.Type, operatorStack.Peek()))
                                {
                                    this.AddOperatorExpression(operatorStack.Pop(), expressions);
                                }

                                operatorStack.Push(token.Type);
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
                this.AddOperatorExpression(operatorStack.Pop(), expressions);
            }

            return expressions.AsReadOnly();
        }

        private bool IsHigherPriority(TokenType opA, TokenType opB)
        {
            var mask = 0x0F00;

            return ((int)opA & mask) > ((int)opB & mask);
        }

        private void AddOperatorExpression(TokenType type, List<IPostFixExpression> target)
        {
            var token = this.CreateOperatorExpression(type);

            if(token != null)
            {
                target.Add(token);
            }
        }

        private IPostFixExpression CreateOperatorExpression(TokenType op)
        {
            switch (op)
            {
                case TokenType.String:
                case TokenType.Number:
                case TokenType.Identifier:
                case TokenType.Seperator:
                case TokenType.Operator:
                    throw new ArgumentException();
                case TokenType.OpeningBracket:
                case TokenType.ClosingBracket:
                    return null;
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
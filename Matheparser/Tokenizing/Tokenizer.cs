using System;
using System.Collections.Generic;
using System.Text;

namespace Matheparser.Tokenizing
{
    public class Tokenizer
    {
        private char[] data;
        private int pos;
        private IConfig config;
        private List<Token> tokens;
        private Stack<char> bracketStack;
        bool lastWasWhiteSpace;

        public Tokenizer(string data, IConfig config)
        {
            this.data = data.ToCharArray();
            this.pos = 0;
            this.config = config;
            this.tokens = new List<Token>();
            this.bracketStack = new Stack<char>();
            this.lastWasWhiteSpace = false;
        }

        public IReadOnlyList<Token> Tokens
        {
            get
            {
                return this.tokens.AsReadOnly();
            }
        }

        public bool Finished
        {
            get
            {
                return this.pos >= this.data.Length;
            }
        }

        public void Reset()
        {
            this.pos = 0;
            this.tokens.Clear();
            this.bracketStack.Clear();
            this.lastWasWhiteSpace = false;
        }

        public void Run()
        {
            while (this.pos < this.data.Length)
            {
                this.tokens.Add(this.ReadNext());
            }

            if (this.bracketStack.Count != 0)
            {
                throw new MissingBracketException();
            }
        }

        private Token ReadNext()
        {
            var c = this.data[this.pos];
            this.lastWasWhiteSpace = false;

            if (char.IsWhiteSpace(c))
            {
                this.SkipWhiteSpace();
            }

            if (this.IsStartOfNumber(c))
            {
                return this.ReadNumber();
            }
            else if (c == this.config.StringSeperator)
            {
                return this.ReadString();
            }
            else if (this.IsStartOfIdentifier(c))
            {
                return this.ReadIdentifier();
            }
            else if (this.config.IsOpeningBracket(c))
            {
                this.pos++;
                this.bracketStack.Push(c);
                return new Token(TokenType.OpeningBracket, c.ToString());
            }
            else if (this.config.IsClosingBracket(c))
            {
                if (this.bracketStack.Count == 0 || !this.config.AreMatchingBrackets(c, this.bracketStack.Peek()))
                {
                    throw new MismatchingBracketException();
                }

                this.pos++;
                this.bracketStack.Pop();
                return new Token(TokenType.ClosingBracket, c.ToString());
            }
            else if (c == this.config.ListSeperator)
            {
                this.pos++;
                return new Token(TokenType.Seperator, c.ToString());
            }
            else if (this.TryReadOperator(out Token token))
            {
                return token;
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        private bool TryReadOperator(out Token tokenOut)
        {
            var c1 = this.data[this.pos];
            var c2 = this.data[this.pos + 1];
            var token = default(Token);

            switch (c1)
            {
                case '+':
                    this.pos++;
                    token = new Token(TokenType.Operator, string.Empty);
                    break;
                case '-':
                    this.pos++;
                    token = new Token(TokenType.Operator, string.Empty);
                    break;
                case '*':
                    this.pos++;
                    token = new Token(TokenType.Operator, string.Empty);
                    break;
                case '/':
                    this.pos++;
                    token = new Token(TokenType.Operator, string.Empty);
                    break;
                case '^':
                    this.pos++;
                    token = new Token(TokenType.Operator, string.Empty);
                    break;
                case '=':
                    if (c2 == '=')
                    {
                        this.pos += 2;
                        token = new Token(TokenType.Operator, string.Empty);
                        break;
                    }
                    else
                    {
                        throw new TokenizerException();
                    }
                case '>':
                    if (c2 == '=')
                    {
                        token = new Token(TokenType.Operator, string.Empty);
                    }
                    else
                    {
                        token = new Token(TokenType.Operator, string.Empty);
                    }

                    break;
                case '<':
                    if (c2 == '=')
                    {
                        token = new Token(TokenType.Operator, string.Empty);
                    }
                    else
                    {
                        token = new Token(TokenType.Operator, string.Empty);
                    }
                    break;

                case '!':
                    if (c2 == '=')
                    {
                        token = new Token(TokenType.Operator, string.Empty);
                    }
                    else
                    {
                        token = new Token(TokenType.Operator, string.Empty);
                    }
                    break;
                default:
                    throw new TokenizerException();
            }

            tokenOut = token;
            return token != default(Token);
        }

        private Token ReadIdentifier()
        {
            var readStart = this.pos;

            do
            {
                ++this.pos;
            }
            while (this.pos < this.data.Length && this.IsPartOfIdentifier(this.data[this.pos]));

            return new Token(TokenType.Identifier, new string(this.data, readStart, this.pos - readStart));
        }

        private Token ReadString()
        {
            var sb = new StringBuilder();
            var escaped = false;

            while (++this.pos < this.data.Length)
            {
                var c = this.data[this.pos];

                if (escaped)
                {
                    sb.Append(this.Escape(c));
                    escaped = false;
                }
                else if (c == this.config.StringEscapeChar)
                {
                    escaped = true;
                }
                else if (c == this.config.StringSeperator)
                {
                    this.pos++;
                    break;
                }
                else
                {
                    sb.Append(c);
                }
            }

            return new Token(TokenType.String, sb.ToString());
        }

        private char Escape(char c)
        {
            switch (c)
            {
                case 'a':
                    return '\a';
                case 'b':
                    return '\b';
                case 'f':
                    return '\f';
                case 'n':
                    return '\n';
                case 'r':
                    return '\r';
                case 't':
                    return '\t';
                case 'v':
                    return '\v';
                case '\'':
                    return '\'';
                case '\"':
                    return '\"';
                case '\\':
                    return '\\';
                default:
                    throw new InvalidEscapeSequenceException();
            }
        }

        private Token ReadNumber()
        {
            var decSeperatorRead = false;
            var readStart = this.pos;

            while (this.pos < this.data.Length)
            {
                if (!this.IsDigit(this.data[this.pos]))
                {
                    if (this.data[this.pos] == this.config.DecimalSeperator && !decSeperatorRead)
                    {
                        decSeperatorRead = true;
                    }
                    else
                    {
                        break;
                    }
                }

                this.pos++;
            }

            return new Token(TokenType.Number, new string(this.data, readStart, this.pos - readStart));
        }

        private void SkipWhiteSpace()
        {
            var readStart = this.pos;

            while (char.IsWhiteSpace(this.data[this.pos]))
            {
                this.pos++;
            }

            this.lastWasWhiteSpace = true;
        }

        private bool IsStartOfIdentifier(char c)
        {
            ////TODO: Vielleicht auch in die Config auslagern.
            return c >= 'a' && c <= 'z' ||
                    c >= 'A' && c <= 'Z' ||
                    c == '_';
        }

        private bool IsPartOfIdentifier(char c)
        {
            ////TODO: Vielleicht auch in die Config auslagern.
            return c >= 'a' && c <= 'z' ||
                    c >= 'A' && c <= 'Z' ||
                    c == '_';
        }

        private bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        private bool IsStartOfNumber(char c)
        {
            return this.IsDigit(c) || c == this.config.DecimalSeperator;
        }
    }
}

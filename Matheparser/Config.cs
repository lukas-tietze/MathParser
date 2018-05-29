using System;
using System.Collections.Generic;

namespace Matheparser
{
    public class ConfigBase : IConfig
    {
        private char operatorAdd;
        private char operatorSub;
        private char operatorMul;
        private char operatorDiv;
        private char operatorMod;
        private char decimalSeperator;
        private char stringSeperator;
        private char listSeperator;
        private char stringEscapeChar;
        private char[] openingBrackets;
        private char[] closingBrackets;

        public static IConfig DefaultConfig
        {
            get
            {
                return new ConfigBase(
                    operatorAdd: '+',
                    operatorSub: '-',
                    operatorMul: '*',
                    operatorDiv: '/',
                    operatorMod: '%',
                    decimalSeperator: ',',
                    stringSeperator: '\"',
                    listSeperator: ';',
                    stringEscapeChar: '\\',
                    openingBrackets: new char[] { '(', '[', '{', },
                    closingBrackets: new char[] { ')', ']', '}', }
                );
            }
        }

        public ConfigBase(char operatorAdd,
                      char operatorSub,
                      char operatorMul,
                      char operatorDiv,
                      char operatorMod,
                      char decimalSeperator,
                      char stringSeperator,
                      char listSeperator,
                      char stringEscapeChar,
                      char[] openingBrackets,
                      char[] closingBrackets)
        {
            this.operatorAdd = operatorAdd;
            this.operatorSub = operatorSub;
            this.operatorMul = operatorMul;
            this.operatorDiv = operatorDiv;
            this.operatorMod = operatorMod;
            this.decimalSeperator = decimalSeperator;
            this.stringSeperator = stringSeperator;
            this.listSeperator = listSeperator;
            this.stringEscapeChar = stringEscapeChar;
            this.openingBrackets = openingBrackets;
            this.closingBrackets = closingBrackets;

            if (!this.Validate())
            {
                throw new ArgumentException();
            }
        }

        private bool Validate()
        {
            var chars = new List<char>()
            {
                this.operatorAdd,
                this.operatorSub,
                this.operatorMul,
                this.operatorDiv,
                this.operatorMod,
                this.decimalSeperator,
                this.stringSeperator,
                this.listSeperator,
                this.stringEscapeChar,
            };

            chars.AddRange(this.openingBrackets);
            chars.AddRange(this.closingBrackets);

            for (var i = 0; i < chars.Count - 1; i++)
            {
                for (var j = i + 1; j < chars.Count; j++)
                {
                    if (chars[i] == chars[j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public char OperatorAdd
        {
            get
            {
                return this.operatorAdd;
            }
        }

        public char OperatorSub
        {
            get
            {
                return this.operatorSub;
            }
        }

        public bool IsOpeningBracket(char c)
        {
            for (var i = 0; i < this.openingBrackets.Length; i++)
            {
                if (this.openingBrackets[i] == c)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsClosingBracket(char c)
        {
            for (var i = 0; i < this.closingBrackets.Length; i++)
            {
                if (this.closingBrackets[i] == c)
                {
                    return true;
                }
            }

            return false;
        }

        public bool AreMatchingBrackets(char a, char b)
        {
            for (var i = 0; i < this.openingBrackets.Length; i++)
            {
                if (this.openingBrackets[i] == a)
                {
                    return this.closingBrackets[i] == b;
                }
            }

            throw new ArgumentException("Bracket type not found");
        }

        public bool IsOperator(char c)
        {
            return c == this.operatorAdd ||
                c == this.operatorSub ||
                c == this.operatorMul ||
                c == this.operatorDiv ||
                c == this.operatorMod;
        }

        public char OperatorMul
        {
            get
            {
                return this.operatorMul;
            }
        }

        public char OperatorDiv
        {
            get
            {
                return this.operatorDiv;
            }
        }

        public char OperatorMod
        {
            get
            {
                return this.operatorMod;
            }
        }

        public char StringSeperator
        {
            get
            {
                return this.stringSeperator;
            }
        }

        public char DecimalSeperator
        {
            get
            {
                return this.decimalSeperator;
            }
        }

        public char ListSeperator
        {
            get
            {
                return this.listSeperator;
            }
        }

        public char StringEscapeChar
        {
            get
            {
                return this.stringEscapeChar;
            }
        }

        public IConfig Clone()
        {
            return new ConfigBase(operatorAdd: this.operatorAdd,
                            operatorSub: this.operatorSub,
                            operatorMul: this.operatorMul,
                            operatorDiv: this.operatorDiv,
                            operatorMod: this.operatorMod,
                            decimalSeperator: this.decimalSeperator,
                            stringSeperator: this.stringSeperator,
                            listSeperator: this.listSeperator,
                            stringEscapeChar: this.stringEscapeChar,
                            openingBrackets: this.openingBrackets,
                            closingBrackets: this.closingBrackets
            );
        }
    }
}

using System;
using System.Collections.Generic;

namespace Matheparser
{
    public interface IConfig
    {
        bool IsOpeningBracket(char c);

        bool IsClosingBracket(char c);

        bool AreMatchingBrackets(char a, char b);

        bool IsOperator(char c);

        char OperatorAdd
        {
            get;
        }

        char OperatorSub
        {
            get;
        }

        char OperatorMul
        {
            get;
        }

        char OperatorDiv
        {
            get;
        }

        char OperatorMod
        {
            get;
        }

        char StringSeperator
        {
            get;
        }

        char DecimalSeperator
        {
            get;
        }

        char ListSeperator
        {
            get;
        }

        char StringEscapeChar
        {
            get;
        }

        IConfig Clone();
    }
}

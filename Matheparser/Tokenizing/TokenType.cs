﻿namespace Matheparser.Tokenizing
{
    public enum TokenType
    {
        String = 0x0000,
        Number = 0x0001,
        Identifier = 0x0002,
        Seperator = 0x0003,
        OpeningBracket = 0x0004,
        ClosingBracket = 0x0005,
        Operator = 0x1000,
        OperatorAdd = 0x1C01,
        OperatorSub = 0x1C02,
        OperatorMul = 0x1D03,
        OperatorDiv = 0x1D04,
        OperatorExp = 0x1E05,
        OperatorNot = 0x1C06,
        OperatorEqual = 0x1A07,
        OperatorNotEqual = 0x1A08,
        OperatorGreater = 0x1A09,
        OperatorGreaterEqual = 0x1A0A,
        OperatorLess = 0x1A0B,
        OperatorLessEqual = 0x1A0C,
    }
}

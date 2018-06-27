namespace Matheparser.Tokenizing
{
    public enum TokenType
    {
        String = 0x2000,
        Number = 0x2001,
        Identifier = 0x2002,
        Seperator = 0x2003,
        OpeningBracket = 0x2004,
        ClosingBracket = 0x2005,
        SetStart = 0x2006,
        SetEnd = 0x2007,
        AccessorStart = 0x2008,
        AccessorEnd = 0x2009,
        FunctionStart = 0x200A,
        FunctionEnd = 0x200B,

        Operator = 0x1000,
        OperatorAdd = 0x1C01,
        OperatorSub = 0x1C02,
        OperatorMul = 0x1D03,
        OperatorDiv = 0x1D04,
        OperatorExp = 0x1E05,
        OperatorMod = 0x1D05,
        OperatorNot = 0x1C06,
        OperatorEqual = 0x1A07,
        OperatorNotEqual = 0x1A08,
        OperatorGreater = 0x1A09,
        OperatorGreaterEqual = 0x1A0A,
        OperatorLess = 0x1A0B,
        OperatorLessEqual = 0x1A0C,
    }
}

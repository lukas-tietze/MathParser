namespace Matheparser.Tokenizing
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
        OperatorAdd = 0x1001,
        OperatorSub = 0x1002,
        OperatorMul = 0x1003,
        OperatorDiv = 0x1004,
        OperatorExp = 0x1005,
        OperatorNot = 0x1006,
        OperatorEqual = 0x1007,
        OperatorNotEqual = 0x1008,
        OperatorGreater = 0x1009,
        OperatorGreaterEqual = 0x100A,
        OperatorLess = 0x100B,
        OperatorLessEqual = 0x100C,
    }
}

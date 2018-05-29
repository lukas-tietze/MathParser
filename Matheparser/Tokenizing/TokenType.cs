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
        OperatorSub = 0x1001,
        OperatorMul = 0x1001,
        OperatorDiv = 0x1001,
        OperatorExp = 0x1001,
        OperatorNot = 0x1001,
        OperatorNotEqual = 0x1001,
        Operator = 0x1001,
    }
}

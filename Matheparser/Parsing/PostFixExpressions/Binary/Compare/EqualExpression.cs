namespace Matheparser.Parsing.PostFixExpressions.Binary.Compare
{
    using System.Diagnostics;

    [DebuggerDisplay("Operator ==")]
    public sealed class EqualExpression : CompareOperatorBase
    {
        internal override bool CompareNumber(double double1, double double2)
        {
            return double1 == double2;
        }

        internal override bool CompareString(string string1, string string2)
        {
            return string1.Equals(string2);
        }
    }
}

namespace Matheparser.Parsing.PostFixExpressions
{
    using System.Diagnostics;
    using Matheparser.Values;

    [DebuggerDisplay("Operator: >=")]
    public class GreaterEqualExpression : CompareOperatorBase
    {
        internal override bool CompareNumber(double double1, double double2)
        {
            return double1 >= double2;
        }

        internal override bool CompareString(string string1, string string2)
        {
            return string1.CompareTo(string2) == 1;
        }
    }
}

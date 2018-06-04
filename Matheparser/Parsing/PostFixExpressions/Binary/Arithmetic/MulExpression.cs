using System;
using System.Diagnostics;
using Matheparser.Exceptions;
using Matheparser.Values;

namespace Matheparser.Parsing.PostFixExpressions.Binary.Arithmetic
{
    [DebuggerDisplay("Operator *")]
    public sealed class MulExpression : BinaryOperatorExpressionBase
    {
        internal override IValue EvalNumber(double double1, double double2)
        {
            return new DoubleValue(double1 * double2);
        }

        internal override IValue EvalString(string string1, string string2)
        {
            throw new InvalidOperationException();
        }

        public override string ToString()
        {
            return "Op *";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Matheparser.Values;

namespace Matheparser.Parsing.PostFixExpressions.Binary.Arithmetic
{
    [DebuggerDisplay("Operator %")]
    public sealed class ModExpression : BinaryOperatorExpressionBase
    {
        internal override IValue EvalNumber(double double1, double double2)
        {
            return new DoubleValue(double1 % double2);
        }

        internal override IValue EvalString(string string1, string string2)
        {
            throw new InvalidOperationException();
        }

        internal override IValue EvalSet(HashSet<IValue> setA, HashSet<IValue> setB)
        {
            var res = new HashSet<IValue>();

            foreach (var item in setA)
            {
                if (setB.Contains(item))
                {
                    res.Add(item);
                }
            }

            return new SetValue(res);
        }

        public override string ToString()
        {
            return "Op %";
        }
    }
}

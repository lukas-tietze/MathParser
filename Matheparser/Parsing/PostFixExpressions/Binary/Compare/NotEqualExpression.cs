﻿namespace Matheparser.Parsing.PostFixExpressions.Binary.Compare
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using Matheparser.Values;

    [DebuggerDisplay("Operator !=")]
    public sealed class NotEqualExpression : CompareOperatorBase
    {
        internal override bool CompareNumber(double double1, double double2)
        {
            return double1 != double2;
        }

        internal override bool CompareString(string string1, string string2)
        {
            return !string1.Equals(string2);
        }

        internal override bool CompareSet(HashSet<IValue> setA, HashSet<IValue> setB)
        {
            if(setA.Count != setB.Count)
            {
                return true;
            }

            foreach(var item in setA)
            {
                if(!setB.Contains(item))
                {
                    return true;
                }
            }

            return false;
        }

        public override string ToString()
        {
            return "Op !=";
        }
    }
}

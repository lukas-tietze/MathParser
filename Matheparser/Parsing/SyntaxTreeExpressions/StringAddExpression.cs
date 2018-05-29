using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matheparser.Values;

namespace Matheparser.Parsing.SyntaxTreeExpressions
{
    public class StringAddExpression : ExpressionNodeBase
    {
        public override ValueType Type
        {
            get
            {
                return ValueType.String;
            }
        }

        public override IValue Eval()
        {
            return new StringValue(string.Concat(this.ChildNodes[0].Eval().AsString, this.ChildNodes[1].Eval().AsString));
        }

        public override bool Validate()
        {
            return this.ChildNodes.Count == 2 &&
                this.ChildNodes[0].Type == ValueType.String &&
                this.ChildNodes[1].Type == ValueType.String;
        }
    }
}

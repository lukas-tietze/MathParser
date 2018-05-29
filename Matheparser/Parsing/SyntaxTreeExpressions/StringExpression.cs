using System;
using Matheparser.Values;

namespace Matheparser.Parsing.Expressions
{
    public class StringExpression : ExpressionNodeBase
    {
        private StringValue value;

        public StringExpression(string value)
        {
            this.value = new StringValue(value);
        }

        public override ValueType Type
        {
            get
            {
                return ValueType.String;
            }
        }

        public override IValue Eval()
        {
            return this.value;
        }

        public override bool Validate()
        {
            return true;
        }
    }
}

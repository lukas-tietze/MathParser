using Matheparser.Values;

namespace Matheparser.Parsing.SyntaxTreeExpressions
{
    public class DoubleExpression : ExpressionNodeBase
    {
        private DoubleValue value;

        public DoubleExpression(string expression)
        {
            this.value = new DoubleValue(double.Parse(expression));
        }

        public override ValueType Type
        {
            get
            {
                return ValueType.Number;
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

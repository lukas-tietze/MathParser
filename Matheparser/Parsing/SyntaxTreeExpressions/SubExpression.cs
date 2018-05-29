using Matheparser.Values;

namespace Matheparser.Parsing.SyntaxTreeExpressions
{
    public class SubExpression : ExpressionNodeBase
    {
        public override ValueType Type
        {
            get
            {
                return ValueType.Number;
            }
        }

        public override IValue Eval()
        {
            return new DoubleValue(this.ChildNodes[0].Eval().AsDouble - this.ChildNodes[1].Eval().AsDouble);
        }

        public override bool Validate()
        {
            return this.ChildNodes.Count == 2 &&
                this.ChildNodes[0].Type == ValueType.Number &&
                this.ChildNodes[1].Type == ValueType.Number;
        }
    }
}

using System.Diagnostics;
using Matheparser.Values;

namespace Matheparser.Parsing.PostFixExpressions
{
    [DebuggerDisplay("Value={this.value}")]
    public class ValueExpression : IPostFixExpression
    {
        private IValue value;

        public ValueExpression(string value) :
            this(ValueCreator.Create(value))
        {
        }

        public ValueExpression(double value) :
            this(ValueCreator.Create(value))
        {
        }

        public ValueExpression(IValue value)
        {
            this.value = value;
        }

        public PostFixExpressionType Type
        {
            get
            {
                return PostFixExpressionType.Value;
            }
        }

        public IValue Eval(IValue[] operands)
        {
            return this.value;
        }
    }
}

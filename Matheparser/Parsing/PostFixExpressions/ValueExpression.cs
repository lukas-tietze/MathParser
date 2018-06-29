namespace Matheparser.Parsing.PostFixExpressions
{
    using System.Diagnostics;
    using Matheparser.Functions;
    using Matheparser.Values;

    [DebuggerDisplay("Value={this.value}")]
    public class ValueExpression : IPostFixExpression
    {
        private readonly IValue value;

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

        public int ArgCount
        {
            get
            {
                return 0;
            }
        }

        public IValue Eval(CalculationContext context, IValue[] operands)
        {
            return this.value;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", this.value.Type, this.value);
        }
    }
}
namespace Matheparser.Values
{
    public class DoubleValue : IValue
    {
        private double value;
        private string asString;

        public DoubleValue(double value)
        {
            this.value = value;
        }

        public ValueType Type
        {
            get
            {
                return ValueType.Number;
            }
        }

        public double AsDouble
        {
            get
            {
                return this.value;
            }
        }

        public string AsString
        {
            get
            {
                if (string.IsNullOrEmpty(this.asString))
                {
                    this.asString = this.value.ToString();
                }

                return this.asString;
            }
        }

        public override string ToString()
        {
            return this.AsString;
        }
    }
}

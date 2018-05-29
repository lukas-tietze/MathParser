namespace Matheparser.Values
{
    public class DoubleValue : IValue
    {
        private double value;

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
                return string.Empty;
            }
        }

        public override string ToString()
        {
            return this.value.ToString();
        }
    }
}

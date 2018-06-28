using System.Collections.Generic;

namespace Matheparser.Values
{
    public class StringValue : IValue
    {
        private string value;

        public StringValue(string value)
        {
            this.value = value;
        }

        public ValueType Type
        {
            get
            {
                return ValueType.String;
            }
        }

        public double AsDouble
        {
            get
            {
                return double.NaN;
            }
        }

        public string AsString
        {
            get
            {
                return this.value;
            }
        }

        public HashSet<IValue> AsSet
        {
            get
            {
                return new HashSet<IValue>(new[] { this });
            }
        }

        public override string ToString()
        {
            return this.value;
        }
    }
}

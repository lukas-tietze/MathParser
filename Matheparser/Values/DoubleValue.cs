using System.Collections.Generic;

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

        public IEnumerable<IValue> AsSet
        {
            get
            {
                return new HashSet<IValue>(new[] { this });
            }
        }

        public override bool Equals(object obj)
        {
            var value = obj as DoubleValue;
            return value != null &&
                   this.value == value.value;
        }

        public override int GetHashCode()
        {
            return -1584136870 + this.value.GetHashCode();
        }

        public override string ToString()
        {
            return this.AsString;
        }
    }
}

namespace Matheparser.Values
{
    using System.Collections.Generic;
    using System.Text;
    using Matheparser.Util;

    public class ArrayValue : IValue
    {
        private readonly ListArray values;

        public ArrayValue()
        {
            this.values = new ListArray();
        }

        public ArrayValue(IEnumerable<IValue> values):
            this()
        {
            foreach (var item in values)
            {
                this.values.Add(item);
            }
        }

        public ArrayValue(IEnumerable<object> items) :
            this()
        {
            foreach (var item in items)
            {
                this.values.Add(ValueCreator.Create(item));
            }
        }

        public ValueType Type
        {
            get
            {
                return ValueType.Set;
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
                return string.Empty;
            }
        }

        public IArray AsSet
        {
            get
            {
                return this.values;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append("{ ");

            foreach (var value in this.values)
            {
                sb.Append(value);
                sb.Append(", ");
            }

            sb.Remove(sb.Length - 2, 2);
            sb.Append(" }");

            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            return obj is ArrayValue value &&
                   EqualityComparer<IArray>.Default.Equals(this.values, value.values);
        }

        public override int GetHashCode()
        {
            return 1649527923 + EqualityComparer<IArray>.Default.GetHashCode(this.values);
        }
    }
}

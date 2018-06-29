using System.Collections.Generic;
using System.Text;

namespace Matheparser.Values
{
    public class SetValue : IValue
    {
        private readonly HashSet<IValue> values;

        public SetValue(HashSet<IValue> values)
        {
            this.values = values;
        }

        public SetValue(IEnumerable<object> items)
        {
            this.values = new HashSet<IValue>();

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

        public HashSet<IValue> AsSet
        {
            get
            {
                return this.values;
            }
        }

        public void Union(IValue item)
        {
            this.values.Add(item);
        }

        public void Cut(IValue item)
        {
            this.values.Remove(item);
        }

        public void Union(SetValue other)
        {
            this.values.UnionWith(other.values);
        }

        public void Cut(SetValue other)
        {
            this.values.IntersectWith(other.values);
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
            var value = obj as SetValue;
            return value != null &&
                   EqualityComparer<HashSet<IValue>>.Default.Equals(this.values, value.values);
        }

        public override int GetHashCode()
        {
            return 1649527923 + EqualityComparer<HashSet<IValue>>.Default.GetHashCode(this.values);
        }
    }
}

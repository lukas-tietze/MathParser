using System.Collections.Generic;

namespace Matheparser.Values
{
    public class SetValue : IValue
    {
        private readonly HashSet<IValue> values;

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
    }
}

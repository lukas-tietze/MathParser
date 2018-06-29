using System.Collections.Generic;

namespace Matheparser.Values
{
    public static class ValueCreator
    {
        public static IValue Create(object value)
        {
            if (value is string stringValue)
            {
                return Create(stringValue);
            }
            else if (value is double doubleValue)
            {
                return Create(doubleValue);
            }
            else if (value is IEnumerable<object> enumerableValue)
            {
                return Create(enumerableValue);
            }
            else
            {
                return Create(value.ToString());
            }
        }

        public static IValue Create(double value)
        {
            return new DoubleValue(value);
        }

        public static IValue Create(string expression)
        {
            if (double.TryParse(expression, System.Globalization.NumberStyles.AllowDecimalPoint, config.Culture, out var res))
            {
                return new DoubleValue(res);
            }

            if (expression.StartsWith("\"") && expression.EndsWith("\""))
            {
                return new StringValue(expression);
            }

            return new Solving.Calculator().Calculate(expression);
        }

        public static IValue Create(IEnumerable<object> value)
        {
            return new ArrayValue(value);
        }
    }
}

namespace Matheparser.Values
{
    public static class ValueCreator
    {
        public static IValue Create(string expression)
        {
            if(double.TryParse(expression, out double res))
            {
                return new DoubleValue(res);
            }

            return new StringValue(expression);
        }

        public static IValue Create(double value)
        {
            return new DoubleValue(value);
        }
    }
}

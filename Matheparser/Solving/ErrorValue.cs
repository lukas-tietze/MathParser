using System;
using Matheparser.Parsing;

namespace Matheparser.Solving
{
    internal class ErrorValue : IValue
    {
        private readonly Exception exception;

        public ErrorValue(Exception p)
        {
            this.exception = p;
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
                return string.Format("{0}: {1}", this.exception.GetType().Name, this.exception.Message);
            }
        }
    }
}
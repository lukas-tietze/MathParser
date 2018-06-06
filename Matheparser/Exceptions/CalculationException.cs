using System;

namespace Matheparser.Exceptions
{
    public class CalculationException : Exception
    {
        public CalculationException(string message) : base(message)
        {
        }
    }
}

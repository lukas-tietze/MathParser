namespace Matheparser.Exceptions
{
    using System;

    internal class MissingFunctionException : Exception
    {
        public MissingFunctionException(string name):
            base(string.Format("The function {0} is not deifned.", name))
        {
        }
    }
}
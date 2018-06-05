using Matheparser.Exceptions;
using Matheparser.Values;

namespace Matheparser.Functions.DefaultFunctions.Text
{
    public sealed class Trim : IFunction
    {
        public string Name
        {
            get
            {
                return "TRIM";
            }
        }

        public IValue Eval(IValue[] parameters)
        {
            this.Validate(parameters);

            return new StringValue(parameters[0].AsString.Trim());
        }

        private void Validate(IValue[] parameters)
        {
            if (parameters.Length == 0)
            {
                throw new MissingOperandException();
            }
        }
    }
}

using Matheparser.Exceptions;
using Matheparser.Values;

namespace Matheparser.Functions.DefaultFunctions.Text
{
    public sealed class Str : IFunction
    {
        public string Name
        {
            get
            {
                return "STR";
            }
        }

        public IValue Eval(IValue[] parameters)
        {
            this.Validate(parameters);

            return new StringValue(parameters[0].ToString());
        }

        private void Validate(IValue[] parameters)
        {
            if(parameters.Length != 1)
            {
                throw new OperandNumberException();
            }
        }
    }
}

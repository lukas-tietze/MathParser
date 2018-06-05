using System;
using Matheparser.Values;
using Matheparser.Exceptions;
using Matheparser.Variables;

namespace Matheparser.Functions.DefaultFunctions.Util
{
    public sealed class Set : IFunction
    {
        public string Name
        {
            get
            {
                return "SET";
            }
        }

        public IValue Eval(IValue[] parameters)
        {
            this.Validate(parameters);

            var var = VariableManager.Instance.GetVariable(parameters[0].AsString).Value = parameters[1];

            return parameters[1];
        }

        private void Validate(IValue[] parameters)
        {
            if (parameters.Length != 2)
            {
                throw new OperandNumberException();
            }

            if (parameters[0].Type != Values.ValueType.String)
            {
                throw new WrongOperandTypeException();
            }
        }
    }
}

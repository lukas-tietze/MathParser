using Matheparser.Exceptions;

namespace Matheparser.Functions.DefaultFunctions.Math
{
    public abstract class OneParamaterMathFunctionBase : IFunction
    {
        public abstract string Name
        {
            get;
        }

        public IValue Eval(IValue[] parameters)
        {
            throw new System.NotImplementedException();
        }

        protected virtual void Validate(IValue[] parameters)
        {
            if (parameters.Length != 1)
            {
                throw new MissingOperandException();
            }

            if (parameters[0].Type != ValueType.Number)
            {
                throw new WrongOperandTypeException();
            }
        }
    }
}

using Matheparser.Exceptions;
using Matheparser.Solving;
using Matheparser.Values;

namespace Matheparser.Functions.DefaultFunctions.Util
{
    class Eval : IFunction
    {
        public string Name
        {
            get
            {
                return "EVAL";
            }
        }

        IValue IFunction.Eval(IValue[] parameters)
        {
            this.Validate(parameters);

            var solver = new Solver();
            return solver.Solve(parameters[0].AsString);
        }

        private void Validate(IValue[] parameters)
        {
            if (parameters.Length != 1)
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matheparser.Exceptions;
using Matheparser.Values;

namespace Matheparser.Functions.DefaultFunctions.Text
{
    public abstract class OneParameterStringAnalysisFunction : IFunction
    {
        public abstract string Name
        {
            get;
        }

        public IValue Eval(IValue[] parameters)
        {
            this.Validate(parameters);

            return new DoubleValue(this.Eval(parameters[0].AsString));
        }

        protected abstract double Eval(string arg);

        protected virtual void Validate(IValue[] parameters)
        {
            if (parameters.Length == 0)
            {
                throw new OperandNumberException();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matheparser.Exceptions;
using Matheparser.Values;

namespace Matheparser.Functions.DefaultFunctions.Util
{
    public class Aggregate : IFunction
    {
        public string Name
        {
            get
            {
                return "AGGREGATE";
            }
        }

        public IValue Eval(IValue[] parameters)
        {
            return null;
        }
    }
}

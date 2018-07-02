using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Matheparser.Values;

namespace Matheparser.Functions.DefaultFunctions.Util
{
    public class MakeSet : FunctionBase
    {
        public override string Name
        {
            get
            {
                return "MKSET";
            }
        }

        public override IValue Eval(IValue[] parameters)
        {
            return new ArrayValue(parameters);
        }
    }
}

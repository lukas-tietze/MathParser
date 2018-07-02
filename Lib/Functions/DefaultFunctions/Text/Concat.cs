using System.Text;
using Matheparser.Values;

namespace Matheparser.Functions.DefaultFunctions.Text
{
    public sealed class Concat : FunctionBase
    {
        public override string Name
        {
            get
            {
                return "CONCAT";
            }
        }

        public override IValue Eval(IValue[] parameters)
        {
            var sb = new StringBuilder();

            foreach (var parameter in parameters)
            {
                sb.Append(parameter.AsString);
            }

            return new StringValue(sb.ToString());
        }
    }
}

using System.Text;
using Matheparser.Values;

namespace Matheparser.Functions.DefaultFunctions.Text
{
    public sealed class Concat : IFunction
    {
        public string Name
        {
            get
            {
                return "CONCAT";
            }
        }

        public IValue Eval(IValue[] parameters)
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

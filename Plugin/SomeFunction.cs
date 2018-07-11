using Matheparser.Functions;
using Matheparser.Values;

namespace Plugin
{
    public class SomeFunction : FunctionBase
    {
        public override string Name
        {
            get
            {
                return "FOO";
            }
        }

        public override IValue Eval(IValue[] parameters)
        {
            return new StringValue("Foo");
        }
    }
}
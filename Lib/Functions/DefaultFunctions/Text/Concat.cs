namespace Matheparser.Functions.DefaultFunctions.Text
{
    using System.Text;
    using Matheparser.Values;
    using Matheparser.Exceptions;

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
            this.Validate(parameters);

            var sb = new StringBuilder();

            foreach (var parameter in parameters)
            {
                sb.Append(parameter.AsString);
            }

            return new StringValue(sb.ToString());
        }

        private Mode Validate(IValue[] parameters)
        {
            if (parameters.Length < 2)
            {
                throw new OperandNumberException();
            }

            var stringFound = false;
            var setFound = false;

            foreach (var parameter in parameters)
            {
                if (parameter.Type == ValueType.Number ||
                    (parameter.Type == ValueType.String && setFound) ||
                    (parameter.Type == ValueType.Set && stringFound))
                {
                    throw new WrongOperandTypeException();
                }

                stringFound = stringFound || parameter.Type == ValueType.String;
                setFound = setFound || parameter.Type == ValueType.Set;
            }

            if(stringFound)
            {
                return Mode.String;
            }

            if(setFound)
            {
                return Mode.Set;
            }

            throw new System.NotSupportedException();
        }

        private enum Mode
        {
            String,
            Set
        }
    }
}

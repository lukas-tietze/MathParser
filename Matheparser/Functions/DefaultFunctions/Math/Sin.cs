namespace Matheparser.Functions.DefaultFunctions.Math
{
    public sealed class Sin : IFunction
    {
        public string Name
        {
            get
            {
                return "SIN";
            }
        }

        public IValue Eval(IValue[] parameters)
        {
            throw new System.NotImplementedException();
        }

        public bool Validate()
        {
            throw new System.NotImplementedException();
        }
    }
}

namespace Matheparser.Functions
{
    using System.Collections.Generic;
    using Matheparser.Functions.DefaultFunctions.Math;

    public class FunctionManager
    {
        private static FunctionManager instance;

        private Dictionary<string, IFunction> functions;

        private FunctionManager()
        {
            this.functions = new Dictionary<string, IFunction>();
        }

        public static FunctionManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FunctionManager();

                    instance.Define(new Rand());
                }

                return instance;
            }
        }

        public void Define(IFunction function)
        {
            this.functions.Add(function.Name, function);
        }

        public IFunction FindByName(string name)
        {
            if (this.functions.TryGetValue(name, out var function))
            {
                return function;
            }

            throw new MissingFunctionException(name);
        }

        public void Clear()
        {
            this.functions.Clear();
        }
    }
}

namespace Matheparser.Functions
{
    using System;
    using System.Collections.Generic;
    using Matheparser.Exceptions;
    using Matheparser.Functions.DefaultFunctions.Calculations;

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

                    foreach (var type in typeof(FunctionManager).Assembly.GetTypes())
                    {
                        if (typeof(IFunction).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                        {
                            instance.Define((IFunction)Activator.CreateInstance(type));
                        }
                    }
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

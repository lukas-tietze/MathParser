namespace Matheparser.Functions
{
    using System.Collections.Generic;

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
            return this.functions[name];
        }

        public void Clear()
        {
            this.functions.Clear();
        }
    }
}

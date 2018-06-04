using System.Collections.Generic;
using Matheparser.Values;

namespace Matheparser.Variables
{
    public class VariableManager
    {
        private static VariableManager instance;

        private Dictionary<string, IVariable> variables;
        private MissingVariabeMode missingVariabeMode;

        private VariableManager()
        {
            this.variables = new Dictionary<string, IVariable>();
            this.missingVariabeMode = MissingVariabeMode.Error;
        }

        public static VariableManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new VariableManager();
                }

                return instance;
            }
        }

        public void Remove(string key)
        {
            this.variables.Remove(key);
        }

        public void Define(IVariable variable)
        {
            this.variables.Add(variable.Name, variable);
        }

        public IVariable GetVariable(string name)
        {
            if (this.variables.TryGetValue(name, out var res))
            {
                return this.variables[name];
            }

            throw new UndefinedVariableException(name);
        }

        public bool IsDefined(string name)
        {
            return this.variables.ContainsKey(name);
        }

        public IValue GetValue(string name)
        {
            if (this.variables.TryGetValue(name, out var res))
            {
                return res.Value;
            }

            throw new UndefinedVariableException(name);
        }

        public enum MissingVariabeMode
        {
            Error,
            ReturnDefaultValue,
        }
    }
}

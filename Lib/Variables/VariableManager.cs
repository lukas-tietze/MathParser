using System;
using System.Collections.Generic;
using Matheparser.Values;

namespace Matheparser.Variables
{
    public class VariableManager
    {
        private Dictionary<string, IVariable> variables;
        private MissingVariabeMode missingVariabeMode;

        public VariableManager() :
            this(false)
        { }

        public VariableManager(bool setDefaultVariables)
        {
            this.variables = new Dictionary<string, IVariable>();
            this.missingVariabeMode = MissingVariabeMode.Error;

            if(setDefaultVariables)
            {
                this.Define(new Variable("E", Math.E));
                this.Define(new Variable("PI", Math.PI));
            }
        }

        public void Remove(string key)
        {
            this.variables.Remove(key);
        }

        public void Define(IVariable variable)
        {
            this.variables[variable.Name] = variable;
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

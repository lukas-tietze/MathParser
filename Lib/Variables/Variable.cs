using Matheparser.Values;

namespace Matheparser.Variables
{
    public class Variable : IVariable
    {
        private string name;
        private IValue value;

        public Variable(string name, double value) :
            this(name, ValueCreator.Create(value))
        {
        }

        public Variable(string name, string expression) :
            this(name, ValueCreator.Create(expression))
        {
        }

        public Variable(string name, IValue value)
        {
            this.name = name;
            this.value = value;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public ValueType Type
        {
            get
            {
                return this.value.Type;
            }
        }

        public IValue Value
        {
            get
            {
                return this.value;
            }

            set
            {
                this.value = value;
            }
        }
    }
}

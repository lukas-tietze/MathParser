using System.Collections.Generic;

namespace Matheparser.Parsing.Expressions
{
    public abstract class ExpressionNodeBase : IExpressionNode
    {
        private List<ExpressionNodeBase> childNodes;
        private ExpressionNodeBase parent;

        public ExpressionNodeBase(ExpressionNodeBase parent)
            : this()
        {
            this.parent = parent;
        }

        public ExpressionNodeBase()
        {
            this.childNodes = new List<ExpressionNodeBase>();
            this.parent = null;
        }

        public abstract ValueType Type { get; }

        public abstract IValue Eval();

        public abstract bool Validate();

        public ExpressionNodeBase Parent
        {
            get
            {
                return this.parent;
            }
        }

        public void AddChild(ExpressionNodeBase node)
        {
            this.childNodes.Add(node);
            node.parent = this;
        }

        public ExpressionNodeBase ReplaceLastChild(ExpressionNodeBase newLastChild)
        {
            if (this.childNodes.Count == 0)
            {
                throw new System.InvalidOperationException();
            }

            var oldLastChild = this.childNodes[this.childNodes.Count - 1];
            this.childNodes.RemoveAt(this.childNodes.Count - 1);

            this.AddChild(newLastChild);

            oldLastChild.parent = null;
            return oldLastChild;
        }

        protected IList<ExpressionNodeBase> ChildNodes
        {
            get
            {
                return this.childNodes;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matheparser.Parsing.PostFixExpressions
{
    public abstract class BinaryOperatorExpressionBase : IPostFixExpression
    {
        public PostFixExpressionType Type
        {
            get
            {
                return PostFixExpressionType.Operator;
            }
        }

        public IValue Eval(IValue[] operands)
        {
            this.Validate(operands);

            if (operands[0].Type == ValueType.Number)
            {
                return this.EvalNumber(operands[0].AsDouble, operands[1].AsDouble);
            }

            return this.EvalString(operands[0].AsString, operands[1].AsString);
        }

        internal abstract IValue EvalString(string string1, string string2);
        internal abstract IValue EvalNumber(double double1, double double2);

        private void Validate(IValue[] operands)
        {
            if (operands.Length != 2)
            {
                throw new MissingOperandException();
            }

            if (operands[0].Type != operands[1].Type)
            {
                throw new WrongOperandTypeException();
            }
        }
    }
}

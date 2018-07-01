using Matheparser.Exceptions;
using Matheparser.Parsing;
using Matheparser.Parsing.Evaluation;
using Matheparser.Tokenizing;
using Matheparser.Values;
using Matheparser.Variables;

namespace Matheparser.Functions.DefaultFunctions.Util
{
    public class Aggregate : FunctionBase
    {
        public override string Name
        {
            get
            {
                return "AGGREGATE";
            }
        }

        public override IValue Eval(IValue[] parameters)
        {
            this.Validate(parameters);

            var add = parameters[0].AsString == "+";
            var count = (int)parameters[1].AsDouble;
            var res = 0.0;
            var evaluator = default(PostFixEvaluator);
            var counter = new Variable(parameters[2].AsString, 0);
            var config = ConfigBase.DefaultConfig;
            this.Context.VariableManager.Define(counter);

            try
            {
                var tokenizer = new Tokenizer(parameters[3].AsString, config);
                tokenizer.Run();
                var parser = new Parser(tokenizer.Tokens, config);
                evaluator = new PostFixEvaluator(parser.CreatePostFixExpression(), config);
            }
            catch (System.Exception e)
            {
                throw new OperandEvaluationException();
            }

            for (var i = 0; i < count; i++)
            {
                counter.Value = ValueCreator.Create(i);
                var aggregate = evaluator.Run();

                if (aggregate.Type != ValueType.Number)
                {
                    throw new OperandEvaluationException();
                }

                if (add)
                {
                    res += aggregate.AsDouble;
                }
                else
                {
                    res *= aggregate.AsDouble;
                }
            }

            this.Context.VariableManager.Remove(counter.Name);

            return new DoubleValue(res);
        }

        private void Validate(IValue[] parameters)
        {
            if (parameters.Length != 4)
            {
                throw new OperandNumberException();
            }

            if (parameters[0].Type != ValueType.String ||
                parameters[1].Type != ValueType.Number ||
                parameters[2].Type != ValueType.String ||
                parameters[3].Type != ValueType.String)
            {
                throw new WrongOperandTypeException();
            }

            if (!parameters[0].AsString.Equals("+") && !parameters[0].AsString.Equals("*"))
            {
                throw new OperandEvaluationException();
            }
        }
    }
}

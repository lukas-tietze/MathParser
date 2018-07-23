namespace Matheparser.Functions.DefaultFunctions.Set
{
    using Matheparser.Exceptions;
    using Matheparser.Parsing;
    using Matheparser.Parsing.Evaluation;
    using Matheparser.Tokenizing;
    using Matheparser.Util;
    using Matheparser.Values;
    using Matheparser.Variables;

    public sealed class Foreach : FunctionBase
    {
        public override string Name
        {
            get
            {
                return "FOREACH";
            }
        }

        public override IValue Eval(IValue[] parameters)
        {
            var mode = this.Validate(parameters);

            var set = parameters[0].AsSet;
            var res = new ListArray();
            var i = new Variable(parameters[1].AsString, new DoubleValue(0));

            this.Context.VariableManager.Define(i);

            switch (mode)
            {
                case Mode.Eval:

                    var evaluator = default(PostFixEvaluator);
                    try
                    {
                        var tokenizer = new Tokenizer(parameters[2].AsString, this.Context.Config);
                        tokenizer.Run();
                        var parser = new Parser(tokenizer.Tokens, this.Context.Config);
                        evaluator = new PostFixEvaluator(parser.CreatePostFixExpression(), this.Context);
                    }
                    catch (System.Exception)
                    {
                        throw new OperandEvaluationException();
                    }

                    foreach (var item in set)
                    {
                        i.Value = item;
                        res.Add(evaluator.Run());
                    }
                    break;
                case Mode.Copy:
                    foreach (var item in set)
                    {
                        i.Value = item;
                        res.Add(ValueHelper.Copy(parameters[2]));
                    }
                    break;
                default:
                    throw new System.NotSupportedException();
            }

            return new ArrayValue(res);
        }

        private Mode Validate(IValue[] parameters)
        {
            if (parameters.Length != 3)
            {
                throw new OperandNumberException();
            }

            if (parameters[0].Type != ValueType.Set ||
                parameters[1].Type != ValueType.String)
            {
                throw new WrongOperandTypeException();
            }

            if (parameters[2].Type == ValueType.String)
            {
                return Mode.Eval;
            }

            return Mode.Copy;
        }

        private enum Mode
        {
            Eval,
            Copy,
        }
    }
}

using System;
using Matheparser;
using Matheparser.Exceptions;
using Matheparser.Functions;
using Matheparser.Parsing;
using Matheparser.Solving;
using Matheparser.Tokenizing;
using Matheparser.Values;
using Matheparser.Variables;

namespace TestTerminal
{
    public static class Program
    {
        private static CalculationContext context;

        [STAThread]
        public static void Main(string[] args)
        {
            context = new CalculationContext(new VariableManager(true), new FunctionManager(true), ConfigBase.DefaultConfig);

            var quit = false;

            while (!quit)
            {
                var input = Console.ReadLine();

                try
                {
                    if (input.StartsWith(":"))
                    {
                        SplitKeyValue(input, out var command, out var expression);

                        switch (command.Substring(1))
                        {
                            case "tokenize":
                                Tokenize(expression);
                                break;
                            case "parse":
                                Parse(expression);
                                break;
                            case "def":
                            case "define":
                            case "defvar":
                                Define(expression, true);
                                break;
                            case "defexp":
                                Define(expression, false);
                                break;
                            case "undef":
                                Undefine(expression);
                                break;
                            case "quit":
                                quit = true;
                                break;
                            case "solve":
                                Solve(expression);
                                break;
                            default:
                                Console.WriteLine("Undefined command \"{0}\"", command);
                                break;
                        }
                    }
                    else
                    {
                        Solve(input);
                    }

                }
                catch (Exception e)
                {
                    HandleException(e);
                }
            }
        }

        private static void HandleException(Exception e)
        {
            if (e.GetType().Namespace.StartsWith(nameof(Matheparser)))
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
            else
            {
                Console.WriteLine("Unhandled Exception of Type {0}: \"{1}\"", e.GetType().Name, e.Message);
            }
        }

        private static void Undefine(string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                expression = QueryInput();
            }

            context.VariableManager.Remove(expression.Trim().ToLower());
        }

        private static void SplitKeyValue(string input, out string key, out string value)
        {
            key = input;
            value = string.Empty;

            var keyStart = 0;
            var keyEnd = 0;

            while (keyStart < input.Length && char.IsWhiteSpace(input[keyStart]))
            {
                keyStart++;
            }

            keyEnd = keyStart + 1;

            while (keyEnd < input.Length && !char.IsWhiteSpace(input[keyEnd]))
            {
                keyEnd++;
            }

            if (keyEnd < input.Length && keyStart < keyEnd)
            {
                key = input.Substring(keyStart, keyEnd - keyStart);
                value = input.Substring(keyEnd);
            }

            key = key.Trim().ToLower();
        }

        private static string QueryInput()
        {
            return QueryInput("> Input Expression:");
        }

        private static string QueryInput(string question)
        {
            Console.WriteLine(question);
            return Console.ReadLine();
        }

        private static void Define(string expression, bool compress)
        {
            if (string.IsNullOrEmpty(expression))
            {
                expression = QueryInput();
            }

            var key = string.Empty;
            var value = string.Empty;

            SplitKeyValue(expression, out key, out value);

            if (string.IsNullOrEmpty(value))
            {
                Console.WriteLine("> Missing value!");
                return;
            }

            var res = default(IValue);

            if (compress)
            {
                try
                {
                    res = new Calculator().Calculate(value);
                }
                catch (CalculationException)
                {
                    res = new ExpressionValue(context, value);
                }
            }
            else
            {
                res = new ExpressionValue(context, value);
            }

            var newVar = new Variable(key, res);

            context.VariableManager.Define(newVar);

            Console.WriteLine("> Defined {0} as {1}.", newVar.Name, newVar.Value);
        }

        private static void Solve(string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                expression = QueryInput();
            }

            var calculator = new Calculator();
            var res = calculator.Calculate(expression);
            Console.WriteLine("> {0}", res.ToString());
        }

        private static void Parse(string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                expression = QueryInput();
            }

            var tokenizer = new Tokenizer(expression, ConfigBase.DefaultConfig);
            tokenizer.Run();
            var parser = new Parser(tokenizer.Tokens, ConfigBase.DefaultConfig);
            var postFix = parser.CreatePostFixExpression();

            for (var i = 0; i < postFix.Count; i++)
            {
                Console.Write("({0})", postFix[i].ToString());

                if (i != postFix.Count - 1)
                {
                    Console.Write(", ");
                }
                else
                {
                    Console.WriteLine();
                }
            }
        }

        private static void Tokenize(string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                expression = QueryInput();
            }

            var tokenizer = new Tokenizer(expression, ConfigBase.DefaultConfig);
            tokenizer.Run();

            if (tokenizer.Tokens.Count > 0)
            {
                for (var i = 0; i < tokenizer.Tokens.Count; i++)
                {
                    Console.Write(string.Format("({0}:{1})", tokenizer.Tokens[i].Type, tokenizer.Tokens[i].Value));

                    if (i != tokenizer.Tokens.Count - 1)
                    {
                        Console.Write(", ");
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}

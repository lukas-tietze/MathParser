using System;
using System.Collections.Generic;
using Matheparser;
using Matheparser.Parsing;
using Matheparser.Solving;
using Matheparser.Tokenizing;
using Matheparser.Variables;

namespace TerminalTest
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var quit = false;

            while (!quit)
            {
                var command = string.Empty;
                var expression = string.Empty;

                SplitKeyValue(Console.ReadLine(), out command, out expression);

                try
                {
                    switch (command)
                    {
                        case "tokenize":
                            Tokenize(expression);
                            break;
                        case "parse":
                            Parse(expression);
                            break;
                        case "solve":
                            Solve(expression);
                            break;
                        case "def":
                        case "define":
                            Define(expression);
                            break;
                        case "undef":
                            Undefine(expression);
                            break;
                        case "quit":
                            quit = true;
                            break;
                        default:
                            Console.WriteLine("Unknown command!");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unhandled Exception of Type {0}: \"{1}\"", e.GetType().Name, e.Message);
                }
            }
        }

        private static void Undefine(string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                expression = QueryInput();
            }

            VariableManager.Instance.Remove(expression.Trim().ToLower());
        }

        private static void SplitKeyValue(string input, out string key, out string value)
        {
            var keyStart = 0;
            var keyEnd = 0;

            for (keyStart = 0; keyStart < input.Length; keyStart++)
            {
                if (!char.IsWhiteSpace(input[keyStart]))
                {
                    break;
                }
            }

            for (keyEnd = keyStart + 1; keyEnd < input.Length; keyEnd++)
            {
                if (char.IsWhiteSpace(input[keyEnd]))
                {
                    break;
                }
            }

            if (keyStart >= keyEnd && keyStart >= input.Length)
            {
                key = string.Empty;
                value = string.Empty;
                return;
            }

            key = input.Substring(keyStart, keyEnd - keyStart).Trim().ToLower();
            value = string.Empty;

            if (keyEnd < input.Length - 1)
            {
                value = input.Substring(keyEnd).Trim();
            }
        }

        private static string QueryInput()
        {
            return QueryInput("Input Expression:");
        }

        private static string QueryInput(string question)
        {
            Console.WriteLine(question);
            return Console.ReadLine();
        }

        private static void Define(string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                expression = QueryInput();
            }

            var key = string.Empty;
            var value = string.Empty;

            SplitKeyValue(expression, out key, out value);

            if (string.IsNullOrEmpty(key))
            {
                Console.WriteLine("Missing Key!");
                return;
            }


            VariableManager.Instance.Define(new Variable(key, value));
        }

        private static void Solve(string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                expression = QueryInput();
            }

            var solver = new Solver();
            var res = solver.Solve(expression);
            Console.WriteLine(res.ToString());
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

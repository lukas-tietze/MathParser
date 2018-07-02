namespace Terminal
{
    using System;
    using System.IO;
    using Matheparser;
    using Matheparser.Exceptions;
    using Matheparser.Functions;
    using Matheparser.Parsing;
    using Matheparser.Solving;
    using Matheparser.Tokenizing;
    using Matheparser.Values;
    using Matheparser.Variables;

    public static class Program
    {
        private static CalculationContext context;
        private static TextWriter outWriter;
        private static TextWriter errWriter;

        [STAThread]
        public static void Main(string[] args)
        {
            context = new CalculationContext(new VariableManager(true), new FunctionManager(true), ConfigBase.DefaultConfig);
            outWriter = Console.Out;
            errWriter = Console.Error;

            foreach (var arg in args)
            {
                Eval(arg);
            }

            var quit = false;

            while (!quit)
            {
                quit = Eval(Console.ReadLine());
            }

            CloseStream(outWriter);
            CloseStream(errWriter);
        }

        private static bool Eval(string input)
        {
            var quit = false;

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
                        case "load":
                            Load(expression);
                            break;
                        case "err":
                            CloseStream(errWriter);
                            errWriter = Open(expression);
                            break;
                        case "out":
                            CloseStream(outWriter);
                            outWriter = Open(expression);
                            break;
                        default:
                            ErrLine("Undefined command \"{0}\"", command);
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

            return quit;
        }

        private static void CloseStream(TextWriter writer)
        {
            writer.Flush();
            writer.Close();
            writer.Dispose();
        }
        private static void Out(string format, params object[] args)
        {
            outWriter.Write(format, args);
        }
        private static void OutLine(string format, params object[] args)
        {
            outWriter.WriteLine(format, args);
        }

        private static void OutLine()
        {
            outWriter.WriteLine();
        }

        private static void Err(string format, params object[] args)
        {
            errWriter.Write(format, args);
        }

        private static void ErrLine(string format, params object[] args)
        {
            errWriter.WriteLine(format, args);
        }

        private static void ErrLine()
        {
            errWriter.WriteLine();
        }

        private static TextWriter Open(string expression)
        {
            if ("<stdout>".Equals(expression.ToLower()))
            {
                return Console.Out;
            }

            if ("<stderr>".Equals(expression.ToLower()))
            {
                return Console.Error;
            }

            return new StreamWriter(new FileStream(expression.Trim(), FileMode.Append, FileAccess.Write));
        }

        private static void Load(string expression)
        {
            var allLines = File.ReadAllLines(expression.Trim());

            foreach (var line in allLines)
            {
                Eval(line);
            }
        }

        private static void HandleException(Exception e)
        {
            if (e.GetType().Namespace.StartsWith(nameof(Matheparser)))
            {
                ErrLine("Error: {0}", e.Message);
            }
            else
            {
                ErrLine("Unhandled Exception of Type {0}: \"{1}\"", e.GetType().Name, e.Message);
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
                value = input.Substring(keyEnd + 1);
            }

            key = key.Trim().ToLower();
        }

        private static string QueryInput()
        {
            return QueryInput("> Input Expression:");
        }

        private static string QueryInput(string question)
        {
            OutLine(question);
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
                OutLine("> Missing value!");
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

            OutLine("> Defined {0} as {1}.", newVar.Name, newVar.Value);
        }

        private static void Solve(string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                expression = QueryInput();
            }

            var calculator = new Calculator();
            var res = calculator.Calculate(expression);
            OutLine("> {0}", res.ToString());
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
                Out("({0})", postFix[i].ToString());

                if (i != postFix.Count - 1)
                {
                    Out(", ");
                }
                else
                {
                    OutLine();
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
                    Out(string.Format("({0}:{1})", tokenizer.Tokens[i].Type, tokenizer.Tokens[i].Value));

                    if (i != tokenizer.Tokens.Count - 1)
                    {
                        Out(", ");
                    }
                    else
                    {
                        OutLine();
                    }
                }
            }
        }
    }
}

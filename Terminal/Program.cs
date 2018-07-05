﻿namespace Terminal
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Matheparser;
    using Matheparser.Exceptions;
    using Matheparser.Functions;
    using Matheparser.Parsing;
    using Matheparser.Solving;
    using Matheparser.Tokenizing;
    using Matheparser.Values;
    using Matheparser.Variables;
    using Matheparser.Util;

    public static class Program
    {
        private static CalculationContext context;
        private static string workingDirectory;
        private static Dictionary<string, Tuple<Func<string, bool>, string>> actions;
        private static Dictionary<string, string> aliases;

        [STAThread]
        public static void Main(string[] args)
        {
            context = new CalculationContext(new VariableManager(true), new FunctionManager(true), ConfigBase.DefaultConfig);
            workingDirectory = Directory.GetCurrentDirectory();
            actions = new Dictionary<string, Tuple<Func<string, bool>, string>>
            {
                {
                    "tokenize",
                    new Tuple<Func<string, bool>, string>(
                    (expression) => {
                        Tokenize(expression);
                        return false;
                    },
                    "")
                },

                {
                    "parse",
                    new Tuple<Func<string, bool>, string>(
                    (expression) => {
                        Parse(expression);
                        return false;
                    },
                    "")
                },

                {
                    "def",
                    new Tuple<Func<string, bool>, string>(
                    (expression) => {
                        Define(expression, true);
                        return false;
                    },
                    "")
                },

                {
                    "exp",
                    new Tuple<Func<string, bool>, string>(
                    (expression) => {
                        Define(expression, false);
                        return false;
                    },
                    "")
                },

                {
                    "undef",
                    new Tuple<Func<string, bool>, string>(
                    (expression) => {
                        Undefine(expression);
                        return false;
                    },
                    "")
                },

                {
                    "quit",
                    new Tuple<Func<string, bool>, string>(
                    (expression) => {
                        return true;
                    },
                    "")
                },

                {
                    "solve",
                    new Tuple<Func<string, bool>, string>(
                    (expression) => {
                        Solve(expression);
                        return false;
                    },
                    "")
                },

                {
                    "load",
                    new Tuple<Func<string, bool>, string>(
                    (expression) => {
                        return Load(expression);
                    },
                    "")
                },

                {
                    "err",
                    new Tuple<Func<string, bool>, string>(
                    (expression) => {
                        Console.SetError(OpenOut(expression));
                        return false;
                    },
                    "")
                },

                {
                    "out",
                    new Tuple<Func<string, bool>, string>(
                    (expression) => {
                        Console.SetOut(OpenOut(expression));
                        return false;
                    },
                    "")
                },

                {
                    "in",
                    new Tuple<Func<string, bool>, string>(
                    (expression) => {
                        Console.SetIn(OpenIn(expression));
                        return false;
                    },
                    "")
                },

                {
                    "dir",
                    new Tuple<Func<string, bool>, string>(
                    (expression) => {
                        ChangeDir(expression);
                        return false;
                    },
                    "")
                },

                {
                    "promt",
                    new Tuple<Func<string, bool>, string>(
                    (expression) => {
                        SetPrompt(expression);
                        return false;
                    },
                    "")
                },

                {
                    "clear",
                    new Tuple<Func<string, bool>, string>(
                    (expression) => {
                        Console.Clear();
                        return false;
                    },
                    "")
                },

                {
                    "files",
                    new Tuple<Func<string, bool>, string>(
                    (expression) => {
                        ListFiles();
                        return false;
                    },
                    "")
                },

                {
                    "clearvars",
                    new Tuple<Func<string, bool>, string>(
                    (expression) => {
                        ClearVars(expression);
                        return false;
                    },
                    "")
                },

                {
                    "help",
                    new Tuple<Func<string, bool>, string>(
                    (expression) => {
                        ShowHelp(expression);
                        return false;
                    },
                    "")
                },
            };

            aliases = new Dictionary<string, string>()
            {
                // {"", ""},
                 {"def", "!"},
                  {"help", "?"},
            };

            Console.Clear();

            foreach (var kvp in aliases)
            {
                if (actions.ContainsKey(kvp.Key))
                {
                    actions.Add(kvp.Value, actions[kvp.Key]);
                }
            }

            var quit = false;

            for (var i = 0; i < args.Length && !quit; i++)
            {
                quit = Eval(args[i]);
            }

            while (!quit)
            {
                quit = Eval(Console.ReadLine());
            }
        }

        private static void ShowHelp(string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                Console.WriteLine("---- Help ----");
                Console.WriteLine("Type the expression you want to Calculate and press enter.");
                Console.WriteLine("Lines with the syntax");
                Console.WriteLine("\t:<command> <arg>");
                Console.WriteLine("will be interpreted as commands, instead of calculations. (Not all commands require an argument)");
                Console.WriteLine();
                Console.WriteLine("Possible Commands are:");

                foreach (var action in actions)
                {
                    Console.WriteLine("\t{0} - {1}", action.Key, action.Value.Item2);
                }
            }
            else if (context.VariableManager.IsDefined(expression))
            {
                Console.WriteLine("{0} is a variable:\n{1}", expression, context.VariableManager.GetVariable(expression));
            }
            else if (context.FunctionManager.IsDefined(expression))
            {
                Console.WriteLine("{0} is a function:\n{1}", expression, context.FunctionManager.FindByName(expression));
            }
            else if (actions.ContainsKey(expression))
            {
                Console.WriteLine("{0} is an command:\n{1}", expression, actions[expression].Item2);
            }
            else
            {
                Console.WriteLine("There is no help topic for {0}. Use :help for general help and check your spelling.", expression);
            }
        }

        private static void ClearVars(string expression)
        {
            if ("all".Equals(expression.Trim().ToLower()))
            {
                context.VariableManager.ClearAll();
            }
            else
            {
                context.VariableManager.ClearUserVariables();
            }
        }

        private static bool Eval(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            var quit = false;

            try
            {
                if (input.StartsWith(":"))
                {
                    SplitKeyValue(input, out var command, out var expression);

                    if (actions.TryGetValue(command.Substring(1), out var func))
                    {
                        quit = func.Item1(expression);
                    }
                    else
                    {
                        HandleUndefinedCommand(command);
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

        private static List<string> FindSimilarCommands(string command)
        {
            var res = new List<string>();

            foreach (var existingCommand in actions.Keys)
            {
                if (existingCommand.Contains(command) ||
                    command.Contains(existingCommand) ||
                    OsaDistance(existingCommand, command) < 3)
                {
                    res.Add(existingCommand);
                }
            }

            return res;
        }

        private static void HandleUndefinedCommand(string command)
        {
            if (string.IsNullOrEmpty(command))
            {
                Console.Error.WriteLine("Please enter a command");

                foreach (var existingCommand in actions.Keys)
                {
                    Console.WriteLine("\t{0}", existingCommand);
                }
            }
            else
            {
                var matches = FindSimilarCommands(command);

                Console.Error.WriteLine("Undefined command \"{0}\"", command);

                if (matches.Count > 0)
                {
                    Console.WriteLine("Possible similar commands:");

                    foreach (var match in matches)
                    {
                        Console.WriteLine("\t{0}", match);
                    }
                }
            }
        }

        private static void ListFiles()
        {
            var directories = Directory.GetDirectories(workingDirectory);
            var files = Directory.GetFiles(workingDirectory);

            Array.Sort(directories);
            Array.Sort(files);

            foreach (var directory in directories)
            {
                Console.WriteLine(Path.GetFileName(directory) + "/");
            }

            foreach (var file in files)
            {
                Console.WriteLine(Path.GetFileName(file));
            }
        }

        private static void ChangeDir(string expression)
        {
            if (!string.IsNullOrEmpty(expression))
            {
                workingDirectory = Path.GetFullPath(Path.Combine(workingDirectory, expression));
            }

            Console.WriteLine("current directory is \"{0}\"", workingDirectory);
        }

        private static void SetPrompt(string expression)
        {

        }

        private static TextReader OpenIn(string expression)
        {
            if ("<stdin>".Equals(expression))
            {
                Console.OpenStandardInput();
                return Console.In;
            }

            return new StreamReader(new FileStream(expression.Trim(), FileMode.OpenOrCreate, FileAccess.Read));
        }

        private static TextWriter OpenOut(string expression)
        {
            if ("<stdout>".Equals(expression.ToLower()))
            {
                Console.OpenStandardOutput();
                return Console.Out;
            }

            if ("<stderr>".Equals(expression.ToLower()))
            {
                Console.OpenStandardInput();
                return Console.Error;
            }

            return new StreamWriter(new FileStream(expression.Trim(), FileMode.Append, FileAccess.Write));
        }

        private static bool Load(string expression)
        {
            var allLines = File.ReadAllLines(Path.GetFullPath(Path.Combine(workingDirectory, expression.Trim())));
            var quit = false;

            for (var i = 0; i < allLines.Length && !quit; i++)
            {
                quit = Eval(allLines[i]);
            }

            return quit;
        }

        private static void HandleException(Exception e)
        {
            if (e.GetType().Namespace.StartsWith(nameof(Matheparser)))
            {
                Console.Error.WriteLine("Error: {0}", e.Message);
            }
            else
            {
                Console.Error.WriteLine("Unhandled Exception of Type {0}: \"{1}\"", e.GetType().Name, e.Message);
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
                    res = new Calculator(context).Calculate(value);
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

            var calculator = new Calculator(context);
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

        private static int OsaDistance(string s1, string s2)
        {
            var d = new int[s1.Length + 1, s2.Length + 1];

            for (var i = 0; i <= s1.Length; i++)
            {
                d[i, 0] = i;
            }

            for (var i = 0; i <= s2.Length; i++)
            {
                d[0, i] = i;
            }

            for (var i = 1; i <= s1.Length; i++)
            {
                for (var j = 1; j <= s2.Length; j++)
                {
                    var cost = s1[i - 1] == s2[j - 1] ? 0 : 1;

                    d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1,    // deletion
                                       d[i, j - 1] + 1),            // insertion
                                       d[i - 1, j - 1] + cost);     // substitution

                    if (i > 2 && j > 2 && s1[i - 1] == s2[j - 2] && s1[i - 2] == s2[j - 1])
                    {
                        d[i, j] = Math.Min(d[i, j], d[i - 2, j - 2] + cost);  // transposition
                    }
                }
            }

            return d[s1.Length, s2.Length];
        }
    }
}

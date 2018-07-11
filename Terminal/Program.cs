namespace Terminal
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
    using Matheparser.Io;
    using System.Runtime.Loader;
    using Api;
    using System.Reflection;

    public static class Program
    {
        private static CalculationContext context;
        private static string workingDirectory;
        private static Dictionary<string, TerminalAction> actions;
        private static List<TerminalAction> uniqeActions;
        private static List<IPlugin> activePlugins;

        [STAThread]
        public static void Main(string[] args)
        {
            context = new CalculationContext(new VariableManager(true), new FunctionManager(true), ConfigBase.DefaultConfig);
            workingDirectory = Directory.GetCurrentDirectory();
            activePlugins = new List<IPlugin>();
            uniqeActions = new List<TerminalAction>()
            {
                new TerminalAction {
                    Name = "tokenize",
                    Alias = "t",
                    Description = "Prints the list of tokens of the expression.",
                    Action = (expression) => {
                        Tokenize(expression);
                        return false;
                    },
                },

                new TerminalAction {
                    Name = "parse",
                    Alias = "p",
                    Description = "Parses the expression and prints the created postfix expression.",
                    Action = (expression) => {
                        Parse(expression);
                        return false;
                    },
                },

                new TerminalAction {
                    Name = "def",
                    Alias = "d",
                    Description = "Evaluates the given expression and defines a variable, which value is the result of the evaluation.",
                    Action = (expression) => {
                        Define(expression, true);
                        return false;
                    },
                },

                new TerminalAction {
                    Name = "exp",
                    Alias = "e",
                    Description = "Defines a variable that contains the given expression.",
                    Action = (expression) => {
                        Define(expression, false);
                        return false;
                    },
                },

                new TerminalAction {
                    Name = "undef",
                    Alias = "ud",
                    Description = "Removes a variable.",
                    Action = (expression) => {
                        Undefine(expression);
                        return false;
                    },
                },

                new TerminalAction {
                    Name = "quit",
                    Alias = "q",
                    Description = "Quit the application.",
                    Action = (expression) => {
                        return true;
                    },
                },

                new TerminalAction {
                    Name = "solve",
                    Alias = "s",
                    Description = "Evaluates the expression and prints the result.",
                    Action = (expression) => {
                        Solve(expression);
                        return false;
                    },
                },

                new TerminalAction {
                    Name = "load",
                    Alias = "ld",
                    Description = "load a file from the specified path and interpret each line as a command.",
                    Action = (expression) => {
                        return Load(expression);
                    },
                },

                new TerminalAction {
                    Name = "plugin",
                    Alias = "pl",
                    Description = "Loads a plugin from the specified path.",
                    Action = (expression) => {
                        return LoadPlugin(expression);
                    },
                },

                new TerminalAction
                {
                    Name = "err",
                    Alias = "|",
                    Description = "Changes the error stream to the specified file. Use :err <std> to print to console.",
                    Action = (expression) =>
                    {
                        context.Out = OpenOut(expression);
                        return false;
                    },
                },

                new TerminalAction
                {
                    Name = "out",
                    Alias = ">",
                    Description = "Changes the output stream to the specified file. Use :out <std> to print to console.",
                    Action = (expression) =>
                    {
                        context.Out = OpenOut(expression);
                        return false;
                    },
                },

                new TerminalAction
                {
                    Name = "in",
                    Alias = "<",
                    Description = "Changes the input stream. Use :in <std> to use the console.",
                    Action = (expression) =>
                    {
                        context.In = OpenIn(expression);
                        return false;
                    },
                },

                new TerminalAction
                {
                    Name = "dir",
                    Alias = "cd",
                    Description = "Changes the to specified directory and prints the current directory.",
                    Action = (expression) =>
                    {
                        ChangeDir(expression);
                        return false;
                    },
                },

                new TerminalAction
                {
                    Name = "promt",
                    Alias = "#",
                    Description = "Use the specified expression as prompt.",
                    Action = (expression) =>
                    {
                        SetPrompt(expression);
                        return false;
                    },
                },

                new TerminalAction
                {
                    Name = "clear",
                    Alias = "cl",
                    Description = "Clears the screen.",
                    Action = (expression) =>
                    {
                        context.Out.Clear();
                        return false;
                    },
                },

                new TerminalAction
                {
                    Name = "files",
                    Alias = "ls",
                    Description = "List all Files and directories in the current directory.",
                    Action = (expression) =>
                    {
                        ListFiles();
                        return false;
                    },
                },

                new TerminalAction
                {
                    Name = "clearvars",
                    Alias = "clv",
                    Description = "Delete all variables, except e and pi. Use :clearvars all to also delete e and pi.",
                    Action = (expression) =>
                    {
                        ClearVars(expression);
                        return false;
                    },
                },

                new TerminalAction
                {
                    Name = "help",
                    Alias = "h",
                    Description = "Prints the help.",
                    Action = (expression) =>
                    {
                        ShowHelp(expression);
                        return false;
                    },
                },
            };

            context.Out.Clear();

            actions = new Dictionary<string, TerminalAction>();

            foreach (var item in uniqeActions)
            {
                actions.Add(item.Name, item);

                if (!string.IsNullOrEmpty(item.Alias))
                {
                    actions.Add(item.Alias, item);
                }
            }

            var quit = false;

            for (var i = 0; i < args.Length && !quit; i++)
            {
                quit = Eval(args[i]);
            }

            while (!quit)
            {
                quit = Eval(context.In.ReadLine());
            }
        }

        private static void ShowHelp(string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                context.Out.WriteLine("---- Help ----");
                context.Out.WriteLine("Type the expression you want to Calculate and press enter.");
                context.Out.WriteLine("Lines with the syntax");
                context.Out.WriteLine("\t:<command> <arg>");
                context.Out.WriteLine("will be interpreted as commands, instead of calculations. (Not all commands require an argument)");
                context.Out.WriteLine();
                context.Out.WriteLine("Possible Commands are:");

                context.Out.BeginIndent("\t", false);

                foreach (var action in uniqeActions)
                {
                    if (action.HasAlias)
                    {
                        context.Out.WriteLine(action.Alias);
                        context.Out.WriteLine("{0} - {1}", action.Name, action.Description);
                    }
                    else
                    {
                        context.Out.WriteLine("{0} - {1}", action.Name, action.Description);
                    }
                }

                context.Out.EndIndent();
            }
            else if (context.VariableManager.IsDefined(expression))
            {
                context.Out.WriteLine("{0} is a variable:\n{1}", expression, context.VariableManager.GetVariable(expression));
            }
            else if (context.FunctionManager.IsDefined(expression))
            {
                context.Out.WriteLine("{0} is a function:\n{1}", expression, context.FunctionManager.FindByName(expression));
            }
            else if (actions.ContainsKey(expression))
            {
                context.Out.WriteLine("{0} is an command:\n{1}", expression, actions[expression].Description);
            }
            else
            {
                context.Out.WriteLine("There is no help topic for {0}. Use :help for general help and check your spelling.", expression);
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
                        quit = func.Run(expression);
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
                context.Err.WriteLine("Please enter a command");

                foreach (var existingCommand in actions.Keys)
                {
                    context.Out.WriteLine("\t{0}", existingCommand);
                }
            }
            else
            {
                var matches = FindSimilarCommands(command);

                context.Err.WriteLine("Undefined command \"{0}\"", command);

                if (matches.Count > 0)
                {
                    context.Out.WriteLine("Possible similar commands:");

                    foreach (var match in matches)
                    {
                        context.Out.WriteLine(match);
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
                context.Out.WriteLine(Path.GetFileName(directory) + "/");
            }

            foreach (var file in files)
            {
                context.Out.WriteLine(Path.GetFileName(file));
            }
        }

        private static void ChangeDir(string expression)
        {
            if (!string.IsNullOrEmpty(expression))
            {
                var newDir = Path.GetFullPath(Path.Combine(workingDirectory, expression));

                if (Directory.Exists(newDir))
                {
                    workingDirectory = newDir;
                }
                else
                {
                    context.Err.WriteLine("Can'tchange to {0}, Directory {1} doesn't exist", expression, newDir);
                }
            }

            context.Out.WriteLine("current directory is \"{0}\"", workingDirectory);
        }

        private static void SetPrompt(string expression)
        {
            context.Out.ClearIndent();
            context.Out.BeginIndent(expression, false);
        }

        private static IReader OpenIn(string expression)
        {
            if ("<std>".Equals(expression.ToLower()))
            {
                return new ConsoleReader();
            }

            if ("<off>".Equals(expression.ToLower()))
            {
                return new EmptyReader();
            }

            return new FileReader(expression.Trim());
        }

        private static IWriter OpenOut(string expression)
        {
            if ("<std>".Equals(expression.ToLower()))
            {
                return new ConsoleWriter();
            }

            if ("<off>".Equals(expression.ToLower()))
            {
                return new EmptyWriter();
            }

            return new FileWriter(expression.Trim());
        }

        private static bool LoadPlugin(string path)
        {
            var dll = default(Assembly);

            try
            {
                dll = AssemblyLoadContext.Default.LoadFromAssemblyPath(Path.GetFullPath(path));
            }
            catch (Exception e)
            {
                HandleException(e);
            }

            foreach (var type in dll.GetTypes())
            {
                if (typeof(IPlugin).IsAssignableFrom(type))
                {
                    var plugin = (IPlugin)Activator.CreateInstance(type);
                    plugin.Init(context);
                }

                if (typeof(IFunction).IsAssignableFrom(type))
                {
                    context.FunctionManager.Define((IFunction)Activator.CreateInstance(type));
                }
            }

            return false;
        }

        private static bool Load(string path)
        {
            var allLines = File.ReadAllLines(Path.GetFullPath(Path.Combine(workingDirectory, path.Trim())));
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
                context.Err.WriteLine("Error: {0}", e.Message);
            }
            else
            {
                context.Err.WriteLine("Unhandled Exception of Type {0}: \"{1}\"", e.GetType().Name, e.Message);
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

            key = key.Trim();
        }

        private static string QueryInput()
        {
            return QueryInput("> Input Expression:");
        }

        private static string QueryInput(string question)
        {
            context.Out.WriteLine(question);
            return context.In.ReadLine();
        }

        private static void Define(string expression, bool compress)
        {
            var key = string.Empty;
            var value = string.Empty;

            SplitKeyValue(expression, out key, out value);

            if (string.IsNullOrEmpty(value))
            {
                context.Out.WriteLine("> Missing value!");
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

            context.Out.WriteLine("Defined {0} as {1}.", newVar.Name, newVar.Value);
        }

        private static void Solve(string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                expression = QueryInput();
            }

            var calculator = new Calculator(context);
            var res = calculator.Calculate(expression);
            context.Out.WriteLine("> {0}", res.ToString());
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
                context.Out.Write("({0})", postFix[i].ToString());

                if (i != postFix.Count - 1)
                {
                    context.Out.Write(", ");
                }
                else
                {
                    context.Out.WriteLine();
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
                    context.Out.Write(string.Format("({0}:{1})", tokenizer.Tokens[i].Type, tokenizer.Tokens[i].Value));

                    if (i != tokenizer.Tokens.Count - 1)
                    {
                        context.Out.Write(", ");
                    }
                    else
                    {
                        context.Out.WriteLine();
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
        public class TerminalAction
        {
            public string Name
            {
                get;
                set;
            }

            public string Alias
            {
                get;
                set;
            }

            public bool HasAlias
            {
                get
                {
                    return !string.IsNullOrEmpty(this.Alias);
                }
            }

            public string Description
            {
                get;
                set;
            }

            public bool HasDesciption
            {
                get
                {
                    return string.IsNullOrEmpty(this.Description);
                }
            }

            public Func<string, bool> Action
            {
                get;
                set;
            }

            public bool Run(string arg)
            {
                return this.Action(arg);
            }
        }
    }


}

using System;
using Matheparser;
using Matheparser.Parsing;
using Matheparser.Solver;
using Matheparser.Tokenizing;

namespace TerminalTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var command = string.Empty;

            while (command != "quit")
            {
                try
                {

                    switch (command)
                    {
                        case "tokenize":
                            Tokenize();
                            break;
                        case "parse":
                            Parse();
                            break;
                        case "Solve":
                            Solve();
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

                command = Console.ReadLine().ToLower();
            }
        }

        private static void Solve()
        {
            var solver = new Solver();
            var res = solver.Solve(Console.ReadLine());
            Console.WriteLine(res.StringValue);
        }

        private static void Parse()
        {
            var tokenizer = new Tokenizer(Console.ReadLine(), Config.DefaultConfig);
            tokenizer.Run();
            var parser = new Parser(tokenizer.Tokens, Config.DefaultConfig);
            parser.Run();

            throw new NotImplementedException("Ausgabe");
        }

        private static void Tokenize()
        {
            var tokenizer = new Tokenizer(Console.ReadLine(), Config.DefaultConfig);
            tokenizer.Run();

            foreach (var token in tokenizer.Tokens)
            {
                Console.Write(token.ToString());
            }
        }
    }
}

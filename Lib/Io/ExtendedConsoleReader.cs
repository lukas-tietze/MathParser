using System;
using System.Text;

namespace Matheparser.Io
{
    public class ExtendedConsoleReader : ConsoleReader
    {
        public override string ReadLine()
        {
            var input = default(ConsoleKeyInfo);
            var buf = new StringBuilder();

            while(input.Key != ConsoleKey.Enter)
            {
                input = Console.ReadKey(true);

                buf.Append(input.KeyChar);
            }

            return buf.ToString();
        }
    }
}
using System;

namespace Matheparser.Io
{
    public class ConsoleWriter : WriterBase
    {
        public override void Clear()
        {
            Console.Clear();
        }

        protected override void WriteCore(string msg)
        {
            Console.Write(msg);
        }

        protected override void WriteLineCore(string msg)
        {
            Console.WriteLine(msg);
        }

        protected override void WriteLineCore()
        {
            Console.WriteLine();
        }
    }
}
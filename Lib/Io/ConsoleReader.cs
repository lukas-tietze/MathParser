using System;

namespace Matheparser.Io
{
    public class ConsoleReader : IReader
    {
        public ConsoleReader()
        {
        }

        public bool EnablePrompt
        {
            get;
            set;
        }

        public virtual ConsoleKey ReadKey()
        {
            return Console.ReadKey(false).Key;
        }

        public virtual ConsoleKey ReadKey(string prompt)
        {
            if(this.EnablePrompt)
            {
                Console.WriteLine(prompt);
            }

            return this.ReadKey();
        }

        public virtual string ReadLine()
        {
            return Console.ReadLine();
        }

        public virtual string ReadLine(string prompt)
        {
            if(this.EnablePrompt)
            {
                Console.WriteLine(prompt);
            }

            return this.ReadLine();
        }
    }
}
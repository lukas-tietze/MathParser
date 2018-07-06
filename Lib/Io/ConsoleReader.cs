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

        public ConsoleKey ReadKey()
        {
            return Console.ReadKey(false).Key;
        }

        public ConsoleKey ReadKey(string prompt)
        {
            if(this.EnablePrompt)
            {
                Console.WriteLine(prompt);
            }

            return this.ReadKey();
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public string ReadLine(string prompt)
        {
            if(this.EnablePrompt)
            {
                Console.WriteLine(prompt);
            }

            return this.ReadLine();
        }
    }
}
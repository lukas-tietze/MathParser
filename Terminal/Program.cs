using System;
using Matheparser;

namespace Terminal
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var engine = new Engine();

            var quit = false;

            engine.EnqueueAllCommands(args);

            while (!quit)
            {
                quit = engine.ExecuteNext();
            }
        }
    }
}

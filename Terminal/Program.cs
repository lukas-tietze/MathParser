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

            for(int i = 0; i < args.Length && !quit; i++)
            {
                quit = engine.Execute(args[i]);
            }

            while(!quit)
            {
                quit = engine.ExecuteNext();
            }
        }
    }
}

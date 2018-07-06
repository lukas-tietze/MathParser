using System;

namespace Terminal
{
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

        public Func<string, bool> Action
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }
    }
}
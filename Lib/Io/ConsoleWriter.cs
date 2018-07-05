using System;

namespace Matheparser.Io
{
    public class ConsoleWriter : IWriter
    {
        public string MessagePrefix
        {
            get;
            set;
        }

        public string WarningPrefix
        {
            get;
            set;
        }

        public string ErrorPrefix
        {
            get;
            set;
        }

        public bool WarningsEnabled
        {
            get;
            set;
        }

        public bool ErrorsEnabled
        {
            get;
            set;
        }

        public bool MessagesEnabled
        {
            get;
            set;
        }

        public void Write(string msg)
        {
            if (this.MessagesEnabled)
            {
                Console.Write(msg);
            }
        }

        public void WriteError(string msg)
        {
            if (this.ErrorsEnabled)
            {
                Console.WriteLine("{0}{1}", this.ErrorPrefix, msg);
            }
        }

        public void WriteLine(string msg)
        {
            if (this.MessagesEnabled)
            {
                Console.WriteLine("{0}{1}", this.MessagePrefix, msg);
            }
        }

        public void WriteWarning(string msg)
        {
            if(this.WarningsEnabled)
            {
                Console.WriteLine("{0}{1}", this.WarningPrefix, msg);
            }
        }
    }
}
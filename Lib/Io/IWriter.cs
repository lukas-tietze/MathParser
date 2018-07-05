namespace Matheparser.Io
{
    public interface IWriter
    {
        string MessagePrefix
        {
            set;
        }

        string WarningPrefix
        {
            set;
        }

        string ErrorPrefix
        {
            set;
        }

        bool WarningsEnabled
        {
            set;
        }

        bool ErrorsEnabled
        {
            set;
        }

        bool MessagesEnabled
        {
            set;
        }

        void WriteWarning(string msg);

        void WriteError(string msg);

        void Write(string msg);

        void WriteLine(string msg);
    }
}
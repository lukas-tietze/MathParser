namespace Terminal
{
    public interface IWriter
    {
        void Write(string format, params object[] args);

        void WriteLine();
        
        void WriteLine(string format, params object[] args);
    }
}
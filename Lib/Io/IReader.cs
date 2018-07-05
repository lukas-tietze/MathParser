namespace Matheparser.Io
{
    public interface IReader
    {
        bool EnablePrompt
        {
            set;
        }

        string ReadLine();

        string ReadLine(string prompt);

        System.ConsoleKey ReadKey();

        System.ConsoleKey ReadKey(string prompt);
    }
}
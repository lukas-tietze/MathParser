namespace Terminal
{
    public class FileWriter : IWriter
    {
        private string path;
        public FileWriter(string path)
        {
            this.path = path;
        }

        public void Write(string format, params object[] args)
        {
            throw new System.NotImplementedException();
        }

        public void WriteLine()
        {
            throw new System.NotImplementedException();
        }

        public void WriteLine(string format, params object[] args)
        {
            throw new System.NotImplementedException();
        }
    }
}
namespace IDisposableInterface.FileOperations
{
    internal class FileReader : IDisposable
    {
        private readonly string filePath;
        private readonly StreamReader streamReader;

        public FileReader(string filePath)
        {
            this.filePath = filePath;
            this.streamReader = new StreamReader(filePath);
        }

        public List<string> ReadFromFile()
        {
            List<string> lines = new List<string>();
            string line;
            while ((line = this.streamReader.ReadLine() !) != null)
            {
                lines.Add(line);
            }

            return lines;
        }

        public void Dispose()
        {
            Console.WriteLine("File reading operation is disposed.\n");
            this.streamReader.Close();
        }
    }
}
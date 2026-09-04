namespace IDisposableInterface.FileOperations
{
    internal class FileWriter : IDisposable
    {
        private readonly string filePath;
        private readonly StreamWriter streamWriter;

        public FileWriter(string filePath)
        {
            this.filePath = filePath;
            this.streamWriter = new StreamWriter(filePath, true);
        }

        public void WriteIntoFile(string contents)
        {
            this.streamWriter.WriteLine(contents);
        }

        public void Dispose()
        {
            Console.WriteLine("File writing operation is disposed.\n");
            this.streamWriter.Close();
        }
    }
}
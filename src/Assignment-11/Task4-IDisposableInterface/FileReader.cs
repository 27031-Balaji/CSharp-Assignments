namespace IDisposableInterface.FileOperations
{
    /// <summary>
    /// The FileReader class is used to read the contents of a file.
    /// </summary>
    internal class FileReader : IDisposable
    {
        private readonly string filePath;
        private readonly StreamReader streamReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileReader"/> class.
        /// </summary>
        /// <param name="filePath">The file path of the file to be read.</param>
        public FileReader(string filePath)
        {
            this.filePath = filePath;
            this.streamReader = new StreamReader(filePath);
        }

        /// <summary>
        /// Used to read the contents from the file.
        /// </summary>
        /// <returns>The list of lines of data from the file.</returns>
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

        /// <summary>
        /// Disposes the file to make sure other classes can use the file.
        /// </summary>
        public void Dispose()
        {
            this.streamReader.Dispose();
            Console.WriteLine("File reading operation is disposed.\n");
        }
    }
}
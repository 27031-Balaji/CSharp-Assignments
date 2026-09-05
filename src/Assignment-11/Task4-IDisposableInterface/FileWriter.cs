namespace IDisposableInterface.FileOperations
{
    /// <summary>
    /// The FileWriter class is used to write into a text file.
    /// </summary>
    internal class FileWriter : IDisposable
    {
        private readonly string filePath;
        private readonly StreamWriter streamWriter;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileWriter"/> class.
        /// </summary>
        /// <param name="filePath">The file path of the file to be used.</param>
        public FileWriter(string filePath)
        {
            this.filePath = filePath;
            this.streamWriter = new StreamWriter(filePath, true);
        }

        /// <summary>
        /// Writes contents to the text file.
        /// </summary>
        /// <param name="contents">The contents to be written into the file.</param>
        public void WriteIntoFile(string contents)
        {
            this.streamWriter.WriteLine(contents);
        }

        /// <summary>
        /// Disposes the file to make sure other classes can use the file.
        /// </summary>
        public void Dispose()
        {
            this.streamWriter.Dispose();
            Console.WriteLine("File writing operation is disposed.\n");
        }
    }
}
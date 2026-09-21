namespace IDisposableInterface.FileOperations
{
    /// <summary>
    /// The FileWriter class is used to write into a text file.
    /// </summary>
    internal class FileWriter : IDisposable
    {
        private readonly string filePath;
        private readonly StreamWriter streamWriter;
        private bool isDisposed;

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
            if (this.isDisposed)
            {
                return;
            }

            this.streamWriter.WriteLine(contents);
        }

        /// <summary>
        /// Disposes the file to make sure other classes can use the file.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        /// <summary>
        /// Releases the resources used by the component.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources, false to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.isDisposed)
            {
                return;
            }

            if (disposing)
            {
                // Handling managed resources here, if any.
                this.streamWriter.Dispose();
            }

            // Handling unmanaged resources, if any.
            this.isDisposed = true;
            Console.WriteLine("File writing operation is disposed.");
        }
    }
}
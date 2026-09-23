using System.Text;

namespace FileUtilities
{
    /// <summary>
    /// Class used to create a file of size 1 GB.
    /// </summary>
    public class LargeFileMaker
    {
        private const long _fileSize = 1L * 1024 * 1024 * 1024;
        private readonly string _filePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="LargeFileMaker"/> class.
        /// </summary>
        /// <param name="filePath">The file path to be used for making the large file.</param>
        public LargeFileMaker(string filePath)
        {
            this._filePath = filePath;
        }

        /// <summary>
        /// Creates the file with random text.
        /// </summary>
        public void Run()
        {
            if (!File.Exists(this._filePath))
            {
                File.Create(this._filePath).Dispose();
            }

            string line = "abcdefghijklmnopqrstuvwxyz";
            long written = 0;

            using (StreamWriter writer = new StreamWriter(this._filePath))
            {
                while (written < _fileSize)
                {
                    writer.Write(line);
                    written += Encoding.UTF8.GetByteCount(line);
                }
            }
        }
    }
}
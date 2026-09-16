using System.Text;

namespace FileDataProcessor.Class
{
    internal class LargeFileMaker
    {
        private const long _fileSize = 1L * 1024 * 1024 * 1024;
        private readonly string _filePath;

        public LargeFileMaker(string filePath)
        {
            this._filePath = filePath;
        }

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
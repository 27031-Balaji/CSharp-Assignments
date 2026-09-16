using System.Diagnostics;
using System.Text;
using FileDataProcessor.Class;

namespace FileDataProcessor
{
    /// <summary>
    /// The Program class is the entry point of the application.
    /// </summary>
    internal class Program
    {
        private static string _readFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "BigFile.txt");
        private static string _writeFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "ProcessedBigFile.txt");

        /// <summary>
        /// This method Main is the main method that runs when the application is built.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            if (!File.Exists(_readFilePath))
            {
                LargeFileMaker fileMaker = new LargeFileMaker(_readFilePath);
                fileMaker.Run();
            }

            Console.WriteLine($"Reading using a file stream and a buffer...");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ReadUsingFileStream();
            stopwatch.Stop();
            Console.WriteLine($"Time Taken: {stopwatch.ElapsedMilliseconds} ms.{Environment.NewLine}");

            Console.WriteLine($"Reading using a buffered stream...");
            stopwatch.Restart();
            ReadUsingBufferedStream();
            stopwatch.Stop();
            Console.WriteLine($"Time Taken: {stopwatch.ElapsedMilliseconds} ms.{Environment.NewLine}");

            Console.WriteLine($"Processing and writing to a new file using MemoryStream...");
            stopwatch.Restart();
            ProcessAndWriteFile();
            stopwatch.Stop();
            Console.WriteLine($"Time Taken: {stopwatch.ElapsedMilliseconds} ms.{Environment.NewLine}");

            Console.ReadKey();
        }

        private static void ReadUsingFileStream()
        {
            byte[] buffer = new byte[1024]; // 1 KB Buffer
            int bytesRead = 0;

            using (FileStream fileStream = new FileStream(_readFilePath, FileMode.Open, FileAccess.Read))
            {
                while (true)
                {
                    bytesRead = fileStream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        break;
                    }

                    string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    string upperCaseData = ProcessData(data);
                }
            }
        }

        private static void ReadUsingBufferedStream()
        {
            byte[] buffer = new byte[1024]; // 1 KB Buffer
            int bytesRead = 0;

            using (FileStream fileStream = new FileStream(_readFilePath, FileMode.Open, FileAccess.Read))
            using (BufferedStream bufferedStream = new BufferedStream(fileStream, 1024 * 1024))
            {
                while (true)
                {
                    bytesRead = bufferedStream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        break;
                    }

                    string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    string upperCaseData = ProcessData(data);
                }
            }
        }

        private static void ProcessAndWriteFile()
        {
            byte[] buffer = new byte[1024];
            int bytesRead;

            using (FileStream inputStream = new FileStream(_readFilePath, FileMode.Open, FileAccess.Read))
            using (FileStream outputStream = new FileStream(_writeFilePath, FileMode.Create, FileAccess.Write))
            using (BufferedStream bufferedInputStream = new BufferedStream(inputStream, 1024 * 1024))
            using (MemoryStream memoryStream = new MemoryStream())
            {
                while ((bytesRead = bufferedInputStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    string processedData = ProcessData(data);

                    byte[] processedBytes = Encoding.UTF8.GetBytes(processedData);
                    memoryStream.Write(processedBytes, 0, processedBytes.Length);

                    memoryStream.WriteTo(outputStream);
                    memoryStream.SetLength(0);
                }
            }
        }

        private static string ProcessData(string data)
        {
            return data.ToUpper();
        }
    }
}
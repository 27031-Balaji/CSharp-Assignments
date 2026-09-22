using System.Diagnostics;
using System.Text;
using AsyncFileProcessor.Class;

namespace AsyncFileProcessor
{
    /// <summary>
    /// The Program class is the entry point of the application.
    /// </summary>
    internal class Program
    {
        private static readonly string _readFilePath1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BigFile1.txt");
        private static readonly string _readFilePath2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BigFile2.txt");
        private static readonly string _writeFilePath1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProcessedBigFile1.txt");
        private static readonly string _writeFilePath2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProcessedBigFile2.txt");

        /// <summary>
        /// This method Main is the main method that runs when the application is built.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        /// <returns>The asynchronous operation.</returns>
        public static async Task Main(string[] args)
        {
            CreateFilesIfNotFound();
            Stopwatch stopwatch = new Stopwatch();
            SynchronousProcessing(stopwatch);
            await AsynchronousProcessing(stopwatch);
            Console.ReadKey();
        }

        /// <summary>
        /// Processes two files synchronously and measures the elapsed time.
        /// </summary>
        /// <param name="stopwatch">A Stopwatch instance used to track the duration of the processing operation.</param>
        private static void SynchronousProcessing(Stopwatch stopwatch)
        {
            Console.WriteLine("Synchronous Processing");
            stopwatch.Start();
            ProcessAndWriteFile(_readFilePath1, _writeFilePath1);
            ProcessAndWriteFile(_readFilePath2, _writeFilePath2);
            stopwatch.Stop();
            Console.WriteLine($"Sync Time: {stopwatch.ElapsedMilliseconds} ms.{Environment.NewLine}");
        }

        /// <summary>
        /// Performs asynchronous processing of files and measures the elapsed time.
        /// </summary>
        /// <param name="stopwatch">The stopwatch used to measure the duration of the asynchronous processing.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private static async Task AsynchronousProcessing(Stopwatch stopwatch)
        {
            Console.WriteLine("Asynchronous Processing");
            stopwatch.Restart();
            await Task.WhenAll(
                ProcessAndWriteFileAsync(_readFilePath1, "AsyncProcessedBigFile1.txt"),
                ProcessAndWriteFileAsync(_readFilePath2, "AsyncProcessedBigFile2.txt"));
            stopwatch.Stop();
            Console.WriteLine($"Async Time: {stopwatch.ElapsedMilliseconds} ms{Environment.NewLine}");
        }

        /// <summary>
        /// Creates files at specified paths if they do not already exist.
        /// </summary>
        private static void CreateFilesIfNotFound()
        {
            Console.WriteLine("Creating files if its not found...");

            if (!File.Exists(_readFilePath1))
            {
                new LargeFileMaker(_readFilePath1).Run();
            }

            if (!File.Exists(_readFilePath2))
            {
                new LargeFileMaker(_readFilePath2).Run();
            }
        }

        /// <summary>
        /// Processes data from the specified input file and writes the results to the specified output file.
        /// </summary>
        /// <param name="inputPath">The path of the input file to be processed.</param>
        /// <param name="outputPath">The path of the output file where the processed data will be written.</param>
        private static void ProcessAndWriteFile(string inputPath, string outputPath)
        {
            byte[] buffer = new byte[1024 * 1024];
            int bytesRead;

            using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
            using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            using (BufferedStream bufferedInputStream = new BufferedStream(inputStream, 20 * 1024 * 1024))
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

        /// <summary>
        /// Asynchronously processes data from the specified input file and writes the results to the specified output file.
        /// </summary>
        /// <param name="inputPath">The path of the input file to be processed.</param>
        /// <param name="outputPath">The path of the output file where the processed data will be written.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private static async Task ProcessAndWriteFileAsync(string inputPath, string outputPath)
        {
            byte[] buffer = new byte[1024 * 1024];
            int bytesRead;

            using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read, FileShare.Read, buffer.Length, useAsync: true))
            using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None, buffer.Length, useAsync: true))
            using (BufferedStream bufferedInputStream = new BufferedStream(inputStream, 20 * 1024 * 1024))
            using (MemoryStream memoryStream = new MemoryStream())
            {
                while ((bytesRead = await bufferedInputStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    string data = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    string processedData = ProcessData(data);
                    byte[] processedBytes = Encoding.UTF8.GetBytes(processedData);
                    await memoryStream.WriteAsync(processedBytes, 0, processedBytes.Length);
                    memoryStream.Position = 0;
                    await memoryStream.CopyToAsync(outputStream);
                    memoryStream.SetLength(0);
                }
            }
        }

        /// <summary>
        /// Processes the data to uppercase.
        /// </summary>
        /// <param name="data">The data to be processed.</param>
        /// <returns>The processed data.</returns>
        private static string ProcessData(string data)
        {
            return data.ToUpper();
        }
    }
}
using System.Text;

namespace LoggingSystem.Subtasks
{
    /// <summary>
    /// The optimized file logger which removes MemoryStreams and reducing unnecessary steps.
    /// </summary>
    internal class OptimizedFileLogger
    {
        private static string _logFilePath = "log.txt";

        /// <summary>
        /// Appends an error message to the log file using UTF-8 encoding.
        /// </summary>
        /// <param name="errorMessage">The error message to log.</param>
        public static void LogError(string errorMessage)
        {
            byte[] errorBytes = Encoding.UTF8.GetBytes(errorMessage);
            using (FileStream fileStream = new FileStream(_logFilePath, FileMode.Append))
            {
                fileStream.Write(errorBytes, 0, errorBytes.Length);
            }
        }
    }
}
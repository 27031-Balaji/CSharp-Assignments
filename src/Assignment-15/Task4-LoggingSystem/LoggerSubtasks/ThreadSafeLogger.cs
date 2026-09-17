using System.Text;

namespace LoggingSystem.Subtasks
{
    /// <summary>
    /// Implements the file logging system with thread-safe logging using locks.
    /// </summary>
    internal class ThreadSafeLogger
    {
        private static string _logFilePath = "log.txt";
        private static object _lockObject = new object();

        /// <summary>
        /// Appends an error message to the log file using UTF-8 encoding.
        /// </summary>
        /// <param name="errorMessage">The error message to log.</param>
        public static void LogError(string errorMessage)
        {
            lock (_lockObject)
            {
                byte[] errorBytes = Encoding.UTF8.GetBytes(errorMessage);
                using (FileStream fileStream = new FileStream(_logFilePath, FileMode.Append))
                {
                    fileStream.Write(errorBytes, 0, errorBytes.Length);
                }
            }
        }
    }
}
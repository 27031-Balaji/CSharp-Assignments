using System.Text;

namespace LoggingSystem.Subtasks
{
    /// <summary>
    /// Implements the file logging system with user-specific file logging.
    /// </summary>
    internal class UserSpecificLogger
    {
        private static readonly Dictionary<string, object> _fileLocks = new Dictionary<string, object>();
        private static readonly object _dictionaryLock = new object();

        /// <summary>
        /// Logs the error to the user-specific log file.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="errorMessage">The error message to be logged.</param>
        public static void LogError(string userId, string errorMessage)
        {
            string logFilePath = $"{userId}_errorLog.txt";
            object fileLock;

            lock (_dictionaryLock)
            {
                if (!_fileLocks.ContainsKey(logFilePath))
                {
                    _fileLocks[logFilePath] = new object();
                }

                fileLock = _fileLocks[logFilePath];
            }

            lock (fileLock)
            {
                byte[] errorBytes = Encoding.UTF8.GetBytes(errorMessage);
                using (FileStream fileStream = new FileStream(logFilePath, FileMode.Append))
                {
                    fileStream.Write(errorBytes, 0, errorBytes.Length);
                }
            }
        }
    }
}
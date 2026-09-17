using System.Text;

namespace LoggingSystem.Subtasks
{
    /// <summary>
    /// The initial logger used to log the error messages.
    /// </summary>
    public class Logger
    {
        private static string _logFilePath = "log.txt";

        /// <summary>
        /// Appends an error message to the log file using UTF-8 encoding.
        /// </summary>
        /// <param name="errorMessage">The error message to log.</param>
        public static void LogError(string errorMessage)
        {
            /* ISSUE:
               A new MemoryStream is created for every log operation.
               This increases memory allocations and garbage collection overhead,
               especially when many users are logging simultaneously.
            */
            using (MemoryStream memoryStream = new MemoryStream())
            {
                /* ISSUE:
                   The error message is first converted into a byte array,
                   creating an additional copy of the data in memory.
                */
                byte[] errorBytes = Encoding.UTF8.GetBytes(errorMessage);

                /* ISSUE:
                   Data is written to the MemoryStream before being written
                   to the file. This extra step is unnecessary since the data
                   could be written directly to the file.
                */
                memoryStream.Write(errorBytes, 0, errorBytes.Length);

                using (FileStream fileStream = new FileStream(_logFilePath, FileMode.Append))
                {
                    memoryStream.WriteTo(fileStream);

                    /* ISSUE:
                       Multiple threads may reach this code at the same time and try
                       to write to the same file concurrently.
                       This can reduce performance.
                    */
                }
            }

            /* ISSUE:
               No synchronization mechanism exists to ensure thread-safe access
               when multiple users log errors simultaneously.
            */
        }
    }
}
using System.Diagnostics;

namespace LoggingSystem.Subtasks
{
    /// <summary>
    /// Implements performance testing for logging mechanisms by executing concurrent tasks.
    /// </summary>
    internal class PerformanceTester
    {
        private string[] _userIds = new string[]
            {
                "User1", "User2", "User3", "User4", "User5",
                "User6", "User7", "User8", "User9", "User10",
                "User11", "User12", "User13", "User14", "User15",
                "User16", "User17", "User18", "User19", "User20",
            };

        private string[] _errorMessages = new string[]
        {
                "Database Error\n",
                "Connection Timeout\n",
                "Invalid Input\n",
                "Network Failure\n",
                "Access Denied\n",
                "File Not Found\n",
                "Authentication Failed\n",
                "Memory Overflow\n",
                "Service Unavailable\n",
                "Unexpected Exception\n",
        };

        /// <summary>
        /// Executes performance tests for thread-safe and user-specific logging mechanisms.
        /// </summary>
        public void Run()
        {
            Console.WriteLine("--- Performance Testing ---");
            Console.WriteLine("Performance test for thread-safe logging with locks: ");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            this.RunForThreadSafeLogger();
            stopwatch.Stop();
            Console.WriteLine($"The time taken for logging 500 tasks is: {stopwatch.ElapsedMilliseconds} ms.\n");

            Console.WriteLine("Performance test for user-specific logging with lock for user-specific files: ");
            stopwatch.Restart();
            this.RunForUserSpecificLogger();
            stopwatch.Stop();
            Console.WriteLine($"The time taken for logging 500 tasks is: {stopwatch.ElapsedMilliseconds} ms.\n");

            Console.ReadKey();
        }

        /// <summary>
        /// Executes 500 concurrent tasks to log randomly selected error messages using a thread-safe logger.
        /// </summary>
        private void RunForThreadSafeLogger()
        {
            Random random = new Random();
            Task[] tasks = new Task[500];

            for (int i = 0; i < 500; i++)
            {
                tasks[i] = Task.Run(() => ThreadSafeLogger.LogError(
                    this._errorMessages[random.Next(this._errorMessages.Length)]));
            }

            Task.WaitAll(tasks);
        }

        /// <summary>
        /// Executes 500 parallel tasks that log random error messages for random user IDs using a user-specific logger.
        /// </summary>
        private void RunForUserSpecificLogger()
        {
            Random random = new Random();
            Task[] tasks = new Task[500];

            for (int i = 0; i < 500; i++)
            {
                tasks[i] = Task.Run(() => UserSpecificLogger.LogError(
                    this._userIds[random.Next(this._userIds.Length)],
                    this._errorMessages[random.Next(this._errorMessages.Length)]));
            }

            Task.WaitAll(tasks);
        }
    }
}
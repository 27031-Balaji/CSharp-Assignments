using DeadlockAnalysis.Class;

namespace Assignments
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The method that runs when the application is started.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        /// <returns>The task containing the operation.</returns>
        public static async Task Main(string[] args)
        {
            DeadlockPreventionCode deadlockPreventionCode = new DeadlockPreventionCode();
            await deadlockPreventionCode.DeadlockMethod();
        }
    }
}
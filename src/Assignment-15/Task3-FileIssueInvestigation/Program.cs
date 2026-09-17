using System.Diagnostics;
using FileIssueInvestigation.Class;

namespace FileIssueInvestigation
{
    /// <summary>
    /// The Program class is the entry point of the application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// This method Main is the main method that runs when the application is built.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            UnoptimizedCode unoptimizedCode = new UnoptimizedCode();
            OptimizedCode optimizedCode = new OptimizedCode();
            Stopwatch stopwatch = new Stopwatch();
            Console.WriteLine($"Unoptimized code: ");
            stopwatch.Start();
            unoptimizedCode.Run();
            stopwatch.Stop();
            Console.WriteLine($"Time taken: {stopwatch.ElapsedTicks} ticks.\n");

            Console.WriteLine($"Optimized code: ");
            stopwatch.Restart();
            optimizedCode.Run();
            stopwatch.Stop();
            Console.WriteLine($"Time taken: {stopwatch.ElapsedTicks} ticks.\n");

            Console.ReadKey();
        }
    }
}
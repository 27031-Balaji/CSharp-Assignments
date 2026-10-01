using System.Diagnostics;

namespace TaskParallelLibrary
{
    /// <summary>
    /// The entry point of the application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The method that executes when the application is run.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        public static void Main(string[] args)
        {
            int[] numbers = new int[10000];

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = i + 1;
            }

            int[] sequentialResult = new int[numbers.Length];
            int[] parallelResult = new int[numbers.Length];

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < numbers.Length; i++)
            {
                sequentialResult[i] = numbers[i] * numbers[i];
            }

            stopwatch.Stop();
            Console.WriteLine($"Sequential Execution: {stopwatch.ElapsedTicks} ticks");

            stopwatch.Restart();
            Parallel.ForEach(numbers, (number, state, index) =>
            {
                parallelResult[(int)index] = number * number;
            });

            stopwatch.Stop();
            Console.WriteLine($"Parallel Execution: {stopwatch.ElapsedTicks} ticks");
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}
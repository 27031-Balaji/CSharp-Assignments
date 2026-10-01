using System.Numerics;

namespace MultiThreading
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    internal class Program
    {
        private static BigInteger sumResult = 0;
        private static BigInteger factorialResult = 1;

        /// <summary>
        /// The method that runs when the application is run.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        public static void Main(string[] args)
        {
            Thread thread1 = new Thread(CalculateSum);
            Thread thread2 = new Thread(CalculateFactorial);

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            BigInteger combinedResult = sumResult + factorialResult;

            Console.WriteLine($"Sum: {sumResult}");
            Console.WriteLine($"Factorial: {factorialResult}");
            Console.WriteLine($"Combined Result: {combinedResult}");

            Console.ReadKey();
        }

        /// <summary>
        /// Calculates the sum of natural numbers.
        /// </summary>
        private static void CalculateSum()
        {
            for (int i = 1; i <= 100000; i++)
            {
                sumResult += i;
            }

            Console.WriteLine("Sum calculation completed.");
        }

        /// <summary>
        /// Calculates the factorial of the number.
        /// </summary>
        private static void CalculateFactorial()
        {
            for (int i = 1; i <= 30; i++)
            {
                factorialResult *= i;
            }

            Console.WriteLine("Factorial calculation completed.");
        }
    }
}
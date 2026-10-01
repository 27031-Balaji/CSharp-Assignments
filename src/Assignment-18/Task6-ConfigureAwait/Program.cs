namespace ConfigureAwaitDemo
{
    /// <summary>
    /// The entry point of the application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// When the application is run, the main method is executed.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        /// <returns>The task doing the operation.</returns>
        public static async Task Main(string[] args)
        {
            Console.WriteLine($"Main started on Thread: {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine();
            int result = await MethodB();
            Console.WriteLine();
            Console.WriteLine($"Final Result: {result}");

            Console.ReadKey();
        }

        /// <summary>
        /// Asynchronously waits for three seconds without capturing the current synchronization context and returns 100.
        /// </summary>
        /// <returns>An integer value of 100.</returns>
        private static async Task<int> MethodA()
        {
            Console.WriteLine($"MethodA BEFORE await - Thread: {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine();
            await Task.Delay(3000).ConfigureAwait(false);
            Console.WriteLine($"MethodA AFTER await - Thread: {Thread.CurrentThread.ManagedThreadId}");

            return 100;
        }

        /// <summary>
        /// Asynchronously waits for MethodA() and doubles the result.
        /// </summary>
        /// <returns>The resultant integer value after doubling.</returns>
        private static async Task<int> MethodB()
        {
            Console.WriteLine($"MethodB BEFORE MethodA - Thread: {Thread.CurrentThread.ManagedThreadId}");
            int result = await MethodA();
            Console.WriteLine($"MethodB AFTER MethodA - Thread: {Thread.CurrentThread.ManagedThreadId}");
            result *= 2;
            Console.WriteLine($"MethodB Processing Complete - Thread: {Thread.CurrentThread.ManagedThreadId}");

            return result;
        }
    }
}
namespace TaskExceptionHandling
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The method that executes when the application is run.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        /// <returns>The task doing the operation.</returns>
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Task Method Exception Handling - Async Void VS Async Task");
            Console.WriteLine();

            try
            {
                await TaskMethod();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Task method throwing exception: {ex.Message}");
            }

            Console.WriteLine();

            try
            {
                VoidMethod();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Void method throwing exception: {ex.Message}");
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Asynchronously waits for one second, then throws a NotImplementedException.
        /// </summary>
        /// <exception cref="NotImplementedException">Thrown to indicate the method is not implemented.</exception>
        private static async void VoidMethod()
        {
            await Task.Delay(1000);
            throw new NotImplementedException("Not implemented from Void method.");
        }

        /// <summary>
        /// Asynchronously waits for one second, then throws a NotImplementedException with a Task.
        /// </summary>
        /// <returns>The task returning after throwing exception.</returns>
        /// <exception cref="NotImplementedException">Thrown to indicate the method is not implemented.</exception>
        private static async Task TaskMethod()
        {
            await Task.Delay(1000);
            throw new NotImplementedException("Not implemented from Task method");
        }
    }
}
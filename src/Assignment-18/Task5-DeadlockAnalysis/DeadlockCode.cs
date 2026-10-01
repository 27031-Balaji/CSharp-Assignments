namespace DeadlockAnalysis.Class
{
    /// <summary>
    /// Contains the method that makes the deadlock.
    /// </summary>
    internal class DeadlockCode
    {
        /// <summary>
        /// Performs an asynchronous operation and outputs the result to the console.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task DeadlockMethod()
        {
            // Note: Do not use .Result or .Wait in production level code.
            var result = this.SomeAsyncOperation().Result;
            Console.WriteLine(result);
        }

        /// <summary>
        /// Does the async operation.
        /// </summary>
        /// <returns>The string that contains message.</returns>
        private async Task<string> SomeAsyncOperation()
        {
            await Task.Delay(1000);
            return "Hello, World!";
        }
    }
}
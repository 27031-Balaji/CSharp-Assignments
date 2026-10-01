namespace DeadlockAnalysis.Class
{
    internal class DeadlockPreventionCode
    {
        /// <summary>
        /// Performs an asynchronous operation and outputs the result to the console.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task DeadlockMethod()
        {
            var result = await this.SomeAsyncOperation();
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
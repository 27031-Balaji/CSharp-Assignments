namespace CSharpAdvancedFeatures.Tasks
{
    /// <summary>
    /// Implements the Task 4 of the advanced features of C#.
    /// </summary>
    internal class Task4
    {
        private Func<int, bool> evenNumberLambdaExpression = x => x % 2 == 0;
        private Func<int, int> squaredLambdaExpression = x => x * x;

        /// <summary>
        /// Runs the application with the task.
        /// </summary>
        public void Run()
        {
            Console.WriteLine("Task 4 - Understanding and Using Lambda Expressions and Statements\n");

            List<int> integerList = new List<int>() { 24, 93, 12, 9, 62, 29, 72 };
            Console.WriteLine("The original list of integers before LINQ operations: ");
            Console.WriteLine(string.Join(", ", integerList));
            Console.WriteLine();

            IEnumerable<int> filteredIntegerList = integerList
                                                        .Where(this.evenNumberLambdaExpression)
                                                        .Select(this.squaredLambdaExpression);

            Console.WriteLine("The array of integers after sorting using anonymous method: ");
            Console.WriteLine(string.Join(", ", filteredIntegerList));
        }
    }
}

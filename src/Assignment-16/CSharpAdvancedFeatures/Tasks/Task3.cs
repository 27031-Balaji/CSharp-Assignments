namespace CSharpAdvancedFeatures.Tasks
{
    /// <summary>
    /// Implements the Task 3 of the advanced features of C#.
    /// </summary>
    internal class Task3
    {
        /// <summary>
        /// Represents a delegate that compares two integers
        /// and returns a value indicating their relative order in ascending sequence.
        /// </summary>
        private readonly Comparison<int> ascendingSortDelegate = delegate(int a, int b)
        {
            if (a > b)
            {
                return 1;
            }
            else if (a < b)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        };

        /// <summary>
        /// Runs the application with the task.
        /// </summary>
        public void Run()
        {
            Console.WriteLine("Task 3 - Implementing Anonymous Methods\n");

            int[] arrayOfIntegers = new int[] { 45, 12, 78, 3, 25, 9, 67 };
            Console.WriteLine("The original array of integers before sorting: ");
            Console.WriteLine(string.Join(", ", arrayOfIntegers));
            Console.WriteLine();

            Array.Sort(arrayOfIntegers, this.ascendingSortDelegate);
            Console.WriteLine("The array of integers after sorting using anonymous method: ");
            Console.WriteLine(string.Join(", ", arrayOfIntegers));
        }
    }
}
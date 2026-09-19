namespace CSharpAdvancedFeatures.Tasks
{
    /// <summary>
    /// Implements the Task 2 of the advanced features of C#.
    /// </summary>
    internal class Task2
    {
        /// <summary>
        /// Runs the application with the tasks.
        /// </summary>
        public void Run()
        {
            Console.WriteLine("Task 2 - Understanding the Use of Dynamic and Var Keywords and Their Differences\n");

            var age = 10;
            Console.WriteLine($"Age variable initialized with var. The value of age is: {age}");

            // age = "Ten"; You cannot change the datatype since the datatype is set only in the first time.
            Console.WriteLine($"After trying to change the datatype: {age}");

            dynamic ageDynamic = 10;
            Console.WriteLine($"Dynamic variable age is initialized with int. The value of age is: {ageDynamic}");

            ageDynamic = "Ten";
            Console.WriteLine($"Dynamic variable age is now changed to string. The value of age is: {ageDynamic}");
        }
    }
}
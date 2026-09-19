using CSharpAdvancedFeatures.Tasks;

namespace Assignments
{
    /// <summary>
    /// The Program class is the entry point of the application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The main method that runs when the application is built.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            Task6 task6 = new Task6();
            task6.Run();
            Console.ReadKey();
        }
    }
}
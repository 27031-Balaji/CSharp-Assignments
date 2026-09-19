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
            Task7 task7 = new Task7();
            task7.Run();
            Console.ReadKey();
        }
    }
}
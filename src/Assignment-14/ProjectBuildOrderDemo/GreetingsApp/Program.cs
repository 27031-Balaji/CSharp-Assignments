using DisplayApp;

namespace GreetingsApp
{
    /// <summary>
    /// The entry point of the application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The method that executes when the application is run.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public static void Main(string[] args)
        {
            ConsoleUI consoleUI = new ConsoleUI();
            Console.WriteLine("Welcome to Project Build Order.");
            consoleUI.Start();
        }
    }
}
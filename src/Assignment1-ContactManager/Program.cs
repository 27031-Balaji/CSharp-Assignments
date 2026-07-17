using Assignment1.View;

namespace Contact_Manager
{
    /// <summary>
    /// This class Program does all the functionalities of the Contact Manager.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// This method Main is the main method that runs when the application is built.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            ConsoleOperation console = new ConsoleOperation();
            console.Run();
        }
    }
}
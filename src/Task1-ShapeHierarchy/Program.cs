using Task1.Helpers;
using Task1.Services;
using Task1.View;

namespace Assignments
{
    /// <summary>
    /// Entry point for the application that composes required services and starts the console UI.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Application entry point. Initializes dependencies and runs the console UI flow.
        /// </summary>
        /// <param name="args">Command-line arguments passed to the application.</param>
        public static void Main(string[] args)
        {
            ShapeHelpers shapeHelpers = new ShapeHelpers();
            ShapeServices shapeServices = new ShapeServices();
            ConsoleOperations consoleOperations = new ConsoleOperations(shapeServices, shapeHelpers);
            consoleOperations.Run();
        }
    }
}
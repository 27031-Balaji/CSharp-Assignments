using Task1.Controllers;
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
        /// Application entry point. Initializes dependencies and runs the controller functions.
        /// </summary>
        /// <param name="args">Command-line arguments passed to the application.</param>
        public static void Main(string[] args)
        {
            ShapeHelpers shapeHelpers = new ShapeHelpers();
            ShapeServices shapeServices = new ShapeServices();
            ConsoleOperations view = new ConsoleOperations();
            ShapeController controller = new ShapeController(shapeServices, shapeHelpers, view);
            controller.Run();
        }
    }
}
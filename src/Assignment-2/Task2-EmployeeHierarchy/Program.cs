using Task2.Helpers;
using Task2.Services;
using Task2.View;

namespace Assignments
{
    /// <summary>
    /// Represents the entry point of the employee bonus application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Creates the required objects and starts the employee bonus system.
        /// </summary>
        /// <param name="args">The command-line arguments passed to the application.</param>
        public static void Main(string[] args)
        {
            EmployeeHelpers employeeHelpers = new EmployeeHelpers();
            EmployeeServices employeeServices = new EmployeeServices();
            ConsoleOperations consoleOperations = new ConsoleOperations(employeeServices, employeeHelpers);
            consoleOperations.Run();
        }
    }
}
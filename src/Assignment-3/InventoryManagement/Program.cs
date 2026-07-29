using InventoryManagement.Controllers;
using InventoryManagement.Helpers;
using InventoryManagement.Persistence;
using InventoryManagement.Services;
using InventoryManagement.View;

namespace Assignments
{
    /// <summary>
    /// The entry point of the inventory management application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Starts the inventory management application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            try
            {
                ProductRepository repository = new ProductRepository();
                ProductServices services = new ProductServices(repository);
                ProductHelpers helpers = new ProductHelpers();
                ConsoleOperations view = new ConsoleOperations();
                ProductControllers controller = new ProductControllers(services, helpers, view);
                controller.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("An unexpected error occurred.");
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
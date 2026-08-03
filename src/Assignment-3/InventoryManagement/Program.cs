using InventoryManagement.Controller;
using InventoryManagement.Helper;
using InventoryManagement.Repository;
using InventoryManagement.Service;
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
                ProductService services = new ProductService(repository);
                ProductHelper helpers = new ProductHelper();
                ConsoleOperation view = new ConsoleOperation();
                ProductController controller = new ProductController(services, helpers, view);
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
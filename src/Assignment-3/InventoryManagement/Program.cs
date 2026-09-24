using InventoryManagement.Controller;
using InventoryManagement.Helper;
using InventoryManagement.Repository;
using InventoryManagement.Service;
using InventoryManagement.View;

namespace InventoryManagement
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
            IRepository repository = new ProductRepository();
            IProductService service = new ProductService(repository);
            ProductHelper helper = new ProductHelper();
            ConsoleOperation view = new ConsoleOperation();
            ProductController controller = new ProductController(service, helper, view);
            controller.Run();
        }
    }
}
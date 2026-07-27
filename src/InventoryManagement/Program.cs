using InventoryManagement.Controllers;
using InventoryManagement.Helpers;
using InventoryManagement.Persistence;
using InventoryManagement.Services;
using InventoryManagement.View;

namespace Assignments
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            ProductRepository repository = new ProductRepository();
            ProductServices services = new ProductServices(repository);
            ProductHelper helper = new ProductHelper();
            ConsoleOperations view = new ConsoleOperations();
            ProductController controller = new ProductController(services, helper, view);
            controller.Run();
        }
    }
}
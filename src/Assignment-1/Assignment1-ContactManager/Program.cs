using ContactManager.Controller;
using ContactManager.Helper;
using ContactManager.Repository;
using ContactManager.Service;
using ContactManager.View;

namespace ContactManager
{
    /// <summary>
    /// The Program class is the entry point of the application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// This method Main is the main method that runs when the application is built.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            ContactRepository repository = new ContactRepository();
            ContactService service = new ContactService(repository);
            ContactHelper helper = new ContactHelper();
            ConsoleOperation view = new ConsoleOperation();
            ContactController controller = new ContactController(service, helper, view);
            controller.Run();
        }
    }
}
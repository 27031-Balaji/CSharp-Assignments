using BankingSystem.Controllers;
using BankingSystem.Helpers;
using BankingSystem.Repository;
using BankingSystem.Services;
using BankingSystem.View;

namespace Assignments
{
    /// <summary>
    /// Represents the entry point of the banking system application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Creates the required objects and starts the banking system.
        /// </summary>
        /// <param name="args">The command-line arguments passed to the application.</param>
        public static void Main(string[] args)
        {
            BankRepository bankRepository = new BankRepository();
            BankServices bankServices = new BankServices(bankRepository);
            BankHelpers bankHelpers = new BankHelpers();
            ConsoleOperations consoleOperations = new ConsoleOperations();
            BankController bankController = new BankController(bankServices, bankHelpers, consoleOperations);
            bankController.Run();
        }
    }
}
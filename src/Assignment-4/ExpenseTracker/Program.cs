using ExpenseTracker.Controller;
using ExpenseTracker.Helper;
using ExpenseTracker.Repository;
using ExpenseTracker.Service;
using ExpenseTracker.View;

namespace ExpenseTracker
{
    /// <summary>
    /// Application entry point for the expense tracker.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The program entry point and creates the objects.
        /// </summary>
        /// <param name="args">Command-line arguments passed to the application.</param>
        public static void Main(string[] args)
        {
            IRepository repository = new CsvFinanceRepository();
            FinanceService expenseService = new FinanceService(repository);
            FinanceHelper expenseHelper = new FinanceHelper();
            ConsoleOperation view = new ConsoleOperation();
            FinanceController controller = new FinanceController(expenseService, expenseHelper, view);
            controller.Run();
        }
    }
}
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
            IExpenseRepository repository = new CsvExpenseRepository();
            ExpenseService expenseService = new ExpenseService(repository);
            ExpenseHelper expenseHelper = new ExpenseHelper();
            ConsoleOperation view = new ConsoleOperation();
            ExpenseController controller = new ExpenseController(expenseService, expenseHelper, view);
            controller.Run();
        }
    }
}
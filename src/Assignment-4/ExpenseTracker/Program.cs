using ExpenseTracker.Controller;
using ExpenseTracker.Helper;
using ExpenseTracker.Repository;
using ExpenseTracker.Service;
using ExpenseTracker.View;

namespace ExpenseTracker
{
    internal class Program
    {
        static void Main(string[] args)
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
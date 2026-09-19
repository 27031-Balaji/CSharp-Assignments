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
            try
            {
                IRepository repository = new CsvFinanceRepository();
                FinanceHelper financeHelper = new FinanceHelper();
                FinanceService financeService = new FinanceService(repository, financeHelper);
                ConsoleOperation view = new ConsoleOperation();
                FinanceController controller = new FinanceController(financeService, financeHelper, view);
                controller.Run();
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case FormatException:
                        Console.WriteLine(ConsoleMessages.InvalidFormatMessage);
                        break;

                    case FileNotFoundException:
                        Console.WriteLine(ConsoleMessages.FileNotFoundMessage);
                        break;

                    case InvalidOperationException:
                        Console.WriteLine(ConsoleMessages.InvalidRecordTypeMessage);
                        break;

                    default:
                        Console.WriteLine($"Unknown Exception: {ex.Message}");
                        break;
                }
            }
        }
    }
}
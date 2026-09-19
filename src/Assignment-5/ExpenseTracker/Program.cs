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
        /// The program entry point that creates the application objects.
        /// </summary>
        /// <param name="args">Command-line arguments passed to the application.</param>
        public static void Main(string[] args)
        {
            try
            {
                IUserRepository userRepository = new CsvUserRepository();
                IFinanceRepository financeRepository = new CsvFinanceRepository();
                AuthenticationHelper authenticationHelper = new AuthenticationHelper();
                FinanceHelper financeHelper = new FinanceHelper();
                HashService hashService = new HashService();

                UserService userService = new UserService(userRepository, hashService);
                FinanceService financeService = new FinanceService(financeRepository, financeHelper);
                AuthenticationService authenticationService = new AuthenticationService(userService, financeService);

                AuthenticationView authenticationView = new AuthenticationView();
                ApplicationView applicationView = new ApplicationView();

                ApplicationController applicationController = new ApplicationController(financeService, authenticationService, financeHelper, applicationView);
                AuthenticationController authenticationController = new AuthenticationController(authenticationService, authenticationView, authenticationHelper, applicationController);
                authenticationController.Run();
            }
            catch (FormatException)
            {
                Console.WriteLine(ConsoleMessages.InvalidFormatMessage);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine(ConsoleMessages.FileNotFoundMessage);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine(ConsoleMessages.InvalidRecordTypeMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }
    }
}
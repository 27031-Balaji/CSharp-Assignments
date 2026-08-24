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
            IUserRepository userRepository = new CsvUserRepository();
            IFinanceRepository financeRepository = new CsvFinanceRepository();
            UserService userService = new UserService(userRepository);
            AuthenticationService authenticationService = new AuthenticationService(userService);
            FinanceService financeService = new FinanceService(financeRepository);

            AuthenticationView authenticationView = new AuthenticationView();
            AuthenticationHelper authenticationHelper = new AuthenticationHelper();
            FinanceHelper financeHelper = new FinanceHelper();
            ApplicationView applicationView = new ApplicationView();

            ApplicationController applicationController = new ApplicationController(financeService, authenticationService, financeHelper, applicationView);
            AuthenticationController authenticationController = new AuthenticationController(authenticationService, authenticationView, authenticationHelper, applicationController);
            authenticationController.Run();
        }
    }
}
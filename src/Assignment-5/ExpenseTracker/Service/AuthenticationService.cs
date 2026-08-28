using ExpenseTracker.Model;

namespace ExpenseTracker.Service
{
    /// <summary>
    /// Provides authentication operations and tracks the current authenticated user.
    /// </summary>
    internal class AuthenticationService
    {
        private readonly UserService userService;
        private readonly FinanceService financeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationService"/> class.
        /// </summary>
        /// <param name="userService">The <see cref="UserService"/> used to manage user data.</param>
        /// <param name="financeService">The <see cref="FinanceService"/> used to load the records.</param>
        public AuthenticationService(UserService userService, FinanceService financeService)
        {
            this.userService = userService;
            this.financeService = financeService;
        }

        /// <summary>
        /// Gets the currently authenticated user.
        /// </summary>
        public AuthenticatedUser LoggedInUser { get; private set; }

        /// <summary>
        /// Gets a value indicating whether a user is currently authenticated.
        /// </summary>
        public bool IsAuthenticated { get => this.LoggedInUser != null; }

        /// <summary>
        /// Determines whether the specified user name is already taken.
        /// </summary>
        /// <param name="userName">The user name to check.</param>
        /// <returns>True if the user name exists, otherwise false.</returns>
        public bool UserNameExists(string userName)
        {
            return this.userService.UserNameExists(userName);
        }

        /// <summary>
        /// Creates a new user account with the specified credentials.
        /// </summary>
        /// <param name="userName">The user name for the new account.</param>
        /// <param name="password">The password for the new account.</param>
        public void SignUp(string userName, string password)
        {
            this.userService.CreateUser(userName, password);
        }

        /// <summary>
        /// Attempts to sign in with the provided credentials.
        /// </summary>
        /// <param name="userName">The user name to authenticate.</param>
        /// <param name="password">The password to validate.</param>
        public void Login(string userName, string password)
        {
            this.LoggedInUser = this.userService.Authenticate(userName, password);
            this.financeService.LoadRecords(this.LoggedInUser.Id);
        }

        /// <summary>
        /// Signs out the currently authenticated user.
        /// </summary>
        public void Logout()
        {
            this.LoggedInUser = null!;
        }

        /// <summary>
        /// Deletes the account identified by the user ID.
        /// </summary>
        /// <param name="userId">The identifier of the user to delete.</param>
        public void DeleteAccount(Guid userId)
        {
            this.userService.DeleteUser(userId);
        }
    }
}
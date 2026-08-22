using ExpenseTracker.Model;

namespace ExpenseTracker.Service
{
    internal class AuthService
    {
        private readonly UserService userService;

        public AuthService(UserService userService)
        {
            this.userService = userService;
        }

        public AuthenticatedUser LoggedInUser { get; private set; }

        public bool IsAuthenticated { get => this.LoggedInUser != null; }

        public bool UserNameExists(string userName)
        {
            return this.userService.UserNameExists(userName);
        }

        public void SignUp(string userName, string password)
        {
            this.userService.CreateUser(userName, password);
        }

        public void Login(string userName, string password)
        {
            this.LoggedInUser = this.userService.Authenticate(userName, password);
        }

        public void Logout()
        {
            this.LoggedInUser = null;
        }

        public void DeleteAccount()
        {
            Guid userId = this.LoggedInUser.Id;
            this.userService.DeleteUser(userId);
            this.LoggedInUser = null !;
        }
    }
}
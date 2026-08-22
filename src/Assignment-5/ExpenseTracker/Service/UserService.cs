using ExpenseTracker.Model;
using ExpenseTracker.Repository;

namespace ExpenseTracker.Service
{
    /// <summary>
    /// Provides user-related operations and coordinates persistence via <see cref="IUserRepository"/>.
    /// </summary>
    internal class UserService
    {
        private readonly IUserRepository userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userRepository">The <see cref="IUserRepository"/> used to query and persist users.</param>
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Determines whether a user with the specified user name already exists.
        /// </summary>
        /// <param name="userName">The user name to test for existence.</param>
        /// <returns>True if a user with the username exists, otherwise false.</returns>
        public bool UserNameExists(string userName)
        {
            return this.userRepository.UserNameExists(userName);
        }

        /// <summary>
        /// Creates a new <see cref="User"/> and saves it to the repository.
        /// </summary>
        /// <param name="userName">The user name for the new user.</param>
        /// <param name="password">The password for the new user.</param>
        public void CreateUser(string userName, string password)
        {
            User user = new User(Guid.NewGuid(), userName, password);
            this.userRepository.AddUser(user);
        }

        /// <summary>
        /// Authenticates a user using the provided credentials.
        /// </summary>
        /// <param name="userName">The user name to authenticate.</param>
        /// <param name="password">The password to validate.</param>
        /// <returns>An <see cref="AuthenticatedUser"/> representing the authenticated user.</returns>
        public AuthenticatedUser Authenticate(string userName, string password)
        {
            User? user = this.userRepository.GetByUserName(userName);

            if (user == null || user.Password != password)
            {
                throw new UnauthorizedAccessException();
            }

            return new AuthenticatedUser(user.Id, user.UserName);
        }

        /// <summary>
        /// Deletes the user identified by <paramref name="userId"/>.
        /// </summary>
        /// <param name="userId">The identifier of the user to delete.</param>
        public void DeleteUser(Guid userId)
        {
            User user = this.userRepository.GetById(userId);
            this.userRepository.DeleteUser(user);
        }
    }
}
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
        private readonly HashService hashService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userRepository">The repository used to manage user data.</param>
        /// <param name="hashService">The service used to hash and verify passwords.</param>
        public UserService(IUserRepository userRepository, HashService hashService)
        {
            this.userRepository = userRepository;
            this.hashService = hashService;
        }

        /// <summary>
        /// Determines whether a user with the specified user name already exists.
        /// </summary>
        /// <param name="userName">The user name to test.</param>
        /// <returns>
        /// True if the user exists; otherwise, false.
        /// </returns>
        public bool UserNameExists(string userName)
        {
            return this.userRepository.UserNameExists(userName);
        }

        /// <summary>
        /// Creates a new user and stores a hashed password.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <param name="password">The plain text password.</param>
        public void CreateUser(string userName, string password)
        {
            string salt = this.hashService.GenerateSalt();
            string passwordHash = this.hashService.HashPassword(password, salt);
            User user = new User(Guid.NewGuid(), userName, passwordHash, salt);
            this.userRepository.AddUser(user);
        }

        /// <summary>
        /// Authenticates a user using the provided credentials.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <param name="password">The password.</param>
        /// <returns>An authenticated user.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown when the credentials are invalid.</exception>
        public AuthenticatedUser Authenticate(string userName, string password)
        {
            User? user = this.userRepository.GetByUserName(userName);
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            bool isValid = this.hashService.VerifyPassword(password, user.Salt, user.PasswordHash);

            if (!isValid)
            {
                throw new UnauthorizedAccessException();
            }

            return new AuthenticatedUser(user.Id, user.UserName);
        }

        /// <summary>
        /// Deletes the specified user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public void DeleteUser(Guid userId)
        {
            User user = this.userRepository.GetById(userId);
            this.userRepository.DeleteUser(user);
        }
    }
}
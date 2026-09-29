namespace ExpenseTracker.Model
{
    /// <summary>
    /// Model used to store the user credentials.
    /// </summary>
    internal class User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <param name="userName">The username of the user.</param>
        /// <param name="passwordHash">The password hash of the user.</param>
        /// <param name="salt">The specific salt for the password.</param>
        public User(Guid id, string userName, string passwordHash, string salt)
        {
            this.Id = id;
            this.UserName = userName;
            this.PasswordHash = passwordHash;
            this.Salt = salt;
        }

        /// <summary>
        /// Gets the ID of the user.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Gets the username of the user.
        /// </summary>
        public string UserName { get; }

        /// <summary>
        /// Gets the password hash of the user.
        /// </summary>
        public string PasswordHash { get; }

        /// <summary>
        /// Gets the specific salt for the password.
        /// </summary>
        public string Salt { get; }
    }
}
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
        /// <param name="password">The password of the user.</param>
        public User(Guid id, string userName, string password)
        {
            this.Id = id;
            this.UserName = userName;
            this.Password = password;
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
        /// Gets the password of the user.
        /// </summary>
        public string Password { get; }
    }
}
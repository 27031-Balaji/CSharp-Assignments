namespace ExpenseTracker.Model
{
    /// <summary>
    /// Model used to store the authenticated user details after login.
    /// </summary>
    internal class AuthenticatedUser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticatedUser"/> class.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <param name="userName">The username of the user.</param>
        public AuthenticatedUser(Guid id, string userName)
        {
            this.Id = id;
            this.UserName = userName;
        }

        /// <summary>
        /// Gets the ID of the user.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Gets the username of the user.
        /// </summary>
        public string UserName { get; }
    }
}
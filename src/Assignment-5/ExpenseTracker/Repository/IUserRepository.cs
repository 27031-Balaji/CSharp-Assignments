using ExpenseTracker.Model;

namespace ExpenseTracker.Repository
{
    /// <summary>
    /// Defines persistence operations for <see cref="User"/> entities.
    /// </summary>
    internal interface IUserRepository
    {
        /// <summary>
        /// Gets the total number of users stored by the repository.
        /// </summary>
        int UserCount { get; }

        /// <summary>
        /// Adds the specified <see cref="User"/> to the repository.
        /// </summary>
        /// <param name="user">The <see cref="User"/> to add.</param>
        void AddUser(User user);

        /// <summary>
        /// Retrieves the <see cref="User"/> with the specified ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>The matching <see cref="User"/>.</returns>
        User GetById(Guid userId);

        /// <summary>
        /// Retrieves the <see cref="User"/> with the specified user name.
        /// </summary>
        /// <param name="userName">The user name to search for.</param>
        /// <returns>The matching <see cref="User"/> if found.</returns>
        User? GetByUserName(string userName);

        /// <summary>
        /// Determines whether a user with the specified user name exists in the repository.
        /// </summary>
        /// <param name="userName">The user name to check for existence.</param>
        /// <returns>True if the user name exists, otherwise false.</returns>
        bool UserNameExists(string userName);

        /// <summary>
        /// Deletes the specified <see cref="User"/> from the repository.
        /// </summary>
        /// <param name="user">The <see cref="User"/> to delete.</param>
        void DeleteUser(User user);
    }
}
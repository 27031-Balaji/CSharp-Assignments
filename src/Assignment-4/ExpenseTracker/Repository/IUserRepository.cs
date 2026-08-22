using ExpenseTracker.Model;

namespace ExpenseTracker.Repository
{
    internal interface IUserRepository
    {
        int UserCount { get; }

        void AddUser(User user);

        User GetById(Guid userId);

        User GetByUserName(string userName);

        bool UserNameExists(string userName);

        void DeleteUser(User user);
    }
}
using ExpenseTracker.Model;
using ExpenseTracker.Repository;

namespace ExpenseTracker.Service
{
    internal class UserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public bool UserNameExists(string userName)
        {
            return this.userRepository.UserNameExists(userName);
        }

        public void CreateUser(string userName, string password)
        {
            User user = new User(Guid.NewGuid(), userName, password);
            this.userRepository.AddUser(user);
        }

        public AuthenticatedUser Authenticate(string userName, string password)
        {
            User user = this.userRepository.GetByUserName(userName);

            if (user.Password != password)
            {
                throw new UnauthorizedAccessException();
            }

            return new AuthenticatedUser(user.Id, user.UserName);
        }

        public void DeleteUser(Guid userId)
        {
            User user = this.userRepository.GetById(userId);
            this.userRepository.DeleteUser(user);
        }
    }
}
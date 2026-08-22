namespace ExpenseTracker.Model
{
    internal class AuthenticatedUser
    {
        public AuthenticatedUser(Guid id, string userName)
        {
            this.Id = id;
            this.UserName = userName;
        }

        public Guid Id { get; }

        public string UserName { get; }
    }
}
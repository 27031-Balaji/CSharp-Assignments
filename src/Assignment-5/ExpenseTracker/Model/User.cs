namespace ExpenseTracker.Model
{
    internal class User
    {
        public User(Guid id, string userName, string password)
        {
            this.Id = id;
            this.UserName = userName;
            this.Password = password;
        }

        public Guid Id { get; }

        public string UserName { get; }

        public string Password { get; }
    }
}

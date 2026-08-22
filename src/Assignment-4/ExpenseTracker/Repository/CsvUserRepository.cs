using ExpenseTracker.ConstantLiteral;
using ExpenseTracker.CsvUtils;
using ExpenseTracker.Model;

namespace ExpenseTracker.Repository
{
    internal class CsvUserRepository
    {
        private const string FilePath = Constant.UserFilePath;
        private const string CsvHeader = Constant.UserCsvHeader;
        private readonly List<User> users;
        private readonly CsvHandler csvHandler;

        public CsvUserRepository()
        {
            this.users = new List<User>();
            this.csvHandler = new CsvHandler(FilePath);
            this.ReadUsersFromFile();
        }

        public int UserCount { get => this.users.Count; }

        public void AddUser(User user)
        {
            List<string> lines = new List<string>();
            if (this.csvHandler.Exists())
            {
                lines.Add(CsvHeader);
            }

            lines.Add(this.ConvertToCsv(user));
            this.csvHandler.Append(lines);
            this.users.Add(user);
        }

        public User GetById(Guid userId)
        {
            return this.users.First(user => user.Id == userId);
        }

        public User GetByUsername(string userName)
        {
            return this.users.First(user => user.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
        }

        public bool UserNameExists(string userName)
        {
            return this.users.Any(user => user.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
        }

        public void DeleteUser(User user)
        {
            this.users.Remove(user);
            this.SaveUsersToFile();
        }

        private void ReadUsersFromFile()
        {
            List<string> lines = this.csvHandler.Read();
            foreach (string line in lines.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                this.users.Add(this.ParseToRecord(line));
            }
        }

        private void SaveUsersToFile()
        {
            List<string> lines = new List<string> { CsvHeader };
            foreach (User user in this.users)
            {
                lines.Add(this.ConvertToCsv(user));
            }

            this.csvHandler.Write(lines);
        }

        private string ConvertToCsv(User user)
        {
            return string.Join(
                ",",
                this.csvHandler.CsvEscape(user.Id.ToString()),
                this.csvHandler.CsvEscape(user.UserName),
                this.csvHandler.CsvEscape(user.Password));
        }

        private User ParseToRecord(string line)
        {
            List<string> values = this.csvHandler.ParseCsvLine(line);
            Guid id = Guid.Parse(values[0]);
            string userName = values[1];
            string password = values[2];

            return new User(id, userName, password);
        }
    }
}
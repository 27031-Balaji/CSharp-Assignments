using ExpenseTracker.ConstantLiteral;
using ExpenseTracker.CsvUtils;
using ExpenseTracker.Model;

namespace ExpenseTracker.Repository
{
    /// <summary>
    /// CSV implementation of <see cref="IUserRepository"/> that persists <see cref="User"/> records to a CSV file.
    /// </summary>
    internal class CsvUserRepository : IUserRepository
    {
        private readonly List<User> users;
        private readonly CsvHandler csvHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvUserRepository"/> class and loads existing users.
        /// </summary>
        public CsvUserRepository()
        {
            this.users = new List<User>();
            this.csvHandler = new CsvHandler(Constant.UserFilePath);
            this.ReadUsersFromFile();
        }

        /// <summary>
        /// Gets the number of users currently loaded in memory.
        /// </summary>
        public int UserCount { get => this.users.Count; }

        /// <summary>
        /// Adds the specified <paramref name="user"/> to the repository and appends it to the CSV file.
        /// </summary>
        /// <param name="user">The <see cref="User"/> to add.</param>
        public void AddUser(User user)
        {
            List<string> lines = new List<string>();
            if (!this.csvHandler.IsEmpty())
            {
                lines.Add(Constant.UserCsvHeader);
            }

            lines.Add(this.ConvertToCsv(user));
            this.csvHandler.Append(lines);
            this.users.Add(user);
        }

        /// <summary>
        /// Retrieves the <see cref="User"/> with the specified user ID.
        /// </summary>
        /// <param name="userId">The identifier of the user to retrieve.</param>
        /// <returns>The matching <see cref="User"/> if found.</returns>
        public User? GetById(Guid userId)
        {
            return this.users.FirstOrDefault(user => user.Id == userId);
        }

        /// <summary>
        /// Retrieves the <see cref="User"/> with the specified username, if any.
        /// </summary>
        /// <param name="userName">The user name to search for.</param>
        /// <returns>The matching <see cref="User"/> if found.</returns>
        public User? GetByUserName(string userName)
        {
            return this.users.FirstOrDefault(user => user.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Determines whether a user with the specified username exists in the repository.
        /// </summary>
        /// <param name="userName">The user name to check.</param>
        /// <returns>True if a matching user exists, otherwise false.</returns>
        public bool UserNameExists(string userName)
        {
            return this.users.Any(user => user.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Deletes the specified <see cref="User"/> from the repository and saves the change to the CSV file.
        /// </summary>
        /// <param name="user">The <see cref="User"/> to delete.</param>
        public void DeleteUser(User user)
        {
            this.users.Remove(user);
            this.SaveUsersToFile();
        }

        /// <summary>
        /// Reads users from the CSV file, skipping corrupted rows.
        /// </summary>
        private void ReadUsersFromFile()
        {
            List<string> lines = this.csvHandler.Read();
            foreach (string line in lines.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                try
                {
                    this.users.Add(this.ParseToRecord(line));
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        /// <summary>
        /// Writes all in-memory users to the CSV file.
        /// </summary>
        private void SaveUsersToFile()
        {
            List<string> lines = new List<string> { Constant.UserCsvHeader };
            foreach (User user in this.users)
            {
                lines.Add(this.ConvertToCsv(user));
            }

            this.csvHandler.Write(lines);
        }

        /// <summary>
        /// Converts a <see cref="User"/> into a CSV line.
        /// </summary>
        /// <param name="user">The <see cref="User"/> to convert.</param>
        /// <returns>A CSV-escaped string representing the user data.</returns>
        private string ConvertToCsv(User user)
        {
            return string.Join(
                ",",
                this.csvHandler.CsvEscape(user.Id.ToString()),
                this.csvHandler.CsvEscape(user.UserName),
                this.csvHandler.CsvEscape(user.PasswordHash),
                this.csvHandler.CsvEscape(user.Salt));
        }

        /// <summary>
        /// Parses a CSV line into a <see cref="User"/> record.
        /// </summary>
        /// <param name="line">The CSV line to parse.</param>
        /// <returns>A <see cref="User"/> instance created from the parsed CSV values.</returns>
        private User ParseToRecord(string line)
        {
            List<string> values = this.csvHandler.ParseCsvLine(line);

            Guid id = Guid.Parse(values[0]);
            string userName = values[1];
            string passwordHash = values[2];
            string salt = values[3];

            return new User(id, userName, passwordHash, salt);
        }
    }
}
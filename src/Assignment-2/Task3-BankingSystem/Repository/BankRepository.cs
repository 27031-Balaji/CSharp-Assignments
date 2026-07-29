using BankingSystem.Models;

namespace BankingSystem.Repository
{
    /// <summary>
    /// Stores and manages bank account records.
    /// </summary>
    internal class BankRepository
    {
        private readonly List<BankAccount> _accounts;

        /// <summary>
        /// Initializes a new instance of the <see cref="BankRepository"/> class.
        /// </summary>
        public BankRepository()
        {
            this._accounts = new List<BankAccount>();
        }

        /// <summary>
        /// Gets the total number of bank accounts in the repository.
        /// </summary>
        /// <value>The count of accounts in the repository.</value>
        public int AccountCount { get => this._accounts.Count; }

        /// <summary>
        /// Adds a bank account to the repository.
        /// </summary>
        /// <param name="account">The bank account to be added.</param>
        public void AddAccount(BankAccount account)
        {
            this._accounts.Add(account);
        }

        /// <summary>
        /// Retrieves all bank accounts from the repository.
        /// </summary>
        /// <returns>A list containing all bank accounts.</returns>
        public List<BankAccount> GetAllAccounts()
        {
            return this._accounts;
        }

        /// <summary>
        /// Retrieves the bank account with the specified account number.
        /// </summary>
        /// <param name="accountNumber">The account number to search for.</param>
        /// <returns>The matching bank account if found, otherwise null.</returns>
        public BankAccount? GetAccountByNumber(string accountNumber)
        {
            return this._accounts.Find(account => account.AccountNumber == accountNumber);
        }

        /// <summary>
        /// Checks whether a bank account with the specified account number exists.
        /// </summary>
        /// <param name="accountNumber">The account number to search for.</param>
        /// <returns>True if the account exists, otherwise false.</returns>
        public bool AccountExists(string accountNumber)
        {
            return this.GetAccountByNumber(accountNumber) != null;
        }
    }
}
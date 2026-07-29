using BankingSystem.Models;
using BankingSystem.Repository;

namespace BankingSystem.Services
{
    /// <summary>
    /// Provides business logic for bank account operations.
    /// </summary>
    internal class BankServices
    {
        /// <summary>
        /// Represents the minimum initial deposit required for a savings account.
        /// </summary>
        private const decimal MinimumBalance = 1000;

        private readonly BankRepository _repository;
        private readonly Random _random = new Random();

        /// <summary>
        /// Initializes a new instance of the <see cref="BankServices"/> class.
        /// </summary>
        /// <param name="repository">The repository used to manage bank account records.</param>
        public BankServices(BankRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Creates a savings account.
        /// </summary>
        /// <param name="initialDeposit">The initial deposit.</param>
        /// <returns>The created savings account, or null if the deposit is insufficient.</returns>
        public SavingsAccount? CreateSavingsAccount(decimal initialDeposit)
        {
            if (initialDeposit < MinimumBalance)
            {
                return null;
            }

            SavingsAccount account = new SavingsAccount(this.GenerateAccountNumber(), initialDeposit);
            this._repository.AddAccount(account);

            return account;
        }

        /// <summary>
        /// Creates a checking account.
        /// </summary>
        /// <param name="initialDeposit">The initial deposit.</param>
        /// <returns>The created checking account.</returns>
        public CheckingAccount CreateCheckingAccount(decimal initialDeposit)
        {
            CheckingAccount account = new CheckingAccount(this.GenerateAccountNumber(), initialDeposit);
            this._repository.AddAccount(account);

            return account;
        }

        /// <summary>
        /// Retrieves all bank accounts.
        /// </summary>
        /// <returns>A list of bank accounts.</returns>
        public List<BankAccount> GetAllAccounts()
        {
            return this._repository.GetAllAccounts();
        }

        /// <summary>
        /// Deposits an amount into the specified account.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <param name="amount">The amount to deposit.</param>
        /// <returns>The updated account if successful, otherwise null.</returns>
        public BankAccount? Deposit(string accountNumber, decimal amount)
        {
            BankAccount? account = this._repository.GetAccountByNumber(accountNumber);

            if (account == null)
            {
                return null;
            }

            account.Deposit(amount);

            return account;
        }

        /// <summary>
        /// Withdraws an amount from the specified account.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <param name="amount">The amount to withdraw.</param>
        /// <returns>The updated account if successful, otherwise null.</returns>
        public BankAccount? Withdraw(string accountNumber, decimal amount)
        {
            BankAccount? account = this._repository.GetAccountByNumber(accountNumber);
            if (account == null)
            {
                return null;
            }

            if (!account.Withdraw(amount))
            {
                return null;
            }

            return account;
        }

        /// <summary>
        /// Checks whether an account exists.
        /// </summary>
        /// <param name="accountNumber">The account number.</param>
        /// <returns>True if the account exists; otherwise, false.</returns>
        public bool AccountExists(string accountNumber)
        {
            return this._repository.AccountExists(accountNumber);
        }

        /// <summary>
        /// Checks whether any accounts are available.
        /// </summary>
        /// <returns>True if accounts exist; otherwise, false.</returns>
        public bool HasAccounts()
        {
            return this._repository.AccountCount > 0;
        }

        /// <summary>
        /// Generates a unique ten-digit account number.
        /// </summary>
        /// <returns>A unique account number.</returns>
        private string GenerateAccountNumber()
        {
            string accountNumber;
            do
            {
                accountNumber = string.Empty;
                for (int i = 0; i < 10; i++)
                {
                    accountNumber += this._random.Next(10);
                }
            }
            while (this._repository.AccountExists(accountNumber));

            return accountNumber;
        }
    }
}
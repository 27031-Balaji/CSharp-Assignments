using BankingSystem.Persistence;
using Task3.Models;

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

        /// <summary>
        /// Initializes a new instance of the <see cref="BankServices"/> class.
        /// </summary>
        /// <param name="repository">The repository used to manage bank account records.</param>
        public BankServices(BankRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Creates a new savings account with the specified initial deposit.
        /// </summary>
        /// <param name="initialDeposit">The initial amount to be deposited.</param>
        /// <returns>A success or failure message based on the account creation.</returns>
        public string CreateSavingsAccount(decimal initialDeposit)
        {
            if (initialDeposit < MinimumBalance)
            {
                return $"Initial deposit must be at least Rs. {MinimumBalance}.\n";
            }

            string accountNumber = this.GenerateAccountNumber();
            SavingsAccount account = new SavingsAccount(accountNumber, initialDeposit);
            this._repository.AddAccount(account);
            return $"Savings Account Created Successfully.\n\n{account.PrintDetails()}\n";
        }

        /// <summary>
        /// Creates a new checking account with the specified initial deposit.
        /// </summary>
        /// <param name="initialDeposit">The initial amount to be deposited.</param>
        /// <returns>A success message after the account is created.</returns>
        public string CreateCheckingAccount(decimal initialDeposit)
        {
            string accountNumber = this.GenerateAccountNumber();
            CheckingAccount account = new CheckingAccount(accountNumber, initialDeposit);
            this._repository.AddAccount(account);
            return $"Checking Account Created Successfully.\n\n{account.PrintDetails()}\n";
        }

        /// <summary>
        /// Retrieves all bank accounts.
        /// </summary>
        /// <returns>A list containing all bank accounts.</returns>
        public List<BankAccount> GetAllAccounts()
        {
            return this._repository.GetAllAccounts();
        }

        /// <summary>
        /// Deposits the specified amount into the given bank account.
        /// </summary>
        /// <param name="accountNumber">The account number of the bank account.</param>
        /// <param name="amount">The amount to be deposited.</param>
        /// <returns>A success or failure message based on the deposit operation.</returns>
        public string Deposit(string accountNumber, decimal amount)
        {
            BankAccount? account = this._repository.GetAccountByNumber(accountNumber);

            if (account == null)
            {
                return "Account not found.";
            }

            account.Deposit(amount);
            return $"Deposit successful.\nCurrent Balance : Rs. {account.Balance}\n";
        }

        /// <summary>
        /// Withdraws the specified amount from the given bank account.
        /// </summary>
        /// <param name="accountNumber">The account number of the bank account.</param>
        /// <param name="amount">The amount to be withdrawn.</param>
        /// <returns>A success or failure message based on the withdrawal operation.</returns>
        public string Withdraw(string accountNumber, decimal amount)
        {
            BankAccount? account = this._repository.GetAccountByNumber(accountNumber);

            if (account == null)
            {
                return "Account not found.\n";
            }

            if (!account.Withdraw(amount))
            {
                return "Withdrawal failed. Please withdraw the right amount.\n";
            }

            return $"Withdrawal successful.\nCurrent Balance : Rs. {account.Balance}\n";
        }

        /// <summary>
        /// Checks whether a bank account with the specified account number exists.
        /// </summary>
        /// <param name="accountNumber">The account number to search for.</param>
        /// <returns>True if the account exists; otherwise, false.</returns>
        public bool AccountExists(string accountNumber)
        {
            return this._repository.AccountExists(accountNumber);
        }

        /// <summary>
        /// Checks whether any bank accounts are available.
        /// </summary>
        /// <returns>True if at least one account exists; otherwise, false.</returns>
        public bool HasAccounts()
        {
            return this._repository.GetAllAccounts().Any();
        }

        /// <summary>
        /// Generates a unique ten-digit account number.
        /// </summary>
        /// <returns>A unique account number.</returns>
        private string GenerateAccountNumber()
        {
            Random random = new Random();
            string accountNumber;
            do
            {
                accountNumber = string.Empty;
                for (int i = 0; i < 10; i++)
                {
                    accountNumber += random.Next(0, 10);
                }
            }
            while (this._repository.AccountExists(accountNumber)); // Duplicate check to ensure uniqueness
            return accountNumber;
        }
    }
}
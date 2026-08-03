namespace BankingSystem.Classes
{
    /// <summary>
    /// Represents the base class for all bank accounts.
    /// </summary>
    public abstract class BankAccount
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BankAccount"/> class.
        /// </summary>
        /// <param name="accountNumber">The unique account number assigned to the bank account.</param>
        /// <param name="initialBalance">The initial balance of the bank account.</param>
        protected BankAccount(string accountNumber, decimal initialBalance)
        {
            this.AccountNumber = accountNumber;
            this.Balance = initialBalance;
        }

        /// <summary>
        /// Gets the account number of the bank account.
        /// </summary>
        /// <value>The account number as a string.</value>
        public string AccountNumber { get; }

        /// <summary>
        /// Gets or sets the current balance of the bank account.
        /// </summary>
        /// <value>The balance as a decimal.</value>
        public decimal Balance { get; protected set; }

        /// <summary>
        /// Deposits the specified amount into the bank account.
        /// </summary>
        /// <param name="amount">The amount to be deposited.</param>
        public void Deposit(decimal amount)
        {
            this.Balance += amount;
        }

        /// <summary>
        /// Withdraws the specified amount from the bank account.
        /// </summary>
        /// <param name="amount">The amount to be withdrawn.</param>
        /// <returns>True if the withdrawal is successful, otherwise false.</returns>
        public abstract bool Withdraw(decimal amount);

        /// <summary>
        /// Returns the account details as a string.
        /// </summary>
        /// <returns>A string containing the account number and balance.</returns>
        public virtual string PrintDetails()
        {
            return $"Account Number : {this.AccountNumber}\nBalance : Rs. {this.Balance}";
        }
    }
}
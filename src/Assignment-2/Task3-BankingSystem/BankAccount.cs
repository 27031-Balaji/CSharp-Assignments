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
        /// <param name="accountNumber">The unique account number assigned to the <see cref="BankAccount"/>.</param>
        /// <param name="initialBalance">The initial balance of the <see cref="BankAccount"/>.</param>
        protected BankAccount(string accountNumber, decimal initialBalance)
        {
            this.AccountNumber = accountNumber;
            this.Balance = initialBalance;
        }

        /// <summary>
        /// Gets the account number of the <see cref="BankAccount"/>.
        /// </summary>
        /// <value>The account number of the <see cref="BankAccount"/>.</value>
        public string AccountNumber { get; }

        /// <summary>
        /// Gets or sets the current balance of the <see cref="BankAccount"/>.
        /// </summary>
        /// <value>The balance as a decimal.</value>
        public decimal Balance { get; protected set; }

        /// <summary>
        /// Deposits the specified amount into the <see cref="BankAccount"/>.
        /// </summary>
        /// <param name="amount">The amount to be deposited to the <see cref="BankAccount"/>.</param>
        public void Deposit(decimal amount)
        {
            this.Balance += amount;
        }

        /// <summary>
        /// Withdraws the specified amount from the <see cref="BankAccount"/>.
        /// </summary>
        /// <param name="amount">The amount to be withdrawn from the <see cref="BankAccount"/>.</param>
        /// <returns>True if the withdrawal is successful, otherwise false.</returns>
        public abstract bool Withdraw(decimal amount);

        /// <summary>
        /// Returns the account details.
        /// </summary>
        /// <returns>A string containing the account number and balance of the <see cref="BankAccount"/>.</returns>
        public virtual string PrintDetails()
        {
            return $"Account Number : {this.AccountNumber}\nBalance : Rs. {this.Balance}";
        }
    }
}
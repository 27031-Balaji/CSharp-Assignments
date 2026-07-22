namespace Task3.Models
{
    /// <summary>
    /// Represents a savings account that requires a minimum balance to be maintained.
    /// </summary>
    public class SavingsAccount : BankAccount
    {
        /// <summary>
        /// Represents the minimum balance that must be maintained in a savings account.
        /// </summary>
        private const decimal MinimumBalance = 1000;

        /// <summary>
        /// Initializes a new instance of the <see cref="SavingsAccount"/> class.
        /// </summary>
        /// <param name="accountNumber">The unique account number assigned to the savings account.</param>
        /// <param name="initialBalance">The initial balance of the savings account.</param>
        public SavingsAccount(string accountNumber, decimal initialBalance)
            : base(accountNumber, initialBalance)
        {
        }

        /// <summary>
        /// Withdraws the specified amount from the savings account if the minimum balance is maintained.
        /// </summary>
        /// <param name="amount">The amount to be withdrawn.</param>
        /// <returns>True if the withdrawal is successful, otherwise false.</returns>
        public override bool Withdraw(decimal amount)
        {
            if (this.Balance - amount < MinimumBalance)
            {
                return false;
            }

            this.Balance -= amount;
            return true;
        }

        /// <summary>
        /// Returns the savings account details as a string.
        /// </summary>
        /// <returns>A string containing the account type and account details.</returns>
        public override string PrintDetails()
        {
            return $"Account Type : Savings\n{base.PrintDetails()}\n";
        }
    }
}
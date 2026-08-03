namespace BankingSystem.Classes
{
    /// <summary>
    /// Represents a checking account that allows withdrawals without maintaining a minimum balance.
    /// </summary>
    public class CheckingAccount : BankAccount
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckingAccount"/> class.
        /// </summary>
        /// <param name="accountNumber">The unique account number assigned to the checking account.</param>
        /// <param name="initialBalance">The initial balance of the checking account.</param>
        public CheckingAccount(string accountNumber, decimal initialBalance)
            : base(accountNumber, initialBalance)
        {
        }

        /// <summary>
        /// Withdraws the specified amount from the checking account if sufficient amount is available.
        /// </summary>
        /// <param name="amount">The amount to be withdrawn.</param>
        /// <returns>True if the withdrawal is successful, otherwise false.</returns>
        public override bool Withdraw(decimal amount)
        {
            if (amount > this.Balance)
            {
                return false;
            }

            this.Balance -= amount;
            return true;
        }

        /// <summary>
        /// Returns the checking account details as a string.
        /// </summary>
        /// <returns>A string containing the account type and account details.</returns>
        public override string PrintDetails()
        {
            return $"Account Type : Checking\n{base.PrintDetails()}\n";
        }
    }
}
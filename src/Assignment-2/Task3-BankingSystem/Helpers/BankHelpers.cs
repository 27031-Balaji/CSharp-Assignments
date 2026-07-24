namespace BankingSystem.Helpers
{
    /// <summary>
    /// Provides validation methods for banking operations.
    /// </summary>
    internal class BankHelpers
    {
        /// <summary>
        /// Validates whether the entered amount is a valid positive decimal value.
        /// </summary>
        /// <param name="input">The amount string entered by the user.</param>
        /// <param name="amount">The converted decimal amount if the validation succeeds.</param>
        /// <returns>True if the amount is a valid positive decimal, otherwise false.</returns>
        public bool IsValidAmount(string input, out decimal amount)
        {
            return decimal.TryParse(input, out amount) && amount > 0;
        }

        /// <summary>
        /// Validates whether the account number contains exactly ten digits.
        /// </summary>
        /// <param name="accountNumber">The account number to be validated.</param>
        /// <returns>True if the account number contains exactly ten digits, otherwise false.</returns>
        public bool IsValidAccountNumber(string accountNumber)
        {
            return accountNumber.Length == 10 && accountNumber.All(char.IsDigit);
        }
    }
}
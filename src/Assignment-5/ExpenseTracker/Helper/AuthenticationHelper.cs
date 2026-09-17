using ExpenseTracker.ConstantLiteral;

namespace ExpenseTracker.Helper
{
    /// <summary>
    /// Helper class used to validate the user input for signup and login.
    /// </summary>
    internal class AuthenticationHelper
    {
        /// <summary>
        /// Determines whether the specified user name does not exceed the maximum allowed length.
        /// </summary>
        /// <param name="userName">The user name to validate.</param>
        /// <returns>True if the user name is valid, otherwise false.</returns>
        public bool IsValidUserName(string userName)
        {
            return !string.IsNullOrWhiteSpace(userName) && userName.Length <= Constant.MaxLengthOfNameAndPassword;
        }

        /// <summary>
        /// Determines whether the specified user entered password that does not exceed the maximum allowed length.
        /// </summary>
        /// <param name="password">The password to validate.</param>
        /// <returns>True if the password is valid, otherwise false.</returns>
        public bool IsValidPassword(string password)
        {
            return !string.IsNullOrWhiteSpace(password) && password.Length <= Constant.MaxLengthOfNameAndPassword;
        }

        /// <summary>
        /// Validates that an input string represents a numeric choice within the allowed range.
        /// </summary>
        /// <param name="input">The input string containing the user's numeric choice.</param>
        /// <param name="maxChoice">The maximum valid choice value.</param>
        /// <param name="choice">When this method returns, contains the parsed numeric choice if valid.</param>
        /// <returns>True if input is an integer between 1 and maximum choice inclusive, otherwise false.</returns>
        public bool IsValidChoice(string input, int maxChoice, out int choice)
        {
            return int.TryParse(input, out choice) && choice >= 1 && choice <= maxChoice;
        }
    }
}
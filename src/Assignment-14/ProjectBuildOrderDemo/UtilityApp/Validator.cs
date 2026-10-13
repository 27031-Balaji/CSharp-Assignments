namespace UtilityApp
{
    /// <summary>
    /// Validates the input given by the user.
    /// </summary>
    public class Validator
    {
        /// <summary>
        /// Checks whether the number is valid or not.
        /// </summary>
        /// <param name="input">The input to be validated.</param>
        /// <param name="number">The number entered by the user, if validated.</param>
        /// <returns>True if the number entered is valid, else false.</returns>
        public bool IsValidNumber(string input, out int number)
        {
            return int.TryParse(input, out number);
        }

        /// <summary>
        /// Checks whether the operator input entered by the user is valid or not.
        /// </summary>
        /// <param name="input">The input to be validated.</param>
        /// <returns>True if the user entered the valid operator, else false.</returns>
        public bool IsValidOperator(string input)
        {
            return input == "+" || input == "-" || input == "*" || input == "/";
        }

        /// <summary>
        /// Checks whether the continue choice entered by the user is valid.
        /// </summary>
        /// <param name="input">The input to validate.</param>
        /// <returns>True if the input is Y or N, else false.</returns>
        public bool IsValidContinueChoice(string input)
        {
            return input.Equals("Y", StringComparison.OrdinalIgnoreCase) || input.Equals("N", StringComparison.OrdinalIgnoreCase);
        }
    }
}
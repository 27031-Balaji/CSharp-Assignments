namespace Task2.Helpers
{
    /// <summary>
    /// Provides helper methods to validate employee input values.
    /// </summary>
    internal class EmployeeHelpers
    {
        /// <summary>
        /// Determines whether the specified name is valid.
        /// </summary>
        /// <param name="name">The name string to validate.</param>
        /// <returns>True if name is not null, empty, or whitespace, otherwise false.</returns>
        public bool IsValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name);
        }

        /// <summary>
        /// Determines whether the input string represents a positive decimal number.
        /// </summary>
        /// <param name="input">The input string representing the number to evaluate.</param>
        /// <param name="number">This is an out variable so it can be used in the main program.</param>
        /// <returns>True if input can be parsed as a decimal and is greater than zero, otherwise false.</returns>
        public bool IsValidPositiveNumber(string input, out decimal number)
        {
            return decimal.TryParse(input, out number) && number > 0;
        }
    }
}

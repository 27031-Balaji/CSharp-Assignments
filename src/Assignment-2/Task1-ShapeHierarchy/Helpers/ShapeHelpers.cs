using System.Drawing;

namespace Task1.Helpers
{
    /// <summary>
    /// Provides helper methods to validate user input for shape creation.
    /// </summary>
    internal class ShapeHelpers
    {
        /// <summary>
        /// Determines whether the specified color string is valid.
        /// </summary>
        /// <param name="color">The color string to validate.</param>
        /// <returns>True if color is not null, empty, or whitespace, otherwise false.</returns>
        public bool IsValidColor(string color)
        {
            return !string.IsNullOrWhiteSpace(color) && Color.FromName(color).IsKnownColor;
        }

        /// <summary>
        /// Determines whether the specified choice is valid.
        /// </summary>
        /// <param name="choice">The choice character to validate.</param>
        /// <returns>True if choice is A or B and not null or whitespace, else false.</returns>
        public bool IsValidChoice(char choice)
        {
            return choice == 'A' || choice == 'B';
        }

        /// <summary>
        /// Determines whether the input is a positive number.
        /// </summary>
        /// <param name="input">The input string of the number to evaluate</param>
        /// <param name="number">The parsed number if valid, otherwise 0.</param>
        /// <returns>True if number is greater than zero, otherwise false.</returns>
        public bool IsValidPositiveNumber(string input, out double number)
        {
            return double.TryParse(input, out number) && number > 0;
        }
    }
}
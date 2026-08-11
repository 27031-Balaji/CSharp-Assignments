using ExpenseTracker.Enums;

namespace ExpenseTracker.Helper
{
    /// <summary>
    /// Provides input validation and classification helpers used by controller.
    /// </summary>
    internal class ExpenseHelper
    {
        /// <summary>
        /// Determines whether the provided input can be parsed as a date using the format "dd/MM/yyyy".
        /// </summary>
        /// <param name="input">The input string representing a date.</param>
        /// <param name="date">When this method returns, contains the parsed date value if parsing succeeded.</param>
        /// <returns>True if the input was parsed successfully to a date, otherwise false.</returns>
        public bool IsValidDate(string? input, out DateOnly date)
        {
            return DateOnly.TryParseExact(input, "dd/MM/yyyy", out date) && date <= DateOnly.FromDateTime(DateTime.Now);
        }

        /// <summary>
        /// Determines whether the provided input can be parsed to a positive decimal amount.
        /// </summary>
        /// <param name="input">The input string representing an amount.</param>
        /// <param name="amount">When this method returns, contains the parsed amount if parsing succeeded.</param>
        /// <returns>True if the input was parsed to a decimal greater than zero, otherwise false.</returns>
        public bool IsValidAmount(string? input, out decimal amount)
        {
            return decimal.TryParse(input, out amount) && amount > 0;
        }

        /// <summary>
        /// Validates that an input string represents a numeric classification choice within the allowed range.
        /// </summary>
        /// <param name="input">The input string containing the user's numeric choice.</param>
        /// <param name="maxChoice">The maximum valid choice value.</param>
        /// <param name="choice">When this method returns, contains the parsed numeric choice if valid.</param>
        /// <returns>True if <paramref name="input"/> is an integer between 1 and <paramref name="maxChoice"/> inclusive; otherwise, false.</returns>
        public bool IsValidClassificationChoice(string input, int maxChoice, out int choice)
        {
            return int.TryParse(input, out choice) && choice >= 1 && choice <= maxChoice;
        }

        /// <summary>
        /// Determines whether the provided input corresponds to a defined <see cref="IncomeSource"/>.
        /// </summary>
        /// <param name="input">The input string representing an income source.</param>
        /// <param name="source">When this method returns, contains the parsed <see cref="IncomeSource"/> if valid.</param>
        /// <returns>True if <paramref name="input"/> maps to a defined <see cref="IncomeSource"/>; otherwise, false.</returns>
        public bool IsValidSource(string? input, out IncomeSource source)
        {
            return System.Enum.TryParse(input, ignoreCase: true, out source) && System.Enum.IsDefined(source);
        }

        /// <summary>
        /// Determines whether the provided input corresponds to a defined <see cref="ExpenseCategory"/>.
        /// </summary>
        /// <param name="input">The input string representing an expense category.</param>
        /// <param name="category">When this method returns, contains the parsed <see cref="ExpenseCategory"/> if valid.</param>
        /// <returns>True if <paramref name="input"/> maps to a defined <see cref="ExpenseCategory"/>; otherwise, false.</returns>
        public bool IsValidCategory(string? input, out ExpenseCategory category)
        {
            return System.Enum.TryParse(input, ignoreCase: true, out category) && System.Enum.IsDefined(category);
        }

        /// <summary>
        /// Parses and validates a month/year value provided in "MM/yyyy" format.
        /// </summary>
        /// <param name="input">The input string containing month and year in MM/YYYY format.</param>
        /// <param name="month">When this method returns, contains the parsed month (1-12) if successful.</param>
        /// <param name="year">When this method returns, contains the parsed year if successful.</param>
        /// <returns>True if the input was parsed successfully to a month and year; otherwise, false.</returns>
        public bool IsValidMonthAndYear(string? input, out int month, out int year)
        {
            month = 0;
            year = 0;

            if (DateOnly.TryParseExact(input, "MM/yyyy", out DateOnly date))
            {
                month = date.Month;
                year = date.Year;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Validates whether the provided record identifier matches the expected format used by the application.
        /// </summary>
        /// <param name="recordId">The record identifier to validate.</param>
        /// <returns>True if <paramref name="recordId"/> is a 12-character alphanumeric string; otherwise, false.</returns>
        public bool IsValidRecordId(string? recordId)
        {
            return !string.IsNullOrWhiteSpace(recordId) && recordId.Length == 12 && recordId.All(char.IsLetterOrDigit);
        }

        /// <summary>
        /// Classifies the provided search input into a <see cref="SearchType"/> for routing to the appropriate search method.
        /// </summary>
        /// <param name="input">The user-provided search input (date, amount, source, or category).</param>
        /// <returns>
        /// A <see cref="SearchType"/> value that describes how the input should be handled by search logic.
        /// </returns>
        public SearchType ReturnSearchType(string? input)
        {
            if (this.IsValidDate(input, out _))
            {
                return SearchType.Date;
            }

            if (this.IsValidAmount(input, out _))
            {
                return SearchType.Amount;
            }

            if (this.IsValidSource(input, out _))
            {
                return SearchType.Source;
            }

            if (this.IsValidCategory(input, out _))
            {
                return SearchType.Category;
            }

            return SearchType.Invalid;
        }
    }
}
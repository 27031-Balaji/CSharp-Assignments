namespace ExpenseTracker.Enums
{
    /// <summary>
    /// Classifies user input for search operations performed against <see cref="Model.FinancialRecord"/>.
    /// </summary>
    public enum SearchType
    {
        /// <summary>
        /// The input could not be classified as a valid search type.
        /// </summary>
        Invalid,

        /// <summary>
        /// The input represents a date value for searching <see cref="Model.FinancialRecord"/>.
        /// </summary>
        Date,

        /// <summary>
        /// The input represents an amount value for searching <see cref="Model.FinancialRecord"/>.
        /// </summary>
        Amount,

        /// <summary>
        /// The input represents an income source for searching <see cref="Model.Income"/>.
        /// </summary>
        Source,

        /// <summary>
        /// The input represents an expense category for searching <see cref="Model.Expense"/>.
        /// </summary>
        Category,
    }
}
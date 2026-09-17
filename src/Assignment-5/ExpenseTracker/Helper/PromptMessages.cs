namespace ExpenseTracker.Helper
{
    /// <summary>
    /// Contains console prompt messages displayed to the user.
    /// </summary>
    internal static class PromptMessages
    {
        /// <summary>
        /// Prompt for entering a record date.
        /// </summary>
        public const string RecordDate = "Enter the date of the record in (DD/MM/YYYY) or press Enter for today's date: ";

        /// <summary>
        /// Prompt for entering a start date.
        /// </summary>
        public const string StartDate = "Enter the start date in (DD/MM/YYYY) or press Enter to skip: ";

        /// <summary>
        /// Prompt for entering an end date.
        /// </summary>
        public const string EndDate = "Enter the end date in (DD/MM/YYYY) or press Enter to skip: ";

        /// <summary>
        /// Prompt for entering a record amount.
        /// </summary>
        public const string Amount = "Enter the amount of the record: ";

        /// <summary>
        /// Prompt for entering a record description.
        /// </summary>
        public const string Description = "Enter the description of the record (Optional): ";

        /// <summary>
        /// Prompt for searching records.
        /// </summary>
        public const string SearchKey = "Enter the date (DD/MM/YYYY) or amount or source/category: ";

        /// <summary>
        /// Prompt for entering a month and year.
        /// </summary>
        public const string MonthAndYear = "Enter the month and year in (MM/YYYY): ";

        /// <summary>
        /// Prompt for selecting a menu option.
        /// </summary>
        public const string MenuOption = "Select an option: ";

        /// <summary>
        /// Prompt for entering a record identifier.
        /// </summary>
        public const string RecordId = "Enter the record ID to {0}: ";

        /// <summary>
        /// Prompt for confirming any action.
        /// </summary>
        public const string ConfirmAction = "Are you sure you want to {0}? (Y/N): ";

        /// <summary>
        /// Prompt for entering a username.
        /// </summary>
        public const string UserName = "Enter username: ";

        /// <summary>
        /// Prompt for entering a password.
        /// </summary>
        public const string Password = "Enter password: ";
    }
}
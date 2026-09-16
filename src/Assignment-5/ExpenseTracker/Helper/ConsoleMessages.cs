namespace ExpenseTracker.Helper
{
    /// <summary>
    /// Used to store messages used for printing in the UI.
    /// </summary>
    internal static class ConsoleMessages
    {
        /// <summary>
        /// Message displayed when login operation is successful.
        /// </summary>
        public const string LoginSuccessMessage = "Login successful.";

        /// <summary>
        /// Message displayed when the user enters the wrong credentials.
        /// </summary>
        public const string InvalidLoginMessage = "Invalid username or password.";

        /// <summary>
        /// Message displayed when the username already exists in the repository.
        /// </summary>
        public const string UsernameExistsMessage = "Username already exists.";

        /// <summary>
        /// Message displayed when the account is created successfully.
        /// </summary>
        public const string AccountCreatedMessage = "Account created successfully.";

        /// <summary>
        /// Message displayed when the account is deleted successfully.
        /// </summary>
        public const string AccountDeletedMessage = "Account deleted successfully.";

        /// <summary>
        /// Message displayed when the logout operation is done successfully.
        /// </summary>
        public const string LogoutSuccessMessage = "Account logged out successfully.";

        /// <summary>
        /// Message displayed when an invalid menu option is selected.
        /// </summary>
        public const string InvalidOptionMessage = "Enter a valid option.";

        /// <summary>
        /// Message displayed when no records are found.
        /// </summary>
        public const string NoRecordFoundMessage = "No records found.";

        /// <summary>
        /// Message displayed when the delete operation is aborted by the user.
        /// </summary>
        public const string DeleteOperationAbortedMessage = "Delete operation aborted.";

        /// <summary>
        /// Message displayed when the delete operation failed.
        /// </summary>
        public const string DeleteOperationFailedMessage = "Delete operation failed.";

        /// <summary>
        /// Message displayed when the delete operation completes successfully.
        /// </summary>
        public const string DeleteOperationSuccessMessage = "Delete operation successful.";

        /// <summary>
        /// Message displayed when the edit operation completes successfully.
        /// </summary>
        public const string EditOperationSuccessMessage = "Edit operation successful.";

        /// <summary>
        /// Message displayed when the edit operation failed.
        /// </summary>
        public const string EditOperationFailedMessage = "Edit operation failed.";

        /// <summary>
        /// Message displayed when an income record is added successfully.
        /// </summary>
        public const string IncomeAddedMessage = "Income record added successfully.";

        /// <summary>
        /// Message displayed when an expense record is added successfully.
        /// </summary>
        public const string ExpenseAddedMessage = "Expense record added successfully.";

        /// <summary>
        /// Message displayed when a record's date is edited successfully.
        /// </summary>
        public const string DateEditedSuccessMessage = "Date edited successfully.";

        /// <summary>
        /// Message displayed when a record's amount is edited successfully.
        /// </summary>
        public const string AmountEditedSuccessMessage = "Amount edited successfully.";

        /// <summary>
        /// Message displayed when a record's classification (source/category) is edited successfully.
        /// </summary>
        public const string ClassificationEditedSuccessMessage = "Classification edited successfully.";

        /// <summary>
        /// Message displayed when a record's description is edited successfully.
        /// </summary>
        public const string DescriptionEditedSuccessMessage = "Description edited successfully.";

        /// <summary>
        /// Message displayed when the record is of an invalid type.
        /// </summary>
        public const string InvalidRecordTypeMessage = "Unknown record type.";

        /// <summary>
        /// Message displayed when the record is in an invalid format.
        /// </summary>
        public const string InvalidFormatMessage = "The records file contains data in an invalid format.";

        /// <summary>
        /// Message displayed when the record file does not exist.
        /// </summary>
        public const string FileNotFoundMessage = "The records file could not be found.";

        /// <summary>
        /// Message displayed when the start date is greater than the end date in the date range.
        /// </summary>
        public const string InvalidDateRangeMessage = "Start date is greater than the end date.";
    }
}
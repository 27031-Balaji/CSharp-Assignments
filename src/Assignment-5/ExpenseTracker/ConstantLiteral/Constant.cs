namespace ExpenseTracker.ConstantLiteral
{
    /// <summary>
    /// Provides constants used across the application.
    /// </summary>
    internal static class Constant
    {
        /// <summary>
        /// Provides the maximum length of the transaction id.
        /// </summary>
        internal const int MaxLengthOfId = 8;

        /// <summary>
        /// Provides the maxiumum length for the username and password.
        /// </summary>
        internal const int MaxLengthOfNameAndPassword = 20;

        /// <summary>
        /// The file path where records are persisted.
        /// </summary>
        internal const string TransactionFolderPath = "Data/Transactions";

        /// <summary>
        /// The file path where the users are persisted.
        /// </summary>
        internal const string UserFilePath = "Data/users.csv";

        /// <summary>
        /// The CSV header line used when creating the records file.
        /// </summary>
        internal const string FinanceCsvHeader = "Id,Date,Type,Classification,Amount,Description";

        /// <summary>
        /// The CSV header line used when creating the users file.
        /// </summary>
        internal const string UserCsvHeader = "Id,Username,Password";
    }
}

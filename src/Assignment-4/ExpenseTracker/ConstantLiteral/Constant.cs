namespace ExpenseTracker.ConstantLiteral
{
    /// <summary>
    /// Provides constants used across the application.
    /// </summary>
    internal class Constant
    {
        /// <summary>
        /// Provides the maximum length of the transaction id.
        /// </summary>
        internal const int MaxLengthOfId = 8;

        /// <summary>
        /// The file path where records are persisted.
        /// </summary>
        internal const string FinanceFilePath = "records.csv";

        internal const string UserFilePath = "users.csv";

        /// <summary>
        /// The CSV header line used when creating the records file.
        /// </summary>
        internal const string FinanceCsvHeader = "Id,Date,Type,Classification,Amount,Description";

        internal const string UserCsvHeader = "Id,Username,Password";
    }
}

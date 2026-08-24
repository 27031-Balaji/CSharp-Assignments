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
        /// The file path where records are persisted.
        /// </summary>
        internal const string FilePath = "records.csv";

        /// <summary>
        /// The CSV header line used when creating the records file.
        /// </summary>
        internal const string CsvHeader = "Id,Date,Type,Classification,Amount,Description";
    }
}

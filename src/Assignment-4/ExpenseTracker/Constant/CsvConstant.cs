namespace ExpenseTracker.Constant
{
    /// <summary>
    /// Provides CSV-related constants used across the application.
    /// </summary>
    internal static class CsvConstant
    {
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

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
        /// Provides the maximum length for the username and password.
        /// </summary>
        internal const int MaxLengthOfNameAndPassword = 20;

        /// <summary>
        /// The folder path where transaction records are persisted.
        /// </summary>
        internal const string TransactionFolderPath = "Data/Transactions";

        /// <summary>
        /// The file path where user records are persisted.
        /// </summary>
        internal const string UserFilePath = "Data/users.csv";

        /// <summary>
        /// The CSV header line used when creating the records file.
        /// </summary>
        internal const string FinanceCsvHeader = "Id,Date,Type,Classification,Amount,Description";

        /// <summary>
        /// The CSV header line used when creating the users file.
        /// </summary>
        internal const string UserCsvHeader = "Id,UserName,PasswordHash,Salt";

        /// <summary>
        /// The salt size to be used for hashing.
        /// </summary>
        internal const int SaltSize = 16;

        /// <summary>
        /// The hash size for the password.
        /// </summary>
        internal const int HashSize = 32;

        /// <summary>
        /// The number of iterations used when generating a password hash.
        /// </summary>
        internal const int Iterations = 10000;
    }
}
